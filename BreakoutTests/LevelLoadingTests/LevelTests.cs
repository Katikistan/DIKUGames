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
    }
    [Test]
    public void TestNewLevel() {
        // Assert.That(level.NewLevel("level2.txt"), Is.True);
    }
}

