[![Build Status](https://ci.appveyor.com/api/projects/status/github/batstyx/Graveyard?svg=true)](https://ci.appveyor.com/project/batstyx/Graveyard)

[![Latest Release](https://img.shields.io/github/release-pre/batstyx/Graveyard.svg)](https://github.com/batstyx/Graveyard/releases)

Forked from [RedHatter/Graveyard](https://github.com/RedHatter/Graveyard) (no longer maintained)

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

* **Resurrect** or **Onyx Bishop** or **Eternal Servitude** or **Kazakus**
Displays resurrect chance next to each minion that has died.

* **Cruel Dinomancer**
Displays resurrect chance next to each minion that has been discarded.

## Installing
1. Download *Graveyard.zip* from [here](https://github.com/batstyx/Graveyard/releases).
2. If needed, unblock the zip file before unzipping, by [right-clicking it and choosing properties](http://blogs.msdn.com/b/delay/p/unblockingdownloadedfile.aspx):
![Unblock](images/unblock.png?raw=true)
3. Extract the containing file *Graveyard.dll* into `%AppData%\HearthstoneDeckTracker\Plugins`.
4. (Re-)start HDT.
5. Enable the plugin in `Options -> Tracker -> Plugins`.
