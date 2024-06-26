using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System.IO;
namespace Galaga.GalagaStates;
public class GamePaused : IGameState {
    private static GamePaused instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons;
    private Text PauseText;
    private int activeMenuButton;
    private int maxMenuButtons;
    private const int CONTINUE = 0;
    private const int MAIN_MENU = 1;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.InitializeGameState();
        }
        return GamePaused.instance;
    }

    public void InitializeGameState() {
        backGroundImage = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Galaga", "Assets", "Images", "SpaceBackground.png"))
            );
        maxMenuButtons = 2;
        activeMenuButton = CONTINUE;
        PauseText = new Text(
            "Paused",
            new Vec2F(0.375f, 0.05f),
            new Vec2F(0.7f, 0.7f)
            );
        menuButtons = new Text[maxMenuButtons];
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
    }
    public void ResetState() {
        GamePaused.instance = null;

    }
    public void UpdateState() {
    }
    public void RenderState() {
        backGroundImage.RenderEntity();
        Vec3I white = new Vec3I(255, 255, 255);
        Vec3I red = new Vec3I(255, 0, 0);
        PauseText.SetColor(white);
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
        PauseText.RenderText();
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
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "RESUME_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                } else {
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "MAIN_MENU"
                    });
                }
                break;
        }
    }

}
