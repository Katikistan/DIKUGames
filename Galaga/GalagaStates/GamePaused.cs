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
        activeMenuButton = 0;
        PauseText = new Text(
            "Paused",
            new Vec2F(0.375f, 0.4f),
            new Vec2F(0.4f, 0.4f)
            );
        menuButtons = new Text[maxMenuButtons];
        menuButtons[0] = new Text (
            "Continue",
            new Vec2F(0.375f, 0.3f),
            new Vec2F(0.4f, 0.4f)
            );
        menuButtons[1] = new Text (
            "Main Menu",
            new Vec2F(0.4f, 0.2f),
            new Vec2F(0.4f, 0.4f)
            );
    }
    public void ResetState() {
        instance = new GamePaused();
        instance.InitializeGameState();
    }
    public void UpdateState() {
    }
    public void RenderState() {
        backGroundImage.RenderEntity();
        Vec3I white = new Vec3I(255, 255, 255);
        Vec3I red = new Vec3I(255,0,0);
        PauseText.SetColor(white);
        switch(activeMenuButton) {
            case(0):
                menuButtons[0].SetColor(red);
                menuButtons[1].SetColor(white);
                break;
            case(1):
                menuButtons[0].SetColor(white);
                menuButtons[1].SetColor(red);
                break;
        }
        PauseText.RenderText();
        menuButtons[0].RenderText();
        menuButtons[1].RenderText();
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
                KeyPress(key);
        }
    }
    private void KeyPress(KeyboardKey key) { // When a key is pressed
        switch (key) {
            case KeyboardKey.Up:
                activeMenuButton = 0;
                break;
            case KeyboardKey.Down:
                activeMenuButton = 1;
                break;
            case KeyboardKey.Enter:
                if (activeMenuButton == 0) {
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                        });
                } else {
                    activeMenuButton = 0;
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
