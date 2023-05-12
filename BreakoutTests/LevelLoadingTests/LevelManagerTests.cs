using Breakout.Levels;
using Breakout;
using DIKUArcade.Events;

namespace BreakoutTests.LevelLoading;

[TestFixture]
public class LevelManagerTests {
    LevelManager levelManager;
    public LevelManagerTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        levelManager = new LevelManager();
    }
    [Test]
    public void TestEmptyLevel() {
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("nolevel.txt");
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("level1.txt");
        Assert.That(levelManager.EmptyLevel(), Is.False);
    }
    [Test]
    public void TestNewLevel() {
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(0));
        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(0));
        levelManager.NewLevel("nolevel.txt");
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(0));
        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(1));
        // ball with no blocks beacuse level isnt valid
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("level1.txt");
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(76));
        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(1));
        Assert.That(levelManager.EmptyLevel(), Is.False);
    }
}