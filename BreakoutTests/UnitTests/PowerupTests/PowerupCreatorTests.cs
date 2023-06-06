using Breakout.Powerups;
using DIKUArcade.Math;
namespace BreakoutTests.Integration.PowerupTests;
[TestFixture]
public class PowerupCreatorTests {
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

