using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
namespace Galaga.GalagaStates;
public class GamePaused : IGameState, IGameEventProcessor {
    private static GamePaused instance = null;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.InitializeGameState();
        }
        return GamePaused.instance;
    }
    public void ProcessEvent(GameEvent gameEvent) {
    }
    public void InitializeGameState() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
    }
    public void ResetState() {
        GamePaused.instance = new GamePaused();
        GamePaused.instance.InitializeGameState();
    }
    public void UpdateState() {
        GalagaBus.GetBus().ProcessEventsSequentially();
    }
    public void RenderState() {}
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {}
}