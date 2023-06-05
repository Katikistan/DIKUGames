using Breakout;
using Breakout.Collisions;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.Unit.PowerupTests;
[TestFixture]
public class PowerupCreatorTests {
    public Powerup lifeplus;
    public Health health;
    public EntityContainer<Powerup> powerups;
    public Player player;

    public PowerupCreatorTests() {
        CreateGL.CreateOpenGL();
    }
        [Test(), Repeat(30)]
        public void TestCreatePowerUp() {
            Vec2F pos = new Vec2F(0.5f, 0.5f);
            Powerup powerup = PowerUpCreator.CreatePowerUp(pos);
            Assert.That(powerup is not null);
            Assert.That(powerup is Powerup);
        }
}

