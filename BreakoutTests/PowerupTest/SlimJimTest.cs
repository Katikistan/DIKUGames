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
public class SlimJimTestt {
    public Powerup lifeloss;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public SlimJimTestt() {
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
    public void SlimJimTest() {
        Assert.That(player.Shape.Extent.X != 0.075f);
        player.ProcessEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SLIM JIM",
            StringArg1 = "START"
        });
        Assert.That(player.Shape.Extent.X == 0.075f);
        player.ProcessEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SLIM JIM",
            StringArg1 = "END"
        });
        Assert.That(player.Shape.Extent.X != 0.075f);
    }
}