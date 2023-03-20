using DIKUArcade.Events.Generic;
using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
namespace Galaga.GalagaStates;
public class GameRunning : IGameState, IGameEventProcessor {
    private static GameRunning instance = null;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }
    public void ProcessEvent(GameEvent gameEvent) {
    }
    public void InitializeGameState() {
        // GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        // GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
    }
    public void ResetState() {
        // GameRunning.instance = new GameRunning();
        // GameRunning.instance.InitializeGameState();
    }
    public void UpdateState() {
        // GalagaBus.GetBus().ProcessEventsSequentially();
    }
    public void RenderState() {}
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {}
}