using Breakout.States;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;

namespace Breakout;
/// <summary>
///  Class responsible for handling a statemachine and keyinputs
/// </summary>
public class Game : DIKUGame, IGameEventProcessor {
    private StateMachine stateMachine;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);

        stateMachine = new StateMachine();
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }
    /// <summary>
    ///  processes windowevents, can close window
    /// </summary>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.StringArg1) {
                case "WINDOW CLOSE":
                    window.CloseWindow();
                    break;
            }
        }
    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }
    /// <summary>
    ///  Renders the active state
    /// </summary>
    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }
    /// <summary>
    ///  Updates the active state
    /// </summary>
    public override void Update() {
        BreakoutBus.GetBus().ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }
}