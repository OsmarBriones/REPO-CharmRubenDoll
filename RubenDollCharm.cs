using CharmCore.Api;

namespace CharmRubenDoll
{
	/// <summary>Patron of Art: artistic valuables in your pockets lose half as much value from wear.</summary>
	internal sealed class RubenDollCharm : Charm
	{
		internal const string PrefabName = "Valuable Museum RubenDoll";

		internal RubenDollCharm()
			: base("RubenDoll", "Ruben Doll - Patron of Art", defaultWearPercent: 5f, defaultCooldownSeconds: null, PrefabName)
		{
			// Extra host-synced numbers for this power go here, for example:
			// Amount = AddSetting("Amount", 30f, 1f, 100f, "Description for the config file.");
		}
	}
}
