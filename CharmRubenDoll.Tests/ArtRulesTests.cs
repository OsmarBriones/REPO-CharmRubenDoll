using CharmRubenDoll.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CharmRubenDoll.Tests;

[TestClass]
public class ArtRulesTests
{
	[TestMethod]
	public void ParseNames_TrimsDropsEmptyAndIgnoresCase()
	{
		var names = ArtRules.ParseNames("  Valuable Museum Fish ,, valuable museum TOOTH ,  ");
		Assert.AreEqual(2, names.Count);
		Assert.IsTrue(names.Contains("VALUABLE MUSEUM FISH"));
		Assert.IsTrue(names.Contains("Valuable Museum Tooth"));
	}

	[DataTestMethod]
	[DataRow(null)]
	[DataRow("")]
	[DataRow("   ")]
	[DataRow(" , , ")]
	public void ParseNames_EmptyInputGivesEmptySet(string? list)
	{
		Assert.AreEqual(0, ArtRules.ParseNames(list).Count);
	}

	[TestMethod]
	public void ParseNames_DuplicatesCollapse()
	{
		Assert.AreEqual(1, ArtRules.ParseNames("A, a, A ").Count);
	}

	[DataTestMethod]
	[DataRow("Valuable Museum Fish(Clone)", "Valuable Museum Fish")]
	[DataRow("  Valuable Museum Fish (Clone) ", "Valuable Museum Fish")]
	[DataRow("Valuable Museum Fish", "Valuable Museum Fish")]
	[DataRow(null, "")]
	public void CleanName_StripsCloneSuffix(string? input, string expected)
	{
		Assert.AreEqual(expected, ArtRules.CleanName(input));
	}

	[TestMethod]
	public void IsListed_MatchesClonesAndIgnoresCase()
	{
		var names = ArtRules.ParseNames("Valuable Museum Fish, Valuable Manor Ocarina");
		Assert.IsTrue(ArtRules.IsListed(names, "Valuable Museum Fish(Clone)"));
		Assert.IsTrue(ArtRules.IsListed(names, "valuable manor ocarina"));
		Assert.IsFalse(ArtRules.IsListed(names, "Valuable Museum RubenDoll(Clone)"));
		Assert.IsFalse(ArtRules.IsListed(names, ""));
		Assert.IsFalse(ArtRules.IsListed(names, null));
	}

	[DataTestMethod]
	[DataRow(10f, 50f, 5f)]
	[DataRow(100f, 50f, 50f)]
	[DataRow(100f, 0f, 100f)]
	[DataRow(100f, 25f, 75f)]
	[DataRow(5f, 50f, 3f)]
	[DataRow(100f, 100f, 0f)]
	[DataRow(1f, 100f, 0f)]
	[DataRow(100f, 150f, 0f)]
	[DataRow(100f, -20f, 100f)]
	public void ReduceLoss_AppliesThePercentage(float loss, float percent, float expected)
	{
		Assert.AreEqual(expected, ArtRules.ReduceLoss(loss, percent));
	}

	[DataTestMethod]
	[DataRow(1f, 50f)]
	[DataRow(1f, 99f)]
	[DataRow(2f, 90f)]
	[DataRow(3f, 99.9f)]
	public void ReduceLoss_LossOfAtLeastOneStaysAtLeastOneBelow100(float loss, float percent)
	{
		Assert.IsTrue(ArtRules.ReduceLoss(loss, percent) >= 1f);
	}

	[DataTestMethod]
	[DataRow(0f)]
	[DataRow(-5f)]
	public void ReduceLoss_NonPositiveLossIsUnchanged(float loss)
	{
		Assert.AreEqual(loss, ArtRules.ReduceLoss(loss, 50f));
	}

	[TestMethod]
	public void ReduceLoss_NeverIncreasesTheLoss()
	{
		for (float loss = 0.3f; loss < 200f; loss += 3.7f)
		{
			for (float percent = 0f; percent <= 100f; percent += 12.5f)
			{
				Assert.IsTrue(ArtRules.ReduceLoss(loss, percent) <= loss);
			}
		}
	}
}
