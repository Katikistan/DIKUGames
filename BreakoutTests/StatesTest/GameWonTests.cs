using DIKUArcade.Events;
using DIKUArcade.Input;
namespace Breakout.States;
[TestFixture]
public class GameWonTest {
    GameEvent changewon;
    GameEvent changemain;
    GameRunning gamerunning;
    MainMenu mainmenu;
    GamePaused gamePaused;
    GameWon gamewon;
    Health health;
    StateMachine statemachine;
    public GameWonTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();

    }
    [SetUp]
    public void Setup() {
        gamerunning = new GameRunning();
        mainmenu = new MainMenu();
        gamePaused = new GamePaused();
        gamewon = new GameWon();
        statemachine = new StateMachine();
        health = new Health();
        changewon = (new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_WON"
                    });
        changemain = (new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "MAIN_MENU"
                    });
    }
    [Test]
    public void TestSwitchGameWon() {
        Assert.That(statemachine.ActiveState != GameLost.GetInstance());
        statemachine.ProcessEvent(changewon);
        //ved ikke hvad der skal gøres for at statemachinen reageree
        Assert.That(statemachine.ActiveState == GameWon.GetInstance());
    }


    [Test]
    public void TestGameWon() {
        gamewon.InitializeGameState();
        gamewon.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Up);
        Assert.That(gamewon.ActiveMenuButton == 0);
        gamewon.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Down);
        Assert.That(gamewon.ActiveMenuButton == 1);

    }
}