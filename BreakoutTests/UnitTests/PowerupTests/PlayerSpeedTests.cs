using Breakout;
using Breakout.Collisions;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.PowerupTests;
[TestFixture]
public class PlayerSpeedTests {
    public Powerup playerSpeed;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public PlayerSpeedTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        health = new Health();
        playerSpeed = new PlayerSpeed(new DynamicShape(
                new Vec2F(0.425f, 0.1f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        powerups = new EntityContainer<Powerup>(5);
        powerups.AddEntity(playerSpeed);

    }
    [Test]
    public void PlayerSpeedTest() {
        Assert.That(player.MovementSpeed != 0.02f);
        player.ProcessEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SPEED",
            StringArg1 = "START"
        });
        Assert.That(player.MovementSpeed == 0.02f);
        player.ProcessEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SPEED",
            StringArg1 = "END"
        });
        Assert.That(player.MovementSpeed != 0.02f);
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
        Assert.That(player.MovementSpeed, Is.EqualTo(0.02f));
    }
}