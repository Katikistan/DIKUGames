using DIKUArcade.Events;
using DIKUArcade.State;
namespace Galaga.GalagaStates;
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        ActiveState = MainMenu.GetInstance();
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.GameStateEvent) {
            switch (gameEvent.StringArg1) {
                case "MAIN_MENU":
                    SwitchState(GameStateType.MainMenu);
                    break;
                case "GAME_PAUSED":
                    SwitchState(GameStateType.GamePaused);
                    break;
                case "GAME_RUNNING":
                    SwitchState(GameStateType.GameRunning);
                    break;

            }
        }
    }
    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
        }
    }
}
