# CharmRubenDoll Architecture

A Charm mod (layer 3 of the PocketCharms family): one valuable, one power. It depends on **Pockets** (storage) and **CharmCore** (registry, wear, host settings, debug mode).

## The power: Patron of Art

While a player carries the Ruben Doll in a pocket, the artistic valuables that same player carries in their pockets lose 50% less value from charm wear (configurable). The doll pays: it wears `WearPercent` (default 5%) for each protected wear step.

- `RubenDollCharm : Charm` registers the prefab `Valuable Museum RubenDoll`, wear 5%, no cooldown, and the setting `LossReductionPercent`.
- `PatronOfArt` is a plain class. The plugin `Awake` registers its `OnWearing` method once with `CharmsApi.OnWearing`, after `CharmsApi.Register`.
- Host only: CharmCore applies all wear on the master (`CharmWear.ApplyWear`) and calls the wear handlers there with a `WearContext` (`Valuable`, `Carrier`, `Loss`). Clients never run the handler.
- Handler steps: return if the charm is disabled or there is no carrier; return if the valuable is a Ruben Doll or not in the artistic set; return if `CharmsApi.CarriedBy(carrier, charm)` is empty; otherwise `Loss = ArtRules.ReduceLoss(Loss, LossReductionPercent)` and `charm.WearValuable(doll, WearPercent)`.
- Recursion: the doll's wear goes through the same pipeline, but there the doll is `ctx.Valuable`, which is excluded, so it is never reduced and never causes more wear. The nested call runs synchronously on the host before the outer wear is applied.
- Domain (`Domain/ArtRules.cs`, unit tested): list parsing (comma separated, trimmed, case-insensitive, empty entries dropped), `(Clone)` stripping, and `ReduceLoss` (rounds to whole dollars, halves up; a loss of at least 1 stays at least 1 unless the reduction is 100%, which gives 0).

## Configuration

| Key | Type | Range | Default | Notes |
|-----|------|-------|---------|-------|
| `LossReductionPercent` | float, percent | 0 to 100 | 50 | `AddSetting`, host-synchronised. 0 = no protection (the doll still wears), 100 = no loss. Negative values make no sense. |
| `WearPercent` | float, percent | CharmCore range | 5 | Charged to the doll per protected step (times the global wear multiplier). |
| `ArtisticValuables` | string list | n/a | the seven `Valuable Museum` exhibits (Cool brain, Fish, GoldFish, GoldTooth, SilverFish, Toast, Tooth) and `Valuable Manor Ocarina` | Plain `Bind` in section `Charm.RubenDoll`, read once at startup, only the host's copy is used. Not synchronised. |

## Game members used

- `ValuableObject.dollarValueCurrent` / `dollarValueOriginal`: used by CharmCore's wear pipeline that this handler plugs into.
- `PlayerAvatar.photonView`: how CharmCore identifies the carrier.
- The valuable's `gameObject.name` (prefab name plus `(Clone)`), the same way CharmCore matches charms.

These are checked by `CharmRubenDoll.Tests/GameContractTests.cs`. The mod patches nothing (no Harmony).

## Decisions

- The artistic set is a config list so it can change without a new build. Defaults follow the spec's tag set; every name exists as a prefab in the extracted game assets.
- The Ruben Doll is excluded by name from protection even if a user adds it to the list.
- Only the carrier's own pockets count: a doll protects its carrier's valuables, not teammates'.
- If the doll is destroyed by its own wear, `CarriedBy` becomes empty and later steps are not reduced.
- The charm switch is checked with `IsEnabled`. The CharmCore on/off switch is internal; CharmCore only knows carriers and applies wear while it is on, so it is not checked again here.
- Rounding to whole dollars matches the game's whole-dollar values; a loss of at least 1 never rounds to 0 unless the reduction is 100%.

## Debug mode

Provided by CharmCore: the `Debug.DebugMode` setting (off by default) places the valuable on the floor at the start of each level and enables `Charm.LogDebug` output. This charm logs each protected step (loss before and after).

## Known limits

- The game destroys a valuable when its value falls under 15% of its original value. Reduced loss only delays that.
- Looked up by exact prefab name; a game update that renames a prefab needs a mod update.
- The artistic list is not synchronised; the host's file decides.
- The doll pays the full `WearPercent` for each protected step, even when the protected loss was small.
- Not verified in game: that the wear handler fires for every charm's wear in a real session, multiplayer behaviour, and interaction with other charms' wear (see the QA battery).
