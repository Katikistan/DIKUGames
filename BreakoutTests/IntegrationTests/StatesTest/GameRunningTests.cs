using Breakout;
using Breakout.Levels;
using Breakout.States;
using DIKUArcade.Entities;
namespace BreakoutTests.Integration.StatesTests;
[TestFixture]
public class GameRunningTests {
    private GameRunning gameRunning;

    public GameRunningTests() {
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
        Assert.That(gameRunning.Levellst is List<string>);
        Assert.That(gameRunning.LevelManager is LevelManager);
        Assert.That(gameRunning.Points is Points);

        Assert.AreEqual(gameRunning.Levellst[0], "level1.txt");
        Assert.AreEqual(gameRunning.Levellst[1], "level2.txt");
        Assert.AreEqual(gameRunning.Levellst[2], "level3.txt");
    }
    [Test]
    public void TestLoadLevels() {
        // If levels is empty a new level is created
        Assert.AreEqual(5,gameRunning.Levellst.Count);
        for (int i = 0; i < gameRunning.Levellst.Count; i++) {
            gameRunning.Levellst.RemoveAt(i);
        }
        gameRunning.UpdateState();
        Assert.AreEqual(gameRunning.Levellst.Count, 2);
    }
}