using Breakout;
using Breakout.Levels;
using Breakout.States;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Input;
namespace BreakoutTests.Integration.StatesTests;
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
        //testing all the fields are correct
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
    public void TestUpdateState() {
        // If levels is empty a new level is created
        Assert.That(gameRunning.Levellst.Count, Is.EqualTo(5));
        for (int i = 0; i < gameRunning.Levellst.Count; i++) {
            gameRunning.Levellst.RemoveAt(i);
        }
        gameRunning.UpdateState();
        Assert.That(gameRunning.Levellst.Count, Is.EqualTo(2));
    }
    [Test]
    public void TestGameWonSwitch() {
        for (int i = 0; i < 10; i++) {
            gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.K);
            gameRunning.UpdateState();
            BreakoutBus.GetBus().ProcessEvents();
        }
        Assert.That(gameRunning.Levellst.Count, Is.EqualTo(0));
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
    [Test]
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
}