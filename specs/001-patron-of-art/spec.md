## User Scenarios

### User Story 1 - Patron of Art (Priority: P1)
A player stores the Ruben Doll in a pocket and gets this power while it stays there: artistic valuables in your pockets lose half as much value from wear.
**Acceptance**: with the valuable pocketed the power works; with it out of the pocket it does not; each use wears the valuable and shows the cooldown on the slot.

## Requirements
- FR-001: Registers a charm with CharmCore for the prefab Valuable Museum RubenDoll.
- FR-002: Settings through CharmCore: Enabled, WearPercent, CooldownSeconds, DebugMode; host-authoritative except DebugMode.
- FR-003: All players install it (Category 2); depends on Pockets and CharmCore.