using Breakout;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Math;
namespace BreakoutTests.Unit.PowerupTests;
[TestFixture]
public class PowerUpTests {
    public Powerup powerup;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public PowerUpTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]

    public void Setup() {
        powerup = new LifePlus(new DynamicShape(
                new Vec2F(0.425f, 0.1f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        powerups = new EntityContainer<Powerup>(5);
        powerups.AddEntity(powerup);
    }
    [Test]
    public void PowerUpTest() {
        Assert.That(powerups.CountEntities() == 1);
        Assert.That(powerup.Shape.Position.Y == 0.1f);

        powerup.Move();
        Assert.That(powerup.Shape.Position.Y == 0.09f);

        powerup.Shape.Position.Y = 0.01f;
        powerup.Move();
        powerup.Move();
        powerup.Move();
        powerups.Iterate(powerup => {
            powerup.Move();
        });
        Assert.That(powerups.CountEntities() == 0);
    }
}