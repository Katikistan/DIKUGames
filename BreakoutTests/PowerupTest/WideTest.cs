using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.States;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout.Collisions;
using Breakout.Powerups;
using Breakout;
using DIKUArcade.Timers;
namespace BreakoutTests.CollisionTests;

[TestFixture]
public class WideTestt {
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public WideTestt() {
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