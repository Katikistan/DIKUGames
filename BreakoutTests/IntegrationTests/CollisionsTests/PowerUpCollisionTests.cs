using Breakout.Collisions;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.Integration.CollisionTests;

[TestFixture]
public class PowerUpCollisionTests {
    private Player player;
    private Powerup powerup1;
    private Powerup powerup2;
    private EntityContainer<Powerup> powerups;
    public PowerUpCollisionTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup(){
        powerup1 = PowerUpCreator.CreatePowerUp(new Vec2F(0.5f, 0.1f));
        powerup2 = PowerUpCreator.CreatePowerUp(new Vec2F(0.5f, 0.5f));
        powerups = new EntityContainer<Powerup>(2);
        powerups.AddEntity(powerup1);
        powerups.AddEntity(powerup2);
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
    }
    [Test]
    public void TestCollision() {
        Assert.That(powerups.CountEntities(), Is.EqualTo(2));
        PowerUpCollision.Collide(powerups, player);
        Assert.That(powerups.CountEntities(), Is.Not.EqualTo(2));
    }
}