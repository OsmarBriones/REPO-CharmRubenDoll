# Changelog

## [Unreleased]

### Changed
- Runtime hygiene pass: the wear handler runs its cheap checks first (art/doll classification cached by instance id, cleared when a level ends) and only looks up the pocket for protectable art; debug line guarded.

### Added
- Patron of Art: while the Ruben Doll is in your pocket, your artistic pocketed valuables lose 50% less value from charm wear (`LossReductionPercent`); the doll wears `WearPercent` for each protected step.
- `ArtisticValuables` list (read at startup, host only) with the museum exhibits and the Ocarina by default.
- `Debug.DebugMode` (off by default): puts the Ruben Doll on the floor at the start of each level and writes detailed logs.
