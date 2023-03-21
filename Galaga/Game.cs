using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
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
        window.SetKeyEventHandler(keyHandler);
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, stateMachine);

    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.StringArg1) {
                case "WINDOW_CLOSE":
                    window.CloseWindow();
                    break;
            }
        }
        else if (gameEvent.EventType == GameEventType.GameStateEvent) {
            switch (gameEvent.Message) {
                case "CHANGE_STATE":
                    window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
                    break;
            }
        }
    }
    public override void Render() { //Rendering entities
        stateMachine.ActiveState.RenderState();
    }
    public void
    public override void Update() {
        window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
        GalagaBus.GetBus().ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }
}
