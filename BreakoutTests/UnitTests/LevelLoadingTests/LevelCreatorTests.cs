using Breakout.Levels;
namespace BreakoutTests.LevelLoadingTests;
[TestFixture]
public class LevelCreatorTests {
    private LevelCreator levelCreator = null!;
    public LevelCreatorTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        levelCreator = new LevelCreator();
    }
    [Test]
    public void TestNewLevel() {
        levelCreator.CreateLevel("level1.txt");
        Assert.That(levelCreator.Blocks.CountEntities(), Is.EqualTo(76));
        Assert.That(levelCreator.HasTimer, Is.EqualTo(true));
        Assert.That(levelCreator.Timer, Is.EqualTo(300));
        levelCreator.CreateLevel("level2.txt");
        Assert.That(levelCreator.Blocks.CountEntities(), Is.EqualTo(72));
        levelCreator.CreateLevel("nolevel.txt");
        // level isnt created beacuse nolevel.txt dosent exist.
        Assert.That(levelCreator.Blocks.CountEntities(), Is.EqualTo(72));
        levelCreator.CreateLevel("Wall.txt");
        Assert.That(levelCreator.HasTimer, Is.EqualTo(false));
        Assert.That(levelCreator.Timer, Is.EqualTo(System.Int32.MaxValue));
    }
}