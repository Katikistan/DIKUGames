using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
namespace galagaTests;

// Any relevant methods and their effect on
// the enrage-state of the Enemy class should
// be tested.
public class TestEnemy {
    private List<Image> enemyStride;
    private Enemy enemy;
    [SetUp]
    public void Setup() {
        enemyStride = ImageStride.CreateStrides
        (4, Path.Combine("Assets", "Images", "BlueMonster.png"));

        ImageStride blueMonster = new ImageStride(80, enemyStride);
        enemy = new Enemy (
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            blueMonster);
    }
    // Test the enraged state of enemy
    [Test]
    public void enragedTest() {
        enemy.IsEnemyDead();
        Assert.AreEqual(enemy.Speed, 0.002f);
    }

    // Test if the enemy dies
    [Test]
    public void deathTest() {
        enemy.IsEnemyDead();
        enemy.IsEnemyDead();
        enemy.IsEnemyDead();
        Assert.AreEqual(enemy.IsEnemyDead(), true);
    }
}