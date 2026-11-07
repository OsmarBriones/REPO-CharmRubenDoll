using UnityEngine;

namespace CharmRubenDoll
{
	/// <summary>
	/// Patron of Art: artistic valuables in your pockets lose half as much value from wear.
	/// TODO: implement the power. Rules every charm follows:
	/// - Only act while <c>Charm.IsActive</c> (the valuable is in the local player's pocket and everything is on).
	/// - Call <c>Charm.TryUse()</c> at the moment the power triggers and apply the effect only when it returns true:
	///   that starts the cooldown and wears the valuable. For wear over time use <c>Charm.TryUse(percent)</c> or <c>Charm.Wear(percent)</c>.
	/// - Write debug detail with <c>Charm.LogDebug(...)</c>, never with the plugin logger (a test enforces it).
	/// </summary>
	internal sealed class PatronofArt : MonoBehaviour
	{
		internal RubenDollCharm Charm { get; set; } = null!;

		private void Update()
		{
			if (Charm == null || !Charm.IsActive)
			{
				return;
			}

			// TODO: detect the moment the power should trigger, then:
			// if (Charm.TryUse())
			// {
			//     ... apply the effect ...
			//     Charm.LogDebug("Patron of Art triggered");
			// }
		}
	}
}
