using Breakout.States;
using DIKUArcade.Events;
using DIKUArcade.State;
namespace BreakoutTests.Unit.StatesTests;
[TestFixture]
public class StateMachineTests {
    private GameRunning gameRunning;
    private MainMenu mainMenu;
    private GamePaused gamePaused;
    private GameLost gameLost;
    private GameWon gameWon;

    private StateMachine stateMachine;
    public StateMachineTests() {
        CreateGL.CreateOpenGL();
    }
    [Test]
    public void TestStateMachine() {
        stateMachine = new StateMachine();
        Assert.That(stateMachine.ActiveState, Is.EqualTo(MainMenu.GetInstance()));

        stateMachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_RUNNING"
        });
        Assert.That(stateMachine.ActiveState, Is.EqualTo(GameRunning.GetInstance()));

        stateMachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_PAUSED"
        });
        Assert.That(stateMachine.ActiveState, Is.EqualTo(GamePaused.GetInstance()));

        stateMachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "RESUME_STATE",
            StringArg1 = "GAME_RUNNING"
        });
        Assert.That(stateMachine.ActiveState, Is.EqualTo(GameRunning.GetInstance()));

        stateMachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_LOST"
        });
        Assert.That(stateMachine.ActiveState, Is.EqualTo(GameLost.GetInstance()));

        stateMachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_WON"
        });
        Assert.That(stateMachine.ActiveState, Is.EqualTo(GameWon.GetInstance()));

        stateMachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "MAIN_MENU"
        });
        Assert.That(stateMachine.ActiveState, Is.EqualTo(MainMenu.GetInstance()));
    }
    [Test]
    public void TestIsIGameState() {
        gameRunning = new GameRunning();
        mainMenu = new MainMenu();
        gamePaused = new GamePaused();
        gameLost = new GameLost();
        gameWon = new GameWon();

        Assert.That(gameRunning is IGameState);
        Assert.That(gamePaused is IGameState);
        Assert.That(gameLost is IGameState);
        Assert.That(gameWon is IGameState);
        Assert.That(mainMenu is IGameState);
    }
}