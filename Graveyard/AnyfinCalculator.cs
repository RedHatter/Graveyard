/**
 *  Code originally from https://github.com/ericBG/AnyfinCalculator.
 *  Thank you ericBG for all the hard work!
 *
 * The MIT License (MIT)
 *
 * Copyright (c) 2016 ericBG
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace HDT.Plugins.Graveyard
{
	public class MurlocInfo
	{
		public enum State
		{
			Dead,
			OnBoard,
			OnOpponentsBoard
		}

		public State BoardState { get; set; }
		public bool CanAttack { get; set; }
		public bool IsSilenced { get; set; }
		public bool AreBuffsApplied { get; set; }
		public int Attack { get; set; }
		public Card Murloc { get; set; }
	}

	public class Range<T> where T : IComparable<T>
	{
		public T Minimum { get; set; }
		public T Maximum { get; set; }
	}

	public class AnyfinCalculator
	{
		public static Range<int> CalculateDamageDealt(List<Card> Graveyard)
		{
			var DeadMurlocs = new List<Card>();
			foreach (var card in Graveyard)
			{
				for (var i = 0; i < card.Count; i++)
				{
					var c = card.Clone() as Card;
					c.Count = 1;
					DeadMurlocs.Add(c);
				}
			}

			if (Core.Game.PlayerMinionCount >= 7)
			{
				return new Range<int>() { Maximum = 0, Minimum = 0 };
			}

			if (DeadMurlocs.Count() + Core.Game.PlayerMinionCount <= 7)
			{
				var damage = CalculateDamageInternal(DeadMurlocs, Core.Game.Player.Board, Core.Game.Opponent.Board);
				return new Range<int> { Maximum = damage, Minimum = damage };
			}

			var sw = Stopwatch.StartNew();
			var board = Core.Game.Player.Board.ToList();
			var opponent = Core.Game.Opponent.Board.ToList();
			int? min = null, max = null;
			foreach (var combination in Combinations(DeadMurlocs, 7 - Core.Game.PlayerMinionCount))
			{
				var damage = CalculateDamageInternal(combination, board, opponent);
				if (damage > max || !max.HasValue)
				{
					max = damage;
				}
				if (damage < min || !min.HasValue)
				{
					min = damage;
				}
			}
			sw.Stop();

			Log.Debug($"Time to calculate the possibilities: {sw.Elapsed.ToString("ss\\:fff")}");
			return new Range<int> { Maximum = max.Value, Minimum = min.Value };
		}

		private static int CalculateDamageInternal(IEnumerable<Card> graveyard, IEnumerable<Entity> friendlyBoard, IEnumerable<Entity> opponentBoard)
		{
			var deadMurlocs = graveyard.ToList();
			var aliveMurlocs = friendlyBoard.Where(c => c.Card.IsMurloc()).ToList();
			var opponentMurlocs = opponentBoard.Where(c => c.Card.IsMurloc()).ToList();

			//compiles together into one big freaking list
			var murlocs =
				deadMurlocs.Select(
					c =>
						new MurlocInfo
						{
							AreBuffsApplied = false,
							Attack = c.Attack,
							BoardState = MurlocInfo.State.Dead,
							CanAttack = c.IsChargeMurloc(),
							IsSilenced = false,
							Murloc = c
						})
					.Concat(
						aliveMurlocs.Select(
							ent =>
								new MurlocInfo
								{
									AreBuffsApplied = true,
									Attack = ent.GetTag(GameTag.ATK),
									BoardState = MurlocInfo.State.OnBoard,
									CanAttack = CanAttack(ent),
									IsSilenced = IsSilenced(ent),
									Murloc = ent.Card
								}))
					.Concat(
						opponentMurlocs.Select(
							ent =>
								new MurlocInfo
								{
									AreBuffsApplied = false,
									Attack = ent.Card.Attack,
									BoardState = MurlocInfo.State.OnOpponentsBoard,
									CanAttack = false,
									IsSilenced = IsSilenced(ent),
									Murloc = ent.Card
								})).ToList();

			// Calculate which of the murlocs give buffs (now only your own murlocs)
			var nonSilencedWarleaders = murlocs.Count(m => m.BoardState == MurlocInfo.State.OnBoard && m.Murloc.IsWarleader() && !m.IsSilenced);
			var nonSilencedGrimscales = murlocs.Count(m => m.BoardState == MurlocInfo.State.OnBoard && m.Murloc.IsGrimscale() && !m.IsSilenced);

			// Get the murlocs that will be summoned
			var murlocsToBeSummoned = murlocs.Count(m => m.BoardState == MurlocInfo.State.Dead);

			// Go through each currently buffed murloc and remove the buffs
			foreach (var murloc in murlocs.Where(t => t.AreBuffsApplied))
			{
				murloc.AreBuffsApplied = false;
				murloc.Attack -= nonSilencedGrimscales + (nonSilencedWarleaders*2);
				if (murloc.IsSilenced)
				{
					continue;
				}
				if (murloc.Murloc.IsGrimscale())
				{
					murloc.Attack += 1;
				}
				if (murloc.Murloc.IsWarleader())
				{
					murloc.Attack += 2;
				}
				if (murloc.Murloc.IsMurkEye())
				{
					murloc.Attack -= (murlocs.Count(m => m.BoardState != MurlocInfo.State.Dead) - 1);
				}
			}

			// Add the now summoned buffers to the pool
			nonSilencedWarleaders += murlocs.Count(m => m.BoardState == MurlocInfo.State.Dead && m.Murloc.IsWarleader());
			nonSilencedGrimscales += murlocs.Count(m => m.BoardState == MurlocInfo.State.Dead && m.Murloc.IsGrimscale());

			// Go through the murlocs on the board and apply all of the final buffs
			foreach (var murloc in murlocs)
			{
				murloc.AreBuffsApplied = true;
				murloc.Attack += nonSilencedGrimscales + (nonSilencedWarleaders*2);
				if (murloc.IsSilenced)
				{
					continue;
				}
				if (murloc.Murloc.IsWarleader())
				{
					murloc.Attack -= 2;
				}
				if (murloc.Murloc.IsGrimscale())
				{
					murloc.Attack -= 1;
				}
				if (murloc.Murloc.IsMurkEye())
				{
					murloc.Attack += (murlocs.Count - 1);
				}
				if (murloc.Murloc.IsTidecaller())
				{
					murloc.Attack += murlocsToBeSummoned;
				}
			}

			Log.Debug(murlocs.Aggregate("", (s, m) =>	s + $"{m.Murloc.Name}{(m.IsSilenced ? " (Silenced)" : "")}: {m.Attack} {(!m.CanAttack ? "(Can't Attack)" : "")}\n"));
			return murlocs.Sum(m => m.CanAttack ? m.Attack : 0);
		}

		private static bool IsSilenced(Entity entity) => entity.GetTag(GameTag.SILENCED) == 1;

		private static bool CanAttack(Entity entity)
		{
			if (entity.GetTag(GameTag.CANT_ATTACK) == 1 || entity.GetTag(GameTag.FROZEN) == 1)
			{
				return false;
			}
			if (entity.GetTag(GameTag.EXHAUSTED) == 1)
			{
				//from reading the HDT source, it seems like internally Charge minions still have summoning sickness
				return entity.GetTag(GameTag.CHARGE) == 1 && entity.GetTag(GameTag.NUM_ATTACKS_THIS_TURN) < MaxAttacks(entity);
			}

			return entity.GetTag(GameTag.NUM_ATTACKS_THIS_TURN) < MaxAttacks(entity);
		}

		private static int MaxAttacks(Entity entity)
		{
			// GVG_111t == V-07-TR-0N (MegaWindfury, 4x attack)
			if (entity.CardId == "GVG_111t")
			{
				return 4;
			}
			// if it has windfury it can attack twice, else it can only attack once
			return entity.GetTag(GameTag.WINDFURY) == 1 ? 2 : 1;
		}

		public static IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> elements, int k)
		{
			return k == 0 ? new[] { new T[0] } : elements.SelectMany((e, i) => Combinations(elements.Skip(i + 1), k - 1).Select(c => (new[] {e}).Concat(c)));
		}
	}

	public static class Murlocs
	{
		static Murlocs()
		{
			BluegillWarrior = Database.GetCardFromId("CS2_173");
			GrimscaleOracle = Database.GetCardFromId("EX1_508");
			MurlocWarleader = Database.GetCardFromId("EX1_507");
			OldMurkEye = Database.GetCardFromId("EX1_062");
			MurlocTidecaller = Database.GetCardFromId("EX1_509");
			AnyfinCanHappen = Database.GetCardFromId("LOE_026");
		}

		public static Card BluegillWarrior { get; }
		public static Card GrimscaleOracle { get; }
		public static Card MurlocWarleader { get; }
		public static Card OldMurkEye { get; }
		public static Card MurlocTidecaller { get; }
		public static Card AnyfinCanHappen { get; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsMurloc(this Card card) => card.Race == "Murloc";

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsChargeMurloc(this Card card) => card.Id == OldMurkEye.Id || card.Id == BluegillWarrior.Id;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBluegill(this Card card) => card.Id == BluegillWarrior.Id;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsGrimscale(this Card card) => card.Id == GrimscaleOracle.Id;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsWarleader(this Card card) => card.Id == MurlocWarleader.Id;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsMurkEye(this Card card) => card.Id == OldMurkEye.Id;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsTidecaller(this Card card) => card.Id == MurlocTidecaller.Id;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAnyfin(this Card card) => card.Id == AnyfinCanHappen.Id;
	}
}
