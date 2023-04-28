using Breakout.Levels;
namespace BreakoutTests.LevelLoading;
[TestFixture]
public class LevelTests {
    private Level level = null!;
    public LevelTests() {
    }
    [SetUp]
    public void Setup() {
        level = new Level();
        level.LevelLoader.ChangePath(Path.Combine(@"../../../../", "Breakout", "Assets", "Levels"));
    }
    [Test]
    public void TestNewLevel() {
        Assert.That(level.NewLevel("level1.txt"), Is.True);
        Assert.That(level.NewLevel("level2.txt"), Is.True);
        Assert.That(level.NewLevel("level3.txt"), Is.True);
    }
}