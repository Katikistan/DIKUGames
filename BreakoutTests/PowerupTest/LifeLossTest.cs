using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.States;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout.Collisions;
using Breakout.Powerups;
using Breakout;
namespace BreakoutTests.CollisionTests;

[TestFixture]
public class LifeLossTest {
    public Powerup lifeloss;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public LifeLossTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]

    public void Setup() {
        health = new Health();
        lifeloss = new LifeLoss(new DynamicShape(
                new Vec2F(0.425f, 0.1f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        powerups = new EntityContainer<Powerup>(5);
        powerups.AddEntity(lifeloss);

    }
    [Test]
    public void TestLifeLoss() {
        Assert.That(health._Health, Is.EqualTo(3));
        Assert.That(powerups.CountEntities(), Is.EqualTo(1));
        // Testing if lifeplus powerup collides with player and changes health
        while (health._Health == 3) {
            if (!PowerUpCollision.Collide(powerups, player)) {
                powerups.Iterate(powerup => {
                    powerup.Move();
                });
            } else {
                Assert.That(health._Health == 3);
                health.ProcessEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "LOSE HEALTH",
                    IntArg1 = 1
                });
            }
        }
        Assert.That(health._Health, Is.EqualTo(2));
        Assert.That(powerups.CountEntities(), Is.EqualTo(0));
    }
}