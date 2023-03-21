using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Galaga.GalagaStates;
using System.Collections.Generic;
namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private StateMachine stateMachine;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        GalagaBus.GetBus().InitializeEventBus(
            new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent,
                GameEventType.GameStateEvent
            });
        window.SetKeyEventHandler(KeyHandler);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        GalagaBus.GetBus().Flush();

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
        stateMachine.ActiveState.HandleKeyEvent(action,key);
    }
    public override void Render() { //Rendering entities
        stateMachine.ActiveState.RenderState();
    }

    public override void Update() {
        GalagaBus.GetBus().ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }
}
