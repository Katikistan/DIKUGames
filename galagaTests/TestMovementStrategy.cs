using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
using Galaga.MovementStrategy;
namespace galagaTests;
[TestFixture]
public class TestMovementStrategy {
    private Enemy enemy1 = null!;
    private Enemy enemy2 = null!;
    private List<Image> enemyStride = null!;
    EntityContainer<Enemy> enemies = null!;
    [SetUp]
    public void Setup() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        enemyStride = ImageStride.CreateStrides
        (4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
        ImageStride blueMonster = new ImageStride(80, enemyStride);
        enemy1 = new Enemy (
            new DynamicShape(new Vec2F(0.3f, 0.1f), new Vec2F(0.1f, 0.1f)),
            blueMonster
            );
        enemy2 = new Enemy (
            new DynamicShape(new Vec2F(0.6f, 0.1f), new Vec2F(0.1f, 0.1f)),
            blueMonster
            );
        enemies = new EntityContainer<Enemy>(2);
    }
    // Test of NoMove of an enemy
    [Test]
    public void NoMoveTest() {
        IMovementStrategy movestrat = new NoMove();
        // Move the enemy
        movestrat.MoveEnemy(enemy1);
        // Store position after movement in a variable
        Vec2F enemy1pos = new Vec2F(enemy1._Shape.Position.X, enemy1._Shape.Position.Y);
        // Position should be equal to the starting position
        Assert.That(enemy1pos.X,Is.EqualTo(enemy1.Startpos.X));
        Assert.That(enemy1pos.Y,Is.EqualTo(enemy1.Startpos.Y));
    }
    // Test of Down on an enemy
    [Test]
    public void DownTest() {
        IMovementStrategy movestrat = new Down();
        // Move the enemy
        movestrat.MoveEnemy(enemy1);
        // Store position after movement in a variable
        Vec2F enemy1pos = new Vec2F(enemy1._Shape.Position.X, enemy1._Shape.Position.Y);
        // Position should be moved by one Speed down
        Assert.That(enemy1pos.X,Is.EqualTo(enemy1.Startpos.X));
        Assert.That(enemy1pos.Y,Is.EqualTo(enemy1.Startpos.Y - enemy1.Speed));
    }
    // Test of ZigZagDown on an enemy
    [Test]
    public void ZigZagDownTest() {
        IMovementStrategy movestrat = new ZigZagDown();
        float s = 0.0003f;
        float yi = enemy1._Shape.Position.Y + s;
        // Move the enemy
        movestrat.MoveEnemy(enemy1);
        // Store position after movement in a variable
        Vec2F enemy1pos = new Vec2F(enemy1._Shape.Position.X, enemy1._Shape.Position.Y);
        // Position should be moved by defined formula
        float a = 0.05f;
        float p = 0.045f;
        float pi = (float)Math.PI;
        float x0 = enemy1.Startpos.X;
        float y0 = enemy1.Startpos.Y;
        float sin = (float)Math.Sin((2 * pi *(y0-yi))/p);
        Assert.That(enemy1pos.X,Is.EqualTo(x0 + a * sin));
        Assert.That(enemy1pos.Y,Is.EqualTo(enemy1.Startpos.Y - enemy1.Speed));

    }
    // Test of MoveEnemies with NoMove
    [Test]
    public void NoMoveEnemies() {
        // Add entities to enemies
        enemies.AddEntity(enemy1);
        enemies.AddEntity(enemy2);
        // NoMove movement strategy
        IMovementStrategy movestrat = new NoMove();
        // Move enemies
        movestrat.MoveEnemies(enemies);
        // Store position after movement in a variable
        Vec2F enemy1pos = new Vec2F(enemy1._Shape.Position.X, enemy1._Shape.Position.Y);
        Vec2F enemy2pos = new Vec2F(enemy2._Shape.Position.X, enemy2._Shape.Position.Y);
        // Position should be moved by one Speed down
        Assert.That(enemy1pos.X,Is.EqualTo(enemy1.Startpos.X));
        Assert.That(enemy1pos.Y,Is.EqualTo(enemy1.Startpos.Y));
        Assert.That(enemy2pos.X,Is.EqualTo(enemy2.Startpos.X));
        Assert.That(enemy2pos.Y,Is.EqualTo(enemy2.Startpos.Y));
    }
    // Test of MoveEnemies with Down
    [Test]
    public void MoveEnemiesDown() {
        // Add entities to enemies
        enemies.AddEntity(enemy1);
        enemies.AddEntity(enemy2);
        // Down movement strategy
        IMovementStrategy movestrat = new Down();
        // Move enemies
        movestrat.MoveEnemies(enemies);
        // Store position after movement in a variable
        Vec2F enemy1pos = new Vec2F(enemy1._Shape.Position.X, enemy1._Shape.Position.Y);
        Vec2F enemy2pos = new Vec2F(enemy2._Shape.Position.X, enemy2._Shape.Position.Y);
        // Position should be moved by one Speed down
        Assert.That(enemy1pos.X,Is.EqualTo(enemy1.Startpos.X));
        Assert.That(enemy1pos.Y,Is.EqualTo(enemy1.Startpos.Y - enemy1.Speed));
        Assert.That(enemy2pos.X,Is.EqualTo(enemy2.Startpos.X));
        Assert.That(enemy2pos.Y,Is.EqualTo(enemy2.Startpos.Y - enemy2.Speed));
    }
     // Test of MoveEnemies with ZigZagDown
    [Test]
    public void ZigZagDownEnemiesTest() {
        // Add entities to enemies
        enemies.AddEntity(enemy1);
        enemies.AddEntity(enemy2);
        // ZigZag movement strategy
        IMovementStrategy movestrat = new ZigZagDown();
        float s = 0.0003f;
        float y1i = enemy1._Shape.Position.Y + s;
        float y2i = enemy2._Shape.Position.Y + s;
        // Move the enemy
        movestrat.MoveEnemies(enemies);
        // Store position after movement in a variable
        Vec2F enemy1pos = new Vec2F(enemy1._Shape.Position.X, enemy1._Shape.Position.Y);
        Vec2F enemy2pos = new Vec2F(enemy2._Shape.Position.X, enemy2._Shape.Position.Y);
        // Position should be moved by defined formula
        float a = 0.05f;
        float p = 0.045f;
        float pi = (float)Math.PI;
        float x10 = enemy1.Startpos.X;
        float y10 = enemy1.Startpos.Y;
        float x20 = enemy2.Startpos.X;
        float y20 = enemy2.Startpos.Y;
        float sin1 = (float)Math.Sin((2 * pi *(y10-y1i))/p);
        float sin2 = (float)Math.Sin((2 * pi *(y10-y2i))/p);
        Assert.That(enemy1pos.X,Is.EqualTo(x10 + a * sin1));
        Assert.That(enemy1pos.Y,Is.EqualTo(enemy1.Startpos.Y - enemy1.Speed));
        Assert.That(enemy2pos.X,Is.EqualTo(x20 + a * sin2));
        Assert.That(enemy2pos.Y,Is.EqualTo(enemy2.Startpos.Y - enemy2.Speed));
    }
}