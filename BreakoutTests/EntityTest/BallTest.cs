using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
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
        ball = BallCreator.CreateBall();
    }
    [Test]
    public void TestIsEntity() {
        Assert.That(ball is Entity);
    }
    [Test]
    public void TestBallMove() {
        Vec2F startpos = ball._Shape.Position;
        Vec2F ballDirection = new Vec2F(0.001f,0.015f);
        Vec2F newpos = new Vec2F(startpos.X + ballDirection.X, startpos.Y + ballDirection.Y);
        Assert.AreEqual(ball._Shape.Direction.X, ballDirection.X);
        Assert.AreEqual(ball._Shape.Direction.Y, ballDirection.Y);
        ball.Move();
        Vec2F currentpos = new Vec2F(ball._Shape.Position.X, ball._Shape.Position.Y);
        Assert.AreNotEqual(currentpos.X, startpos.X);
        Assert.AreNotEqual(currentpos.Y, startpos.Y);
        Assert.AreEqual(currentpos.X, newpos.X);
        Assert.AreEqual(currentpos.Y, newpos.Y);
    }
}