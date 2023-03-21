using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System.IO;
namespace Galaga.GalagaStates;
public class GameRunning : IGameState/*, IGameEventProcessor*/ {
    private static GameRunning instance = null;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }

    public void InitializeGameState() { //make text and backgroundhere
        // GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        // GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        // GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        // if active button == start, then change text color
    }
    // public void ProcessEvent(GameEvent gameEvent) {
    //     if (gameEvent.StringArg1 == "GAME_RUNNING") {

    //     }
    //     if (gameEvent.StringArg1 == "GAME_RUNNING") {
            
    //     }
    // }

    public void ResetState() {
        instance = new GameRunning();
        instance.InitializeGameState();        
    }
    public void UpdateState() {
    }
    public void RenderState() {
    }
    //match case istedet hvor jeg tjekker action og key samme tid 
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            switch(key) {
                case KeyboardKey.Left:
                    break;
                case KeyboardKey.Right:
                    break;
                case KeyboardKey.Escape:
                    GalagaBus.GetBus().RegisterEvent(
                        new GameEvent{
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_PAUSED"
                        });
                    break;
            }
        }
    }
}
