using DIKUArcade.Events;
using DIKUArcade.State;
namespace Breakout.States;
[TestFixture]
public class StateMachineTest {
    private GameRunning gamerunning;
    private MainMenu mainmenu;
    private GamePaused gamePaused;
    private GameLost gamelost;

    private StateMachine statemachine;
    public StateMachineTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        gamerunning = new GameRunning();
        mainmenu = new MainMenu();
        gamePaused = new GamePaused();
        gamelost = new GameLost();
        statemachine = new StateMachine();
    }
    [Test]
    public void TestStateMachine() {
        Assert.AreEqual(statemachine.ActiveState, MainMenu.GetInstance());

        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_RUNNING"
        });
        Assert.AreEqual(statemachine.ActiveState, GameRunning.GetInstance());

        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_PAUSED"
        });
        Assert.AreEqual(statemachine.ActiveState, GamePaused.GetInstance());

        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "RESUME_STATE",
            StringArg1 = "GAME_RUNNING"
        });
        Assert.AreEqual(statemachine.ActiveState, GameRunning.GetInstance());


        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_LOST"
        });

        Assert.AreEqual(statemachine.ActiveState, GameLost.GetInstance());
        Assert.That(gamerunning is IGameState);
        Assert.That(gamePaused is IGameState);
        Assert.That(gamelost is IGameState);
        Assert.That(mainmenu is IGameState);
    }
}