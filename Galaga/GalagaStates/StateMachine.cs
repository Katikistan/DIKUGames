using DIKUArcade.Events;
using DIKUArcade.State;
namespace Galaga.GalagaStates;
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState {
        get; private set;
    }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        ActiveState = MainMenu.GetInstance();
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.GameStateEvent) {
            switch (gameEvent.Message) {
                case ("RESUME_STATE"):
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                case ("CHANGE_STATE"):
                    // Resumes a state and makes it the ActiveState
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    // Resets the state
                    ActiveState.ResetState();
                    // GetInstance() of ActiveState which is null, therefore it's initialized
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
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
