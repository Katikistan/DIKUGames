using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;
namespace Breakout.States;
/// <summary>
///  A main menu state
/// </summary>
public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private Entity backGround;
    public Entity BackGround {
        get => backGround;
    }
    private Text[] menuButtons = new Text[2];
    private int activeMenuButton;
    public int ActiveMenuButton {
        get => activeMenuButton;
    }
    private const int NEW_GAME = 0;
    private const int QUIT = 1;
    private Vec3I white = new Vec3I(255, 255, 255);
    private Vec3I red = new Vec3I(255, 0, 0);
    /// <summary>
    ///  Gets or creates an instance of the MainMenu state
    /// </summary>
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.InitializeGameState();
        }
        return MainMenu.instance;
    }
    /// <summary>
    ///  Inizializes the Game state, this functions as a constructor for the state
    /// </summary>
    public void InitializeGameState() {
        backGround = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Breakout", "Assets", "Images", "BreakoutTitleScreen.png")));
        menuButtons[NEW_GAME] = new Text("New Game",
            new Vec2F(0.375f, 0.2f),
            new Vec2F(0.4f, 0.4f));
        menuButtons[QUIT] = new Text("Quit",
            new Vec2F(0.46f, 0.1f),
            new Vec2F(0.4f, 0.4f));
        activeMenuButton = NEW_GAME;
        menuButtons[NEW_GAME].SetColor(red);
        menuButtons[QUIT].SetColor(white);
        Points.GetInstance().ResetPoints();
    }
    /// <summary>
    /// Resets the state
    /// </summary>
    public void ResetState() {
        MainMenu.instance = null;
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
        backGround.RenderEntity();
        menuButtons[NEW_GAME].RenderText();
        menuButtons[QUIT].RenderText();
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
                activeMenuButton = NEW_GAME;
                menuButtons[NEW_GAME].SetColor(red);
                menuButtons[QUIT].SetColor(white);
                break;
            case KeyboardKey.Down:
                activeMenuButton = QUIT;
                menuButtons[NEW_GAME].SetColor(white);
                menuButtons[QUIT].SetColor(red);
                break;
            case KeyboardKey.Enter:
                if (activeMenuButton == NEW_GAME) {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
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
