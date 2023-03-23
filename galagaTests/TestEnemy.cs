using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
namespace galagaTests;
[TestFixture]
public class TestEnemy {
    private List<Image> enemyStride = null!;
    private Enemy enemy = null!;
    public TestEnemy() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        enemyStride = ImageStride.CreateStrides
        (4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));

        ImageStride blueMonster = new ImageStride(80, enemyStride);
        enemy = new Enemy(
            new DynamicShape
            (new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            blueMonster
            );
    }
    // Test the enraged state of enemy
    [Test]
    public void enragedTest() {
        enemy.LoseHP();
        Assert.That(enemy.Speed, Is.EqualTo(0.002f));
    }

    // Test if the enemy dies
    [Test]
    public void deathTest() {
        enemy.LoseHP();
        enemy.LoseHP();
        enemy.LoseHP();
        Assert.That(enemy.IsDead(), Is.EqualTo(true));
    }

    // Test the IncreaseSpeed method
    [Test]
    public void increaseSpeedTest() {
        enemy.IncreaseSpeed(0.001f);
        Assert.That(enemy.Speed, Is.EqualTo(0.002f));

    }
}