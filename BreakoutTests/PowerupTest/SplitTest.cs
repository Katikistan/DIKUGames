using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.States;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout.Collisions;
using Breakout.Powerups;
using Breakout;
using Breakout.Levels;
using Breakout.Balls;
namespace BreakoutTests.CollisionTests;

[TestFixture]
public class SplitTest {
    public Ball ball;
    public Powerup split;
    public EntityContainer<Powerup> powerups;
    public EntityContainer<Ball> balls;

    public Player player;
    public LevelManager levelmanager;
    public SplitTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]

    public void Setup() {
        levelmanager = new LevelManager();
        ball = BallCreator.CreateBall(new Vec2F(0.45f, 0.2f), new Vec2F(0.001f, 0.015f));
        split = new LifeLoss(new DynamicShape(
                new Vec2F(0.425f, 0.1f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        powerups = new EntityContainer<Powerup>(5);
        powerups.AddEntity(split);

    }
    [Test]
    public void TestSplit() {
        Assert.That(levelmanager.Balls.CountEntities() == 0);
        levelmanager.Balls.AddEntity(ball);
        Assert.That(levelmanager.Balls.CountEntities() == 1);
        // Testing if lifeplus powerup collides with player and changes health
        while (!PowerUpCollision.Collide(powerups, player)) {
            powerups.Iterate(powerup => {
                powerup.Move();
            });
        }  
        levelmanager.ProcessEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "SPLIT",
        });
        Assert.That(levelmanager.Balls.CountEntities() == 3);
    }
}