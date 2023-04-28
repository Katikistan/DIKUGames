using Breakout.Levels;
namespace BreakoutTests.LevelLoading;
[TestFixture]
public class LevelCreatorTests {
    private LevelCreator levelCreator = null!;
    public LevelCreatorTests() {
    }
    [SetUp]
    public void Setup() {
        levelCreator = new LevelCreator();
        levelCreator.LevelReader.ChangePath(Path.Combine(@"../../../../", "Breakout", "Assets", "Levels"));
    }
    [Test]
    public void TestNewLevel() {
        Assert.That(levelCreator.CreateLevel("level1.txt"), Is.True);
        Assert.That(levelCreator.CreateLevel("level2.txt"), Is.True);
        Assert.That(levelCreator.CreateLevel("level3.txt"), Is.True);
    }
}