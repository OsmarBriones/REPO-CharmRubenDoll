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

		internal PatronOfArt(RubenDollCharm charm, HashSet<string> artisticNames)
		{
			this.charm = charm;
			this.artisticNames = artisticNames;
		}

		/// <summary>Handler for <see cref="CharmsApi.OnWearing"/>.</summary>
		internal void OnWearing(WearContext context)
		{
			// The core switch is internal to CharmCore; the charm switch is ours to check.
			if (!charm.IsEnabled || context.Carrier == null || context.Valuable == null)
			{
				return;
			}

			string name = ArtRules.CleanName(context.Valuable.gameObject.name);

			// The doll and other dolls are never protected. This also stops recursion: the doll's own wear step has the doll as its valuable.
			if (string.Equals(name, RubenDollCharm.PrefabName, StringComparison.OrdinalIgnoreCase) || !ArtRules.IsListed(artisticNames, name))
			{
				return;
			}

			IReadOnlyList<ValuableObject> dolls = CharmsApi.CarriedBy(context.Carrier, charm);
			if (dolls.Count == 0 || dolls[0] == null)
			{
				return;
			}

			float before = context.Loss;
			context.Loss = ArtRules.ReduceLoss(before, charm.LossReductionPercent.Value);
			charm.LogDebug($"Patron of Art: {name} loss {before:0} -> {context.Loss:0}; the doll wears {charm.WearPercent:0.#}%");
			charm.WearValuable(dolls[0], charm.WearPercent);
		}
	}
}
