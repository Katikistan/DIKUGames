using Breakout;
using Breakout.States;
using DIKUArcade.Events;
namespace BreakoutTests.Unit;
[TestFixture]
public class HealthTests {
    GameRunning gamerunning;
    MainMenu mainmenu;
    GamePaused gamePaused;
    GameLost gamelost;
    Health health;
    StateMachine statemachine;
    public HealthTests() {
        CreateGL.CreateOpenGL();
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
        Assert.That(health._Health == 3);
        health.LoseHealth();
        Assert.That(health._Health == 2);
        health.LoseHealth();
        Assert.That(health._Health == 1);
        Assert.That(statemachine.ActiveState != GameLost.GetInstance());
        health.LoseHealth();
        Assert.That(health._Health == 0);
        health.LoseHealth();
        Assert.That(health._Health == 0);
        BreakoutBus.GetBus().ProcessEvents();
        
        Assert.That(statemachine.ActiveState == GameLost.GetInstance());
    }
    [Test]
    public void TestGainHealth() {
        Assert.That(health._Health == 3);
        health.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET HEALTH",
            IntArg1 = 1
        });
        Assert.That(health._Health == 4);

    }
}