using Galaga.GalagaStates;
using DIKUArcade.Events;
using Galaga;
namespace galagaTests;
public class StateMachineTesting {
    private StateMachine stateMachine = null!;
    
    public StateMachineTesting () {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        GalagaBus.GetBus().InitializeEventBus(
            new List<GameEventType> {
                GameEventType.GameStateEvent,
                GameEventType.InputEvent,
                GameEventType.WindowEvent
                });
    }
    [SetUp]
    public void InitiateStateMachine() {
        stateMachine = new StateMachine();
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }
    [Test]
    public void TestEventGamePaused() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            }
        );
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }
}