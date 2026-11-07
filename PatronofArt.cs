using System;
using System.Collections.Generic;
using CharmCore.Api;
using CharmRubenDoll.Domain;

namespace CharmRubenDoll
{
	/// <summary>
	/// Patron of Art: while a player carries the Ruben Doll in a pocket, the artistic valuables that same player carries lose
	/// less value from wear, and the doll pays a share of its own value for each protected wear step.
	/// Runs on the host only: CharmCore applies all wear on the master and calls the wear handlers there.
	/// </summary>
	internal sealed class PatronOfArt
	{
		private readonly RubenDollCharm charm;
		private readonly HashSet<string> artisticNames;
		private readonly Dictionary<int, bool> protectable = new Dictionary<int, bool>();
		private const int MaxCachedValuables = 512;

		internal PatronOfArt(RubenDollCharm charm, HashSet<string> artisticNames)
		{
			this.charm = charm;
			this.artisticNames = artisticNames;
		}

		/// <summary>Forgets the cached classification of the level's valuables (keyed by instance id, never by object).</summary>
		internal void ClearCache()
		{
			protectable.Clear();
		}

		/// <summary>Handler for <see cref="CharmsApi.OnWearing"/>. Runs for every wear step of every charm, so the cheap checks come first and nothing is allocated until a protectable valuable is found.</summary>
		internal void OnWearing(WearContext context)
		{
			// The core switch is internal to CharmCore; the charm switch is ours to check.
			if (!charm.IsEnabled)
			{
				return;
			}

			ValuableObject? valuable = context.Valuable;
			if (valuable == null || context.Carrier == null || !IsProtectable(valuable))
			{
				return;
			}

			// Only now the (allocating) pocket lookup, for a valuable that is art.
			IReadOnlyList<ValuableObject> dolls = CharmsApi.CarriedBy(context.Carrier, charm);
			if (dolls.Count == 0 || dolls[0] == null)
			{
				return;
			}

			float before = context.Loss;
			context.Loss = ArtRules.ReduceLoss(before, charm.LossReductionPercent.Value);
			if (charm.DebugMode)
			{
				charm.LogDebug($"Patron of Art: {valuable.gameObject.name} loss {before:0} -> {context.Loss:0}; the doll wears {charm.WearPercent:0.#}%");
			}

			charm.WearValuable(dolls[0], charm.WearPercent);
		}

		/// <summary>True when the valuable is listed art and not a doll (the doll's own wear step must not recurse). Cached by instance id: the name is read once per valuable.</summary>
		private bool IsProtectable(ValuableObject valuable)
		{
			int id = valuable.GetInstanceID();
			if (protectable.TryGetValue(id, out bool known))
			{
				return known;
			}

			string name = ArtRules.CleanName(valuable.gameObject.name);
			bool result = !string.Equals(name, RubenDollCharm.PrefabName, StringComparison.OrdinalIgnoreCase) && ArtRules.IsListed(artisticNames, name);
			if (protectable.Count >= MaxCachedValuables)
			{
				protectable.Clear();
			}

			protectable[id] = result;
			return result;
		}
	}
}
