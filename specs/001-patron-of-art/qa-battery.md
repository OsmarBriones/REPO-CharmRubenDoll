# QA battery: Patron of Art (CharmRubenDoll)

Deploy: Pockets, CharmCore and CharmRubenDoll are in the r2modman `Debug` profile (compare hashes with the builds). Restart the game first.

## Quick start with debug mode
Set `DebugMode = true` in the `Debug` section of `BepInEx/config/com.osmar.CharmRubenDoll.cfg` (off by default). When a level starts, a Ruben Doll is placed on the floor about 2.5 m in front of you; the log then also shows the detailed lines. Any level works.

## Single player
- [ ] Log shows `Charm registered: Ruben Doll - Patron of Art` and `CharmRubenDoll 1.0.0 loaded!`.
- [ ] Store the valuable in a slot: the slot shows its picture and value.
- [ ] With the doll and one artistic valuable (for example a Fish) in pockets, plus another charm that wears (for example Coffee Cup): use the other charm. The log (DebugMode) shows a line "Patron of Art: ... loss X -> Y"; the Fish loses about half of what it loses without the doll, and the doll loses 5% of its original value.
- [ ] Without the doll the same wear takes the full amount.
- [ ] A non-artistic valuable (for example a diamond) is not reduced and the doll does not wear.
- [ ] Two Ruben Dolls pocketed: each doll wears normally from other charms, never reduced.
- [ ] An artistic valuable in another player pocket while only you carry the doll: no protection.
- [ ] Take the doll out: the protection stops.
- [ ] Keep protecting until the doll breaks: the slot empties, protection stops, no errors in the log. A protected valuable still breaks later (under 15% of its value).

## Settings
- [ ] `LossReductionPercent = 100`: protected valuables lose nothing; `0`: no reduction (doll still wears). `WearPercent = 0`: the doll never wears.
- [ ] Add Diamond to `ArtisticValuables` and restart: the diamond is now protected. Empty list: nothing protected.
- [ ] Charm `Enabled = false`: no effect and no wear. CharmCore `Enabled = false`: nothing works.

## Multiplayer (host and client)
- [ ] A client carries the doll and a Fish: wear applied to the Fish is reduced, values match on both machines.
- [ ] Host LossReductionPercent overrides the client's; the host's ArtisticValuables list is the one used.
- [ ] Client leaves and rejoins: protection resumes when they carry the doll again.

Log patterns: `Charm registered`, `threw`, `Exception`.
