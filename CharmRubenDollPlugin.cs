using BepInEx;
using CharmRubenDoll.Domain;
using CharmCore.Api;
using UnityEngine;

namespace CharmRubenDoll
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	[BepInDependency("com.osmar.Pockets", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("com.osmar.CharmCore", BepInDependency.DependencyFlags.HardDependency)]
	public class CharmRubenDollPlugin : BaseUnityPlugin
	{
		public const string PluginGuid = "com.osmar.CharmRubenDoll";
		public const string PluginName = "CharmRubenDoll";
		public const string PluginVersion = "1.0.0";

		internal static new BepInEx.Logging.ManualLogSource Logger { get; private set; } = null!;

		private void Awake()
		{
			Logger = base.Logger;

			// Prevent the plugin from being deleted
			this.gameObject.transform.parent = null;
			this.gameObject.hideFlags = HideFlags.HideAndDontSave;

			// Registering binds Enabled, WearPercent, CooldownSeconds and the Debug section (DebugMode) in this mod's config file.
			RubenDollCharm charm = new RubenDollCharm();
			CharmsApi.Register(charm, Config);

			// Read once at startup. Only the host's copy matters: the wear handler runs on the host.
			string artisticList = Config.Bind(
				"Charm.RubenDoll",
				"ArtisticValuables",
				"Valuable Museum Cool brain, Valuable Museum Fish, Valuable Museum GoldFish, Valuable Museum GoldTooth, Valuable Museum SilverFish, Valuable Museum Toast, Valuable Museum Tooth, Valuable Manor Ocarina",
				"Comma-separated prefab names of the valuables that count as art. Matched ignoring case and spaces around the names. Read once when the game starts (restart to apply); only the host's list is used.").Value;

			PatronOfArt patron = new PatronOfArt(charm, ArtRules.ParseNames(artisticList));
			charm.Patron = patron;
			CharmsApi.OnWearing(patron.OnWearing);

			Logger.LogInfo($"{PluginName} {PluginVersion} loaded!");
		}
	}
}
