using Breakout;
using Breakout.Levels;
using Breakout.Players;
using Breakout.Powerups;
using Breakout.Collisions;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.Unit.PowerupTests;
[TestFixture]
public class SlimJimTests {
    public Powerup slimjim;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;
    public SlimJimTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]

    public void Setup() {
        health = new Health();
        slimjim = new SlimJim(new DynamicShape(
                new Vec2F(0.425f, 0.1f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        powerups = new EntityContainer<Powerup>(5);
        powerups.AddEntity(slimjim);

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
    [Test]
    public void TestSlimJimEffect() {
        Assert.That(player.Shape.Extent.X, Is.EqualTo(0.150f));

        while (!PowerUpCollision.Collide(powerups, player)){
            powerups.Iterate(powerup => {
                powerup.Move();
            });
        }
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(player.Shape.Extent.X, Is.EqualTo(0.075f));
    }
}