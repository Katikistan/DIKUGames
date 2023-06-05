using Breakout;
using Breakout.Levels;
using Breakout.States;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Input;
namespace BreakoutTests.Unit.StatesTests;
[TestFixture]
public class GameRunningTests {
    private GameRunning gameRunning;
    private GameEvent gameWonState;
    private GameEvent pressEscape;
    private StateMachine stateMachine;
    public GameRunningTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        gameRunning = new GameRunning();
        gameRunning.InitializeGameState();
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, gameRunning.LevelManager);
        stateMachine = new StateMachine();
        gameWonState = (new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE STATE",
            StringArg1 = "GAME WON"
        });
        pressEscape = (new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE STATE",
            StringArg1 = "GAME PAUSED"
        });
    }
    [Test]
    public void TestInitializeGameState() {
        Assert.That(gameRunning.LevelManager, Is.Not.EqualTo(null!));
        Assert.That(gameRunning.Points, Is.Not.EqualTo(null!));
        Assert.That(gameRunning.Background, Is.Not.EqualTo(null!));

        Assert.That(gameRunning.Health is Health);
        Assert.That(gameRunning.Background is Entity);
        Assert.That(gameRunning.Levellst is List<string>);
        Assert.That(gameRunning.LevelManager is LevelManager);
        Assert.That(gameRunning.Points is Points);

        Assert.That(gameRunning.Levellst[0], Is.EqualTo("level1.txt"));
        Assert.That(gameRunning.Levellst[1], Is.EqualTo("level2.txt"));
        Assert.That(gameRunning.Levellst[2], Is.EqualTo("level3.txt"));
    }
    [Test]
    public void EscapeTest() {
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Escape);
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That((stateMachine.ActiveState).GetType(), Is.EqualTo((new GamePaused()).GetType()));
    }
    [Test]
    public void ClearTest() {
        Assert.That(gameRunning.LevelManager.Blocks.CountEntities(), Is.Not.EqualTo(0));
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.K);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.K);
        BreakoutBus.GetBus().ProcessEvents();
        BreakoutBus.GetBus().ProcessEvents();

        Assert.That(gameRunning.LevelManager.Blocks.CountEntities(), Is.EqualTo(0));
    }
}