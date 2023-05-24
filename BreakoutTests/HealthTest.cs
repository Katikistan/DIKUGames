using Breakout.Levels;
using Breakout;
using Breakout.States;
using DIKUArcade.Events;

namespace BreakoutTests.LevelLoading;

[TestFixture]
public class HealthTest {
    GameRunning gamerunning;
    MainMenu mainmenu;
    GamePaused gamePaused;
    GameLost gamelost;
    Health health;
    StateMachine statemachine;
    StateTransformer stateTransformer;
    public HealthTest() {
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
    public void TestLoseHealth() {
        Assert.That(health.health == 3);
        health.LoseHealth();
        Assert.That(health.health == 2);
        health.LoseHealth();
        Assert.That(health.health == 1);
        Assert.That(statemachine.ActiveState != GameLost.GetInstance());
        health.LoseHealth();
        Assert.That(health.health == 0);
        health.LoseHealth();
        Assert.That(health.health == 0);
        BreakoutBus.GetBus().ProcessEvents();
        //ved ikke hvad der skal gøres for at statemachinen reageree
        Assert.That(statemachine.ActiveState == GameLost.GetInstance());
    }
    [Test]
    public void TestGainHealth() {
        Assert.That(health.health == 3);
        health.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET HEALTH",
            IntArg1 = 1
        });
        Assert.That(health.health == 4);

    }
}