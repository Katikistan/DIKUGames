using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using Breakout.States;
namespace Breakout;
public class Game : DIKUGame, IGameEventProcessor {
    private StateMachine stateMachine;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        BreakoutBus.GetBus().InitializeEventBus(
            new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent,
                GameEventType.GameStateEvent
            });
        window.SetKeyEventHandler(KeyHandler);
        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);

        stateMachine = new StateMachine();
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.StringArg1) {
                case "WINDOW_CLOSE":
                    window.CloseWindow();
                    break;
            }
        }
    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }
    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }
    public override void Update() {
        BreakoutBus.GetBus().ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }
}