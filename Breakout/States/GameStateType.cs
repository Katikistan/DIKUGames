using System;

namespace Breakout.States;
public enum GameStateType {
    GameRunning,
    GamePaused,
    MainMenu
}

public class StateTransformer {
    /// <summary>
    /// </summary>
    /// <param name="a"></param>
    public static GameStateType TransformStringToState(string state) {
        switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            default:
                throw new ArgumentException("Invalid GameStateType string");
        }
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    public static string TransformStateToString(GameStateType state) {
        switch (state) {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            default:
                throw new ArgumentException("Invalid GameStateType");
        }
    }
}

