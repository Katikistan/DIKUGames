using Breakout;
using Breakout.States;
using DIKUArcade.Events;
using DIKUArcade.Input;
namespace BreakoutTests.Integration.StatesTests;
[TestFixture]
public class GameLostTests {
    GameRunning gamerunning;
    MainMenu mainmenu;
    GamePaused gamePaused;
    GameLost gamelost;
    Health health;
    StateMachine statemachine;
    public GameLostTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        gamerunning = new GameRunning();
        mainmenu = new MainMenu();
        gamePaused = new GamePaused();
        gamelost = new GameLost();
        statemachine = new StateMachine();
        health = new Health();
    }
    [Test]
    public void TestSwitchGameLost() {
        Assert.That(statemachine.ActiveState != GameLost.GetInstance());
        statemachine.ProcessEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_LOST"
                    });
        Assert.That(statemachine.ActiveState == GameLost.GetInstance());
    }
    [Test]
    public void TestGameLost() {
        gamelost.InitializeGameState();
        gamelost.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Up);
        Assert.That(gamelost.ActiveMenuButton == 0);
        gamelost.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Down);
        Assert.That(gamelost.ActiveMenuButton == 1);
    }
}