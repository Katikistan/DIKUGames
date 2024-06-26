using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System.IO;
namespace Galaga.GalagaStates;
public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;
    private const int NEW_GAME = 0;
    private const int QUIT = 1;
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.InitializeGameState();
        }
        return MainMenu.instance;
    }

    public void InitializeGameState() {
        backGroundImage = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Galaga", "Assets", "Images", "TitleImage.png")));
        maxMenuButtons = 2;
        activeMenuButton = NEW_GAME;
        menuButtons = new Text[maxMenuButtons];
        menuButtons[NEW_GAME] = new Text("New Game",
            new Vec2F(0.375f, 0.2f),
            new Vec2F(0.4f, 0.4f));
        menuButtons[QUIT] = new Text("Quit",
            new Vec2F(0.46f, 0.1f),
            new Vec2F(0.4f, 0.4f));
    }
    public void ResetState() {
        MainMenu.instance = null;
    }
    public void UpdateState() {
    }
    public void RenderState() {
        backGroundImage.RenderEntity();
        Vec3I white = new Vec3I(255, 255, 255);
        Vec3I red = new Vec3I(255, 0, 0);
        switch (activeMenuButton) {
            case (NEW_GAME):
                menuButtons[NEW_GAME].SetColor(red);
                menuButtons[QUIT].SetColor(white);
                break;
            case (QUIT):
                menuButtons[NEW_GAME].SetColor(white);
                menuButtons[QUIT].SetColor(red);
                break;
        }
        menuButtons[NEW_GAME].RenderText();
        menuButtons[QUIT].RenderText();
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            KeyPress(key);
        }
    }
    private void KeyPress(KeyboardKey key) { 
        switch (key) {
            case KeyboardKey.Up:
                activeMenuButton = NEW_GAME;
                break;
            case KeyboardKey.Down:
                activeMenuButton = QUIT;
                break;
            case KeyboardKey.Enter:
                if (activeMenuButton == NEW_GAME) {
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                } else {
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        Message = "CLOSE_GAME",
                        StringArg1 = "WINDOW_CLOSE"
                    });
                }
                break;
        }
    }

}
