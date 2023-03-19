using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
namespace Galaga.GalagaStates;
public class MainMenu : IGameState, IGameEventProcessor {
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
    public void ProcessEvent(GameEvent gameEvent) {
    }
    public void InitializeGameState() { //make text and backgroundhere
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        // if active button == start, then change text color
    }
    public void ResetState() {
        MainMenu.instance = new MainMenu();
        MainMenu.instance.InitializeGameState();
    }
    public void UpdateState() {
        GalagaBus.GetBus().ProcessEventsSequentially();
    }
    public void RenderState() {}
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {}
}