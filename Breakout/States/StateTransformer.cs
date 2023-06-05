using System;
namespace Breakout.States;
/// <summary>
///  A class used to transform strings into state tyopes.
/// </summary>
public class StateTransformer {
    /// <summary>
    ///  Transforms strings into state types.
    /// </summary>
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

