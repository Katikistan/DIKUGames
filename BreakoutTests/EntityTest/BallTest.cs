using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Balls;
namespace BreakoutTests.EntityTest;
[TestFixture]
public class TestBall {
    private Ball ball;
    public TestBall() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        ball = BallCreator.CreateBall(new Vec2F(0.45f, 0.2f), new Vec2F(0.001f, 0.015f));
    }
    [Test]
    public void TestIsEntity() {
        Assert.That(ball is Entity);
    }
    [Test]
    public void TestBallMove() {
        Vec2F startpos = ball._Shape.Position;
        Vec2F ballDirection = new Vec2F(0.001f, 0.015f);
        Vec2F newpos = new Vec2F(startpos.X + ballDirection.X, startpos.Y + ballDirection.Y);
        Assert.That(ball._Shape.Direction.X, Is.EqualTo(ballDirection.X));
        Assert.That(ball._Shape.Direction.Y, Is.EqualTo(ballDirection.Y));
        ball.Move();
        Vec2F currentpos = new Vec2F(ball._Shape.Position.X, ball._Shape.Position.Y);
        Assert.That(currentpos.X, Is.Not.EqualTo(startpos.X));
        Assert.That(currentpos.Y, Is.Not.EqualTo(startpos.Y));
        Assert.That(currentpos.X, Is.EqualTo(newpos.X));
        Assert.That(currentpos.Y, Is.EqualTo(newpos.Y));
    }
}