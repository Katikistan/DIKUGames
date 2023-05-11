using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using Breakout.Balls;
using Breakout.Players;
using Breakout.Collisions;
namespace BreakoutTests.CollisionTests;

[TestFixture]
public class WallCollisionTests{
    private EntityContainer<Ball> balls;
    private Ball ball1;
    private Ball ball2;
    public WallCollisionTests(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        balls = new EntityContainer<Ball>(2);
    }
    [Test]
    public void TestCollideLeftWall(){
        // ball1 does not collide with the left wall, ball2 does
        ball1 = new Ball(new DynamicShape(new Vec2F(0.45f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        ball2 = new Ball(new DynamicShape(new Vec2F(-0.1f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        balls.AddEntity(ball1);
        balls.AddEntity(ball2);
        WallCollision.Collide(balls);

        Assert.AreEqual(ball1._Shape.Direction.X, - ball2._Shape.Direction.X);
    }
    [Test]
    public void TestCollideRightWall(){
        // ball1 does not collide with the right wall, ball2 does
        ball1 = new Ball(new DynamicShape(new Vec2F(0.45f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        ball2 = new Ball(new DynamicShape(new Vec2F(1.0f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        balls.AddEntity(ball1);
        balls.AddEntity(ball2);
        WallCollision.Collide(balls);

        Assert.AreEqual(ball1._Shape.Direction.X, - ball2._Shape.Direction.X);
    }
    [Test]
    public void TestCollideTopWall(){
        // ball1 does not collide with the top wall, ball2 does
        ball1 = new Ball(new DynamicShape(new Vec2F(0.45f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        ball2 = new Ball(new DynamicShape(new Vec2F(0.45f, 1.0f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        balls.AddEntity(ball1);
        balls.AddEntity(ball2);
        WallCollision.Collide(balls);

        Assert.AreEqual(ball1._Shape.Direction.Y, - ball2._Shape.Direction.Y);
    }
    [Test]
    public void TestCollideBottom(){
        // ball1 does not collide with the bottom, ball2 does
        ball1 = new Ball(new DynamicShape(new Vec2F(0.45f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        ball2 = new Ball(new DynamicShape(new Vec2F(0.45f, - 0.1f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        balls.AddEntity(ball1);
        balls.AddEntity(ball2);
        Assert.AreEqual(balls.CountEntities(), 2);
        WallCollision.Collide(balls);
        Assert.AreEqual(balls.CountEntities(), 1);   
    }
}
