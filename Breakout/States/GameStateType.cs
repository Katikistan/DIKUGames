using System;

namespace Breakout.States;
public enum GameStateType {
    GameRunning,
    GamePaused,
    MainMenu,
    GameLost,
    GameWon
}
public class StateTransformer {
    public static GameStateType TransformStringToState(string state) {
        switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "GAME_LOST":
                return GameStateType.GameLost;
            case "GAME_WON":
                return GameStateType.GameWon;
            default:
                throw new ArgumentException("Invalid GameStateType string");
        }
    }
}

