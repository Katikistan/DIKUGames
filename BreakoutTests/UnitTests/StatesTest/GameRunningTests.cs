using Breakout;
using Breakout.Levels;
using Breakout.States;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Input;
namespace BreakoutTests.StatesTests;
[TestFixture]
public class GameRunningTests {
    private GameRunning gameRunning;
    private GameEvent gameWonState;
    private GameEvent pressEscape;
    private GameEvent playerMoveLeft;
    private GameEvent playerMoveRight;
    private StateMachine stateMachine;
    public GameRunningTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
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
    public void TestUpdateState() {
        // If levels is empty a new level is created
        Assert.AreEqual(5,gameRunning.Levellst.Count);
        for (int i = 0; i < gameRunning.Levellst.Count; i++) {
            gameRunning.Levellst.RemoveAt(i);
        }
        gameRunning.UpdateState();
        Assert.AreEqual(gameRunning.Levellst.Count, 2);
    }
    [Test]
    public void TestLoadLevels() {
        for (int i = 0; i < 10; i++) {
            gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.K);
            gameRunning.UpdateState();
            BreakoutBus.GetBus().ProcessEvents();
        }
        Assert.That((stateMachine.ActiveState).GetType(), Is.EqualTo((new GameWon()).GetType()));
    }
    [Test]
    public void TestMoveLeftKey(){
        float movementSpeed = 0.01f;
        float startPosX = 0.425f;
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        var playerPos = gameRunning.LevelManager.Player.GetPosition();
        // Player moved to the left
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
        // Left key is released
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Left);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        // Player pos is the same/hasn't moved
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestMoveRightKey(){
        float movementSpeed = 0.01f;
        float startPosX = 0.425f;
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        var playerPos = gameRunning.LevelManager.Player.GetPosition();
        // Player moved to the right
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
        // Right key is released
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Right);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        // Player pos is the same/hasn't moved
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
    }
        public void TestMoveAKey(){
        float movementSpeed = 0.01f;
        float startPosX = 0.425f;
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.A);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        var playerPos = gameRunning.LevelManager.Player.GetPosition();
        // Player moved to the left
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
        // A key is released
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.A);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        // Player pos is the same/hasn't moved
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestMoveDKey(){
        float movementSpeed = 0.01f;
        float startPosX = 0.425f;
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.D);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        var playerPos = gameRunning.LevelManager.Player.GetPosition();
        // Player moved to the right
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
        // D key is released
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.D);
        BreakoutBus.GetBus().ProcessEvents();
        gameRunning.LevelManager.Player.Move();
        // Player pos is the same/hasn't moved
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
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
        Assert.That(gameRunning.LevelManager.Blocks.CountEntities(), Is.EqualTo(0));
    }
}