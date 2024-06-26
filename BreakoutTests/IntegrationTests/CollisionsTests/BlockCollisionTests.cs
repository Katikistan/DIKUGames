using Breakout.Balls;
using Breakout.Blocks;
using Breakout.Collisions;
using Breakout.Levels;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.Integration.CollisionTests;
[TestFixture]
public class BlockCollisionTests {
    private EntityContainer<Block> blocks;
    private EntityContainer<Ball> balls;
    private Ball ball;
    private LevelCreator levelCreator;
    public BlockCollisionTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        ball = new Ball(new DynamicShape(
            new Vec2F(0.45f, 0.2f),
            new Vec2F(0.03f, 0.03f),
            new Vec2F(0.001f, 0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        balls = new EntityContainer<Ball>(2);
        balls.AddEntity(ball);
        levelCreator = new LevelCreator();
        levelCreator.CreateLevel("level1.txt");
        blocks = levelCreator.Blocks;
    }
    [Test]
    public void TestCollideUpDown() {
        int blockCount = (blocks.CountEntities());
        BlockCollision.Collide(balls, blocks, false);
        Assert.That(blockCount, Is.EqualTo(blocks.CountEntities()));

        // Adding a ball that collides with blocks
        Ball collisionBall = new Ball(new DynamicShape(
            new Vec2F(0.4789996f, 0.63499963f),
            new Vec2F(0.03f, 0.03f),
            new Vec2F(0.001f, 0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        balls.AddEntity(collisionBall);

        Vec2F ballDirection = new Vec2F(
            (float) collisionBall._Shape.Direction.X, (float) collisionBall._Shape.Direction.Y);

        BlockCollision.Collide(balls, blocks, false);

        Assert.That(blockCount, Is.Not.EqualTo(blocks.CountEntities()));
        Assert.That(ballDirection.X, Is.EqualTo(collisionBall._Shape.Direction.X));
        Assert.That(ballDirection.Y, Is.EqualTo(-collisionBall._Shape.Direction.Y));
    }
    [Test]
    public void TestCollideLeftRight() {
        int blockCount = (blocks.CountEntities());
        BlockCollision.Collide(balls, blocks, false);
        Assert.That(blockCount, Is.EqualTo(blocks.CountEntities()));

        // Adding a ball that collides with blocks
        Ball collisionBall = new Ball(new DynamicShape(
            new Vec2F(0.41926193f, 0.7319267f),
            new Vec2F(0.03f, 0.03f),
            new Vec2F(-0.009906301f, -0.011307749f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        balls.AddEntity(collisionBall);

        Vec2F ballDirection = new Vec2F(
            (float) collisionBall._Shape.Direction.X, (float) collisionBall._Shape.Direction.Y);

        BlockCollision.Collide(balls, blocks, false);

        Assert.That(blockCount, Is.Not.EqualTo(blocks.CountEntities()));
        Assert.That(ballDirection.X, Is.EqualTo(-collisionBall._Shape.Direction.X));
        Assert.That(ballDirection.Y, Is.EqualTo(collisionBall._Shape.Direction.Y));
    }
    [Test]
    public void TestHardBallCollision() {
        int blockCount = (blocks.CountEntities());
        BlockCollision.Collide(balls, blocks, false);
        Assert.That(blockCount, Is.EqualTo(blocks.CountEntities()));

        // Adding a ball that collides with blocks
        Ball collisionBall = new Ball(new DynamicShape(
            new Vec2F(0.41926193f, 0.7319267f),
            new Vec2F(0.03f, 0.03f),
            new Vec2F(-0.009906301f, -0.011307749f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        balls.AddEntity(collisionBall);

        Vec2F ballDirection = new Vec2F(
            (float) collisionBall._Shape.Direction.X, (float) collisionBall._Shape.Direction.Y);
        BlockCollision.Collide(balls, blocks, true);

        //checking that hardball's direction is unchanged after collisions
        Assert.That(blockCount, Is.Not.EqualTo(blocks.CountEntities()));
        Assert.That(ballDirection.X, Is.EqualTo(collisionBall._Shape.Direction.X));
        Assert.That(ballDirection.Y, Is.EqualTo(collisionBall._Shape.Direction.Y));
    }
}
