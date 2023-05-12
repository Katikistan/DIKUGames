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
    public void TestNewLevel() {
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("nolevel.txt");
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("level1.txt");
        Assert.That(levelManager.EmptyLevel(), Is.False);
    }
}