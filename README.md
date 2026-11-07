# Charm Ruben Doll

Patron of Art: while the Ruben Doll is in your pocket, your artistic valuables in your pockets lose less value from wear (half as much by default). The doll pays for it.

> **Everyone in the lobby must install this mod, together with Pockets and CharmCore.** The power changes what your own character can do and its state shows on your inventory slot, so friends without the mods would see an empty slot and be unable to use the pocket.

## How to Use

1. Find the **Ruben Doll** valuable and store it in a free inventory slot with [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/) (grab it, press the slot key).
2. Keep your artistic valuables (museum exhibits such as the Fish, Gold Fish, Tooth, Toast or Cool Brain, and the Ocarina) in other pocket slots. Whenever another charm wears one of them down, it loses only half of what it would have lost, and the Ruben Doll loses a little of its own value instead.
3. Take the valuable out of the pocket and the power stops.

## Features

- **Patron of Art:** artistic valuables in your pockets lose 50% less value from charm wear while you carry the doll. The doll itself and other Ruben Dolls are not protected, and it only protects what you carry, not what teammates carry.
- **It costs the doll:** every time it protects a valuable, the doll wears away a share of its own value, and a worn-out doll breaks. The protection only delays the damage: a valuable that falls too low in value still breaks eventually.
- **Everything is configurable:** wear, how strong the protection is and which valuables count as art. The host's choices apply to everyone.

## Requirements
- [BepInEx Pack for R.E.P.O.](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
- [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/)
- [CharmCore](https://thunderstore.io/c/repo/p/OsmarBriones/CharmCore/)

## Installation
1. Install through r2modman / Thunderstore Mod Manager (Pockets and CharmCore come with it), or place `CharmRubenDoll.dll` next to `Pockets.dll` and `CharmCore.dll` in `BepInEx/plugins`. **All players need it.**
2. Launch the game once; the configuration file is created inside `BepInEx/config`.

## Configuration
Settings are in `com.osmar.CharmRubenDoll.cfg` inside `BepInEx/config`, section `Charm.RubenDoll`. The host's choices apply to everyone.

- `Enabled` (default `true`): turn this charm on or off.
- `WearPercent` (default `5`): share of the doll's original value lost each time it protects a valuable.
- `LossReductionPercent` (default `50`, from 0 to 100): how much less value the protected valuables lose. 100 means no loss at all.
- `ArtisticValuables` (text list): comma-separated names of the valuables that count as art. It is read when the game starts, so restart to apply changes, and only the host's list counts.

Section `Debug`: `DebugMode` (default `false`) places a Ruben Doll on the floor in front of you when a level starts (host or singleplayer) and writes extra log lines. For testing. `SpawnNow` (default `false`, needs `DebugMode`): set it to `true` while a level is running to place one right away; it switches itself back off.

## Issues & Bug Reports
The official way to report issues, suggest improvements, or submit feedback is by opening an issue on the official GitHub repository:
👉 [GitHub Issues](https://github.com/OsmarBriones/REPO-CharmRubenDoll/issues)

## Credits
Developed by **Osmar Briones**
