using DIKUArcade.Entities;
using Breakout.Levels;

namespace Breakout.States;
[TestFixture]
public class GameRunningTest {
    private GameRunning gameRunning;

    public GameRunningTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        gameRunning = new GameRunning();
        gameRunning.InitializeGameState();
    }
    [Test]
    public void TestInitializeGameState() {
        Assert.AreNotEqual(gameRunning.LevelManager, null!);
        Assert.AreNotEqual(gameRunning.Points, null!);
        Assert.AreNotEqual(gameRunning.Background, null!);

        Assert.That(gameRunning.Health is Health);
        Assert.That(gameRunning.Background is Entity);
        Assert.That(gameRunning.Levels is List<string>);
        Assert.That(gameRunning.LevelManager is LevelManager);
        Assert.That(gameRunning.Points is Points);

        Assert.AreEqual(gameRunning.Levels[0], "level1.txt");
        Assert.AreEqual(gameRunning.Levels[1], "level2.txt");
        Assert.AreEqual(gameRunning.Levels[2], "level3.txt");
    }
    [Test]
    public void TestLoadLevels() {
        // If levels is empty a new level is created
        Assert.AreEqual(5,gameRunning.Levels.Count);
        for (int i = 0; i < gameRunning.Levels.Count; i++) {
            gameRunning.Levels.RemoveAt(i);
        }
        gameRunning.UpdateState();
        Assert.AreEqual(gameRunning.Levels.Count, 2);
    }
}