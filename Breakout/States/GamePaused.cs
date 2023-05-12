using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;

namespace Breakout.States;
public class GamePaused : IGameState {
    private static GamePaused instance = null;
    private Entity background;
    public Entity Background {
        get { return background; }
    }
    private Text[] menuButtons = new Text[2];
    private Text pauseText;
    public Text PauseText {
        get { return pauseText; }
    }
    private int activeMenuButton;
    public int ActiveMenuButton {
        get { return activeMenuButton; }
    }
    private const int CONTINUE = 0;
    private const int MAIN_MENU = 1;
    private Vec3I white = new Vec3I(255, 255, 255);
    private Vec3I red = new Vec3I(255, 0, 0);
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.InitializeGameState();
        }
        return GamePaused.instance;
    }
    public void InitializeGameState() {
        background = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Breakout", "Assets", "Images", "SpaceBackground.png")));
        pauseText = new Text(
            "Paused",
            new Vec2F(0.375f, 0.05f),
            new Vec2F(0.7f, 0.7f)
            );
        activeMenuButton = CONTINUE;
        menuButtons[CONTINUE] = new Text(
            "Continue",
            new Vec2F(0.42f, 0.2f),
            new Vec2F(0.4f, 0.4f)
            );
        menuButtons[MAIN_MENU] = new Text(
            "Main Menu",
            new Vec2F(0.4f, 0.1f),
            new Vec2F(0.4f, 0.4f)
            );
        pauseText.SetColor(white);
    }
    public void ResetState() {
        GamePaused.instance = null;
    }
    public void UpdateState() {
    }
    public void RenderState() {
        background.RenderEntity();
        switch (activeMenuButton) {
            case (CONTINUE):
                menuButtons[CONTINUE].SetColor(red);
                menuButtons[MAIN_MENU].SetColor(white);
                break;
            case (MAIN_MENU):
                menuButtons[CONTINUE].SetColor(white);
                menuButtons[MAIN_MENU].SetColor(red);
                break;
        }
        pauseText.RenderText();
        menuButtons[CONTINUE].RenderText();
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
                activeMenuButton = CONTINUE;
                break;
            case KeyboardKey.Down:
                activeMenuButton = MAIN_MENU;
                break;
            case KeyboardKey.Enter:
                if (activeMenuButton == CONTINUE) {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "RESUME_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                } else {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "MAIN_MENU"
                    });
                }
                break;
        }
    }

}
