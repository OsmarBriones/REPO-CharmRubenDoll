using CharmCore.Api;

namespace CharmRubenDoll
{
	/// <summary>Patron of Art: artistic valuables in your pockets lose less value from wear.</summary>
	internal sealed class RubenDollCharm : Charm
	{
		internal const string PrefabName = "Valuable Museum RubenDoll";

		internal RubenDollCharm()
			: base("RubenDoll", "Ruben Doll - Patron of Art", defaultWearPercent: 5f, defaultCooldownSeconds: null, PrefabName)
		{
			LossReductionPercent = AddSetting("LossReductionPercent", 50f, 0f, 100f, "How much less value (in percent) your artistic pocketed valuables lose from each wear step while the Ruben Doll is in your pocket. 50 = half the loss, 100 = no loss at all, 0 = no protection. The doll itself wears by WearPercent for each protected step.");
		}

		/// <summary>Percentage of each wear loss that artistic valuables are spared (0 to 100).</summary>
		internal CharmSetting LossReductionPercent { get; }
	}
}
