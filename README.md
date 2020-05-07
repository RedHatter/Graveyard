[![Latest Release](https://img.shields.io/github/release-pre/RedHatter/Graveyard.svg)](https://github.com/RedHatter/Graveyard/releases)

# Graveyard
Graveyard is a plugin for the [Hearthstone Deck Tracker](https://github.com/HearthSim/Hearthstone-Deck-Tracker).

Graveyard displays minions that have died this game, both friendly and enemy. In addition Graveyard will display specialized views for decks containing certain cards.

![Resurrect and N'Zoth](images/resurrect.png?raw=true)

![Discarded](images/discarded.png?raw=true)

* **Anyfin Can Happen**
Displays Murlocs killed by both the player and opponent as well as a damage calculator (thanks to [AnyfinCalculator](https://github.com/ericBG/AnyfinCalculator)).

* **N'Zoth the Corruptor**
Displays deathrattle minions that have died.

* **Hadronox**
Displays taunt minions that have died.

* **Bloodraver Gul'dan**
Displays friendly demons that have died.

* **Resurrect**, **Onyx Bishop**, **Eternal Servitude**, **Lesser Diamond Spellstone**,  **Mass Resurrection**, **Catrina Muerte** and **Kazakus**
Displays resurrect chance next to each minion that has died.

* **Soulwarden**
Displays generation chance next to each card that has been discarded.

* **Nine Lives**, **Da Undatakah** and **Twilight's Call**
Displays friendly deathrattle minions that have died.

* **Witching Hour**
Displays friendly beasts that have died.

* **Dragoncaller Alanna**, **Shudderwock** and **Tess Greymane**
Displays respective cards that have been played (5+ mana spells for Alanna, battlecry cards for Shudderwock and cards of other classes for Tess).

* **The Caverns Below**
Displays all friendly minions that have been played.

* **Kangor's Endless Army**
Displays summon chance of friendly (base) mechs that have died.

* **Mulchmuncher**
Displays friendly Treants that have died.

* **Zul'jin**
Displays spells that have been cast.

* **Hoard Pillager** and **Rummaging Kobold**
Displays equip/return chance of weapons used this game.

## Installing
1. Download *Graveyard.zip* from [here](https://github.com/RedHatter/Graveyard/releases).
2. If needed, unblock the zip file before unzipping, by [right-clicking it and choosing properties](http://blogs.msdn.com/b/delay/p/unblockingdownloadedfile.aspx):
![Unblock](images/unblock.png?raw=true)
3. Extract the containing file *Graveyard.dll* into `%AppData%\HearthstoneDeckTracker\Plugins`.
4. (Re-)start HDT.
5. Enable the plugin in `Options -> Tracker -> Plugins`.
