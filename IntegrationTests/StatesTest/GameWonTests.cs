using Breakout;
using Breakout.States;
using DIKUArcade.Events;
using DIKUArcade.Input;
namespace BreakoutTests.Integration.StatesTests;
[TestFixture]
public class GameWonTests {
    GameEvent changewon;
    GameEvent changemain;
    GameRunning gamerunning;
    MainMenu mainmenu;
    GamePaused gamePaused;
    GameWon gamewon;
    Health health;
    StateMachine statemachine;
    public GameWonTests() {
        CreateGL.CreateOpenGL();

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
        Assert.That(statemachine.ActiveState != GameWon.GetInstance());
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
    [Test]
    public void TestGameWonEnterKey() {
        gamewon.InitializeGameState();

        gamewon.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Up);
        gamewon.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Enter);

        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(statemachine.ActiveState, Is.EqualTo(MainMenu.GetInstance()));

        gamewon.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Down);
        gamewon.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Enter);
    }
}