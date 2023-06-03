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
        levelCreator.CreateLevel("level2.txt");
        Assert.That(levelCreator.Blocks.CountEntities(), Is.EqualTo(72));
        levelCreator.CreateLevel("nolevel.txt");
        // level isnt created beacuse nolevel.txt dosent exist.
        Assert.That(levelCreator.Blocks.CountEntities(), Is.EqualTo(72));

    }
}