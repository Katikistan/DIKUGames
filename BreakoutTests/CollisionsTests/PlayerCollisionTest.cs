using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using Breakout.Balls;
using Breakout.Players;
using Breakout.Collisions;
using Breakout.Levels;
namespace BreakoutTests.CollisionTests;

[TestFixture]
public class PlayerCollisionTest{
    private Ball ball;
    private EntityContainer<Ball> balls;
    private Player player;
    [SetUp]
    public void Setup() {
        ball = BallCreator.CreateBall();
        balls = new EntityContainer<Ball>(1);
        balls.AddEntity(ball);   
    }
    [Test]
    public void TestCollide(){
    }
}