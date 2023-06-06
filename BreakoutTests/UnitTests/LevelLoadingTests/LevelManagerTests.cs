using Breakout;
using Breakout.Levels;
using Breakout.Powerups;
using Breakout.States;
using DIKUArcade.Events;
using DIKUArcade.Math;
namespace BreakoutTests.Unit.LevelLoadingTests;
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
    public void TestEmptyLevel() {
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("nolevel.txt");
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("level1.txt");
        Assert.That(levelManager.EmptyLevel(), Is.False);
    }
    [Test]
    public void TestNewLevel() {
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(0));
        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(0));
        levelManager.NewLevel("nolevel.txt");
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(0));
        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(1));
        // ball with no blocks beacuse level isnt valid
        Assert.That(levelManager.EmptyLevel(), Is.True);
        levelManager.NewLevel("level1.txt");
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(76));
        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(1));
        Assert.That(levelManager.EmptyLevel(), Is.False);
    }
    [Test]
    public void TestProcessEvent() {
        Assert.That(levelManager.HardBalls, Is.False);
        levelManager.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "HARD BALL",
            StringArg1 = "START"
        });
        Assert.That(levelManager.HardBalls, Is.True);

        levelManager.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "HARD BALL",
            StringArg1 = "END"
        });
        Assert.That(levelManager.HardBalls, Is.False);

        levelManager.NewLevel("level1.txt");
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(76));
        levelManager.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "CLEAR",
        });
        Assert.That(levelManager.Blocks.CountEntities(), Is.EqualTo(0));

        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(1));
        levelManager.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "SPLIT",
        });
        Assert.That(levelManager.Balls.CountEntities(), Is.EqualTo(3));

        Assert.That(levelManager.Powerups.CountEntities(), Is.EqualTo(0));
        levelManager.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "SPAWN POWERUP",
            ObjectArg1 = (object) (new Vec2F(0.425f, 0.1f))
        });
        Assert.That(levelManager.Powerups.CountEntities(), Is.EqualTo(1));
    }
    [Test]
    public void TestPowerupMove() {
        levelManager.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "SPAWN POWERUP",
            ObjectArg1 = (object) (new Vec2F(0.425f, 0.1f))
        });
        Assert.That(levelManager.Powerups.CountEntities(), Is.EqualTo(1));
        foreach (Powerup powerup in levelManager.Powerups) {
            Assert.That(powerup.Shape.Position.X, Is.EqualTo(0.425f));
            Assert.That(powerup.Shape.Position.Y, Is.EqualTo(0.1f));
        }
        levelManager.Update();
        foreach (Powerup powerup in levelManager.Powerups) {
            Assert.That(powerup.Shape.Position.X, Is.EqualTo(0.425F));
            Assert.That(powerup.Shape.Position.Y, Is.EqualTo(0.09f));
        }
    }
}