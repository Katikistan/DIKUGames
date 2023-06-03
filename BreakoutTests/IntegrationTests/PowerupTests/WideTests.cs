using Breakout;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.Integration.PowerupTests;
[TestFixture]
public class WideTests {
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public WideTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]

    public void Setup() {
        health = new Health();
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));

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
}