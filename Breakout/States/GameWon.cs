using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;
namespace Breakout.States;
/// <summary>
///  A state for when the game is won
/// </summary>
public class GameWon : IGameState {
    private static GameWon instance = null;
    private Points points = null!;
    private Entity background;
    private Text[] menuButtons = new Text[2];
    private Text gameOverText;
    private Text pointsText;
    private int pointsValue;
    private int activeMenuButton;
    public int ActiveMenuButton {
        get => activeMenuButton;
    }
    private const int MAIN_MENU = 0;
    private const int QUIT = 1;
    private Vec3I white = new Vec3I(255, 255, 255);
    private Vec3I red = new Vec3I(255, 0, 0);
    /// <summary>
    ///  Gets or creates an instance of the GameWon state
    /// </summary>
    public static GameWon GetInstance() {
        if (GameWon.instance == null) {
            GameWon.instance = new GameWon();
            GameWon.instance.InitializeGameState();
        }
        return GameWon.instance;
    }
    /// <summary>
    ///  Inizializes the Game state, this functions as a constructor for the state
    /// </summary>
    public void InitializeGameState() {
        points = Points.getInstance();
        pointsValue = points.GetPoints();

        background = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Breakout", "Assets", "Images", "SpaceBackground.png")));

        gameOverText = new Text(
            "Game Won",
            new Vec2F(0.30f, 0.17f),
            new Vec2F(0.7f, 0.7f)
            );
        pointsText = new Text(
            $"Points: {pointsValue}",
            new Vec2F(0.41f, 0.32f),
            new Vec2F(0.4f, 0.4f)
        );
        menuButtons[MAIN_MENU] = new Text(
            "Main Menu",
            new Vec2F(0.39f, 0.1f),
            new Vec2F(0.4f, 0.4f)
            );

        menuButtons[QUIT] = new Text(
            "Quit game",
            new Vec2F(0.4f, 0f),
            new Vec2F(0.4f, 0.4f)
            );
        gameOverText.SetColor(white);
        pointsText.SetColor(white);
        activeMenuButton = MAIN_MENU;
        menuButtons[MAIN_MENU].SetColor(red);
        menuButtons[QUIT].SetColor(white);
        points = Points.getInstance();
    }
    /// <summary>
    /// Resets the state
    /// </summary>
    public void ResetState() {
        GameWon.instance = null;
    }
    /// <summary>
    ///  Updates the state, this an empty method
    /// </summary>
    public void UpdateState() {
    }
    /// <summary>
    ///  Renders objects in the state
    /// </summary>
    public void RenderState() {
        background.RenderEntity();
        gameOverText.RenderText();
        menuButtons[QUIT].RenderText();
        menuButtons[MAIN_MENU].RenderText();
        pointsText.RenderText();
    }
    /// <summary>
    ///  Handles key input events such as key presses and key realising
    /// </summary>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            KeyPress(key);
        }
    }
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Up:
                activeMenuButton = MAIN_MENU;
                menuButtons[MAIN_MENU].SetColor(red);
                menuButtons[QUIT].SetColor(white);
                break;
            case KeyboardKey.Down:
                activeMenuButton = QUIT;
                menuButtons[QUIT].SetColor(red);
                menuButtons[MAIN_MENU].SetColor(white);
                break;
            case KeyboardKey.Enter:
                if (ActiveMenuButton == MAIN_MENU) {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "MAIN_MENU"
                    });
                } else {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        Message = "CLOSE_GAME",
                        StringArg1 = "WINDOW CLOSE"
                    });
                }
                break;
        }
    }
}
