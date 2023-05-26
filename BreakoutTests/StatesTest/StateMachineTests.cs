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
        Assert.That(statemachine.ActiveState, Is.EqualTo(MainMenu.GetInstance()));

        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_RUNNING"
        });
        Assert.That(statemachine.ActiveState, Is.EqualTo(GameRunning.GetInstance()));

        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_PAUSED"
        });
        Assert.That(statemachine.ActiveState, Is.EqualTo(GamePaused.GetInstance()));

        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "RESUME_STATE",
            StringArg1 = "GAME_RUNNING"
        });
        Assert.That(statemachine.ActiveState, Is.EqualTo(GameRunning.GetInstance()));


        statemachine.ProcessEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_LOST"
        });

        Assert.That(statemachine.ActiveState, Is.EqualTo(GameLost.GetInstance()));
        Assert.That(gamerunning is IGameState);
        Assert.That(gamePaused is IGameState);
        Assert.That(gamelost is IGameState);
        Assert.That(mainmenu is IGameState);
    }
}