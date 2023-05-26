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
        Assert.That(gameRunning.LevelManager, Is.Not.EqualTo(null!));
        Assert.That(gameRunning.Points, Is.Not.EqualTo (null!));
        Assert.That(gameRunning.Background, Is.Not.EqualTo(null!));

        Assert.That(gameRunning.Health is Health);
        Assert.That(gameRunning.Background is Entity);
        Assert.That(gameRunning.Levels is List<string>);
        Assert.That(gameRunning.LevelManager is LevelManager);
        Assert.That(gameRunning.Points is Points);

        Assert.That(gameRunning.Levels[0], Is.EqualTo("level1.txt"));
        Assert.That(gameRunning.Levels[1], Is.EqualTo("level2.txt"));
        Assert.That(gameRunning.Levels[2], Is.EqualTo("level3.txt"));
    }
    [Test]
    public void TestLoadLevels() {
        // If levels is empty a new level is created
        Assert.That(gameRunning.Levels.Count, Is.EqualTo(5));
        for (int i = 0; i < gameRunning.Levels.Count; i++) {
            gameRunning.Levels.RemoveAt(i);
        }
        gameRunning.UpdateState();
        Assert.That(gameRunning.Levels.Count, Is.EqualTo(2));
    }
}