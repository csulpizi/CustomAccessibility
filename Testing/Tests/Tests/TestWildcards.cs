namespace Testing.Tests;

[TestClass]
public class TestWildcards
{
    [TestMethod]
    [DataRow(
        "Animals",
        new string[] { "Animals.Koala", "Animals", "Plants", "Oops.Animals" },
        new bool[] { false, true, false, false }
    )]
    [DataRow(
        "Animals**",
        new string[] { "Animals.Koala", "Animals", "Animals.Birds.Crow", "Plants", "Oops.Animals" },
        new bool[] { true, true, true, false, false }
    )]
    [DataRow(
        "Animals.**",
        new string[] { "Animals.Koala", "Animals", "Animals.Birds.Crow", "Plants", "Oops.Animals" },
        new bool[] { true, false, true, false, false }
    )]
    [DataRow(
        "Ani*.**",
        new string[] { "Animals.Koala", "Animals", "Animals.Birds.Crow", "Plants", "Oops.Animals" },
        new bool[] { true, false, true, false, false }
    )]
    [DataRow(
        "**.Birds.**",
        new string[]
        {
            "Animals.Koala",
            "Animals",
            "Animals.Birds.Crow",
            "Animals.Birds.Jackdaw",
            "Plants",
            "Oops.Animals",
            "Oops.Birds.Armadillo"
        },
        new bool[] { false, false, true, true, false, false, true }
    )]
    public void TestRegex(string input, string[] samples, bool[] expected)
    {
        var regex = CustomAccessibility.Util.CoerceWildCardStringToRegex(input);
        Assert.IsGreaterThan(0, samples.Length);
        for (int i = 0; i < samples.Length; i++)
        {
            Assert.AreEqual(regex.IsMatch(samples[i]), expected[i]);
        }
    }
}
