using Breakout;
using Breakout.Levels;
using Breakout.States;
using DIKUArcade.Events;
namespace BreakoutTests.Integration.LevelLoadingTests;
[TestFixture]
public class LevelManagerTests {
    LevelManager levelManager;
    StateMachine stateMachine;
    public LevelManagerTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        levelManager = new LevelManager();
        stateMachine = new StateMachine();
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, levelManager);
    }
    [Test]
    public void TestNoTimeLeft() {
        levelManager.LevelTimer.SetTime(0);
        levelManager.Update();
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That((stateMachine.ActiveState).GetType(), Is.EqualTo((new GameLost()).GetType()));
    }
}