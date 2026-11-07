using System;
using System.Collections.Generic;

namespace CharmRubenDoll.Domain
{
	/// <summary>Game-free rules of Patron of Art: which valuables count as art and how a wear loss is softened. Unit tested.</summary>
	internal static class ArtRules
	{
		/// <summary>Suffix Unity adds to the name of an instantiated prefab.</summary>
		private const string CloneSuffix = "(Clone)";

		/// <summary>Turns a comma-separated list of prefab names into a case-insensitive set. Entries are trimmed; empty entries are dropped.</summary>
		internal static HashSet<string> ParseNames(string? list)
		{
			HashSet<string> names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (list == null || string.IsNullOrWhiteSpace(list))
			{
				return names;
			}

			foreach (string part in list.Split(','))
			{
				string name = part.Trim();
				if (name.Length > 0)
				{
					names.Add(name);
				}
			}

			return names;
		}

		/// <summary>The prefab name of an object name: "(Clone)" stripped (every occurrence) and spaces trimmed.</summary>
		internal static string CleanName(string? objectName)
		{
			return (objectName ?? string.Empty).Replace(CloneSuffix, string.Empty).Trim();
		}

		/// <summary>True when the object name (with or without "(Clone)") is in the set.</summary>
		internal static bool IsListed(HashSet<string> names, string? objectName)
		{
			string clean = CleanName(objectName);
			return clean.Length > 0 && names.Contains(clean);
		}

		/// <summary>
		/// The loss that remains after softening it by <paramref name="reductionPercent"/> (0 to 100, clamped). The result is rounded
		/// to the nearest whole dollar (halves go up). A loss of at least 1 stays at least 1 unless the reduction is 100%, which
		/// gives exactly 0. Losses of 0 or less are returned unchanged.
		/// </summary>
		internal static float ReduceLoss(float loss, float reductionPercent)
		{
			if (!(loss > 0f))
			{
				return loss;
			}

			float percent = reductionPercent < 0f ? 0f : reductionPercent > 100f ? 100f : reductionPercent;
			if (percent >= 100f)
			{
				return 0f;
			}

			float reduced = (float)Math.Round(loss * (1f - (percent / 100f)), MidpointRounding.AwayFromZero);
			if (loss >= 1f && reduced < 1f)
			{
				reduced = 1f;
			}

			return reduced > loss ? loss : reduced;
		}
	}
}
