using Breakout;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.PowerupTests;
[TestFixture]
public class SlimJimTests {
    public Powerup lifeloss;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public SlimJimTests() {
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