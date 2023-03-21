using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System.IO;
namespace Galaga.GalagaStates;
public class MainMenu : IGameState/*, IGameEventProcessor*/ {
    private static MainMenu instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.InitializeGameState();
        }
        return MainMenu.instance;
    }

    public void InitializeGameState() { //make text and backgroundhere
        // GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        // GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        // GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        // if active button == start, then change text color
        backGroundImage = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f), 
                new Vec2F(1.0f, 1.0f)), 
                new Image(Path.Combine(
                "..", "Galaga", "Assets", "Images", "TitleImage.png")));
        maxMenuButtons = 2;
        activeMenuButton = 0;
        menuButtons = new Text[maxMenuButtons];
        menuButtons[0] = new Text ("New Game", 
            new Vec2F(0.375f, 0.3f),
            new Vec2F(0.4f, 0.4f));
        menuButtons[1] = new Text ("Quit", 
            new Vec2F(0.375f, 0.3f),
            new Vec2F(0.4f, 0.4f));
    }
    // public void ProcessEvent(GameEvent gameEvent) {
    //     if (gameEvent.StringArg1 == "GAME_RUNNING") {

    //     }
    //     if (gameEvent.StringArg1 == "GAME_RUNNING") {
            
    //     }
    // }

    public void ResetState() {
        instance = new MainMenu();
        instance.InitializeGameState();        
    }
    public void UpdateState() {
    }
    public void RenderState() {
        backGroundImage.RenderEntity();
        Vec3I white = new Vec3I(255, 255, 255);
        Vec3I red = new Vec3I(255,0,0);
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
        menuButtons[0].RenderText();
        menuButtons[1].RenderText();
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress)
            switch(key) {
                case KeyboardKey.Up:
                    activeMenuButton = 0;
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = 1;
                    break;
                case KeyboardKey.Enter:
                    if (activeMenuButton == 0) {
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent{
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_RUNNING"
                            });
                    } else {
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent {
                            EventType = GameEventType.WindowEvent,
                            Message = "CLOSE_GAME",
                            StringArg1 = "WINDOW_CLOSE"
                            });
                    }
                    break;
            }
    }
}
