using Breakout;
using Breakout.Levels;
using Breakout.States;
using Breakout.Powerups;
using DIKUArcade.Events;
using DIKUArcade.Math;
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
    [Test]
    public void TestRemovePowerUp() {
        Assert.That(levelManager.Powerups.CountEntities(), Is.EqualTo(0));
        // Adding an entity to the powerups entity container
        levelManager.Powerups.AddEntity(PowerUpCreator.CreatePowerUp(new Vec2F(0.425f, 0.1f)));
        Assert.That(levelManager.Powerups.CountEntities(), Is.EqualTo(1));
        // Moving the powerup until it collides with the player
        while (levelManager.Powerups.CountEntities() != 0) {
            levelManager.Update();
        }
        Assert.That(levelManager.Powerups.CountEntities(), Is.EqualTo(0));
    }
}