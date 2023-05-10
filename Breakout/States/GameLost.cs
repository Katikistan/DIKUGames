using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;

namespace Breakout.States;
public class GameLost : IGameState {
    private static GameLost instance = null;
    private Entity background;
    private Text[] menuButtons;
    private Text GameOverText;
    private int activeMenuButton;
    private int maxMenuButtons;
    private const int MAIN_MENU = 0;
    private const int QUIT = 1;
    public static GameLost GetInstance() {
        if (GameLost.instance == null) {
            GameLost.instance = new GameLost();
            GameLost.instance.InitializeGameState();
        }
        return GameLost.instance;
    }
    public void InitializeGameState() {
        background = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Breakout", "Assets", "Images", "SpaceBackground.png")));
        maxMenuButtons = 2;
        activeMenuButton = MAIN_MENU;
        GameOverText = new Text(
            "Game over",
            new Vec2F(0.375f, 0.05f),
            new Vec2F(0.7f, 0.7f)
            );
        menuButtons = new Text[maxMenuButtons];
        menuButtons[MAIN_MENU] = new Text(
            "Main Menu",
            new Vec2F(0.42f, 0.2f),
            new Vec2F(0.4f, 0.4f)
            );
        menuButtons[QUIT] = new Text(
            "Quit game",
            new Vec2F(0.4f, 0.1f),
            new Vec2F(0.4f, 0.4f)
            );
    }
    public void ResetState() {
        GameLost.instance = null;

    }
    public void UpdateState() {
    }
    public void RenderState() {
        background.RenderEntity();
        Vec3I white = new Vec3I(255, 255, 255);
        Vec3I red = new Vec3I(255, 0, 0);
        GameOverText.SetColor(white);
        switch (activeMenuButton) {
            case (QUIT):
                menuButtons[QUIT].SetColor(red);
                menuButtons[MAIN_MENU].SetColor(white);
                break;
            case (MAIN_MENU):
                menuButtons[QUIT].SetColor(white);
                menuButtons[MAIN_MENU].SetColor(red);
                break;
        }
        GameOverText.RenderText();
        menuButtons[QUIT].RenderText();
        menuButtons[MAIN_MENU].RenderText();
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            KeyPress(key);
        }
    }
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Up:
                activeMenuButton = MAIN_MENU;
                break;
            case KeyboardKey.Down:
                activeMenuButton = QUIT;
                break;
            case KeyboardKey.Enter:
                if (activeMenuButton == MAIN_MENU) {
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
