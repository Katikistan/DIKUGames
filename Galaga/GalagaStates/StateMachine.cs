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
            switch (gameEvent.Message, gameEvent.StringArg1) {
                case ("CHANGE_STATE","MAIN_MENU"):
                    SwitchState(GameStateType.MainMenu);
                    break;
                case ("CHANGE_STATE","GAME_PAUSED"):
                    SwitchState(GameStateType.GamePaused);
                    break;
                case ("CHANGE_STATE","GAME_RUNNING"):
                    SwitchState(GameStateType.GameRunning);
                    break;
                case ("NEW_STATE","GAME_RUNNING"):
                    SwitchState(GameStateType.GameRunning);
                    ActiveState.ResetState();
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
