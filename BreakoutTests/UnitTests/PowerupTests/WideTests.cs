using Breakout;
using Breakout.Collisions;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.PowerupTests;
[TestFixture]
public class WideTests {
    public Powerup wide;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public WideTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]

    public void Setup() {

        health = new Health();
        wide = new Wide(new DynamicShape(
            new Vec2F(0.425f, 0.1f),
            new Vec2F(0.03f, 0.03f),
            new Vec2F(0.00f, -0.01f)));
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        powerups = new EntityContainer<Powerup>(5);
        powerups.AddEntity(wide);
    }
    [Test]
    public void WideTest() {
        Assert.That(player.Shape.Extent.X == 0.15f);
        player.ProcessEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "WIDE",
            StringArg1 = "START"
        });
        Assert.That(player.Shape.Extent.X == 0.3f);
        player.ProcessEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "WIDE",
            StringArg1 = "END"
            });
        Assert.That(player.Shape.Extent.X == 0.15f);
    }
    [Test]
    public void TestEffect() {
        Assert.That(player.MovementSpeed, Is.Not.EqualTo(0.02f));
        while (!PowerUpCollision.Collide(powerups, player)){
            powerups.Iterate(powerup => {
                powerup.Move();
            });
        }
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(player.Shape.Extent.X, Is.EqualTo(0.3f));
    }
}