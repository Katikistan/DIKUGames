using Breakout.Balls;
using Breakout.Collisions;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.CollisionTests;

[TestFixture]
public class PlayerCollisionTests {
    private Ball ballmid;
    private Ball ballleft;
    private Ball ballright;
    private Ball ballleftleft;
    private Ball ballrightright;
    private EntityContainer<Ball> balls;
    private Player player;
    [SetUp]
    public void Setup() {
        ballleft = new Ball(new DynamicShape(
            new Vec2F(0.45f, 0.1f), new Vec2F(0.03f, 0.03f), new Vec2F(0.00f, -0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        ballleftleft = new Ball(new DynamicShape(
            new Vec2F(0.425f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.00f, -0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        ballmid = new Ball(new DynamicShape(
            new Vec2F(0.475f, 0.3f), new Vec2F(0.03f, 0.03f), new Vec2F(0.00f, -0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        ballright = new Ball(new DynamicShape(
            new Vec2F(0.525f, 0.4f), new Vec2F(0.03f, 0.03f), new Vec2F(0.00f, -0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        ballrightright = new Ball(new DynamicShape(
            new Vec2F(0.56f, 0.5f), new Vec2F(0.03f, 0.03f), new Vec2F(0.00f, -0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));

        balls = new EntityContainer<Ball>(5);
        balls.AddEntity(ballleft);
        balls.AddEntity(ballleftleft);
        balls.AddEntity(ballmid);
        balls.AddEntity(ballright);
        balls.AddEntity(ballrightright);
    }
    [Test]
    public void TestCollide() {

        // left side of player hit test
        while (!PlayerCollision.Collide(balls, player)) {
            balls.Iterate(ball => {
                ball.Move();
            });
        }
        Assert.That(ballleft._Shape.Direction.Y, Is.EqualTo(0.01409f));
        Assert.That(ballleft._Shape.Direction.X, Is.EqualTo(-0.0051f));
        ballleft.DeleteEntity();

        // leftleft side of player hit test

        while (!PlayerCollision.Collide(balls, player)) {
            balls.Iterate(ball => {
                ball.Move();
            });
        }
        Assert.That(ballleftleft._Shape.Direction.Y, Is.EqualTo(0.0106f));
        ballleftleft.DeleteEntity();

        // middle of player hit test

        while (!PlayerCollision.Collide(balls, player)) {
            balls.Iterate(ball => {
                ball.Move();
            });
        }
        Assert.That(ballmid._Shape.Direction.Y, Is.EqualTo(0.015f));
        ballmid.DeleteEntity();

        // right side of player hit test

        while (!PlayerCollision.Collide(balls, player)) {
            balls.Iterate(ball => {
                ball.Move();
            });
        }
        Assert.That(ballright._Shape.Direction.Y, Is.EqualTo(0.01409f));
        ballright.DeleteEntity();

        // rightright side of player hit test

        while (!PlayerCollision.Collide(balls, player)) {
            balls.Iterate(ball => {
                ball.Move();
            });
        }
        Assert.That(ballrightright._Shape.Direction.Y, Is.EqualTo(0.0106f));
        ballrightright.DeleteEntity();
    }
}