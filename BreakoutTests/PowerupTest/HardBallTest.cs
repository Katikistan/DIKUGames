using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.States;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout.Collisions;
using Breakout.Blocks;
using Breakout.Balls;
using Breakout.Levels;
using Breakout.Powerups;
using Breakout;

namespace BreakoutTests.PowerupTests;

[TestFixture]
public class HardBallTest{
    private LevelManager levelManager;
    private Ball ball;
    private Block block;
    private EntityContainer<Ball> balls;
    private EntityContainer<Block> blocks;
    private DynamicShape blockShape;
    public HardBallTest(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup(){
        float x = 1f / 12f;
        float y = (1f / 12f) / 2.5f;
        blockShape = new(0.5f, 0.5f, x, y);
        levelManager = new LevelManager();
        blocks = new EntityContainer<Block>(1);
        block = new DefaultBlock(blockShape, "red-block.png");
        balls = new EntityContainer<Ball>(1);
        ball = BallCreator.CreateBall(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f));
        balls.AddEntity(ball);
    }
    [Test]
    public void TestHardBallEvent() {
        Assert.That(!levelManager.HardBall);
        levelManager.ProcessEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "HARD BALL",
                    StringArg1 = "START"
                });
        Assert.That(levelManager.HardBall);
    }
    [Test]
    public void TestCollision() {
        float direction = ball._Shape.Direction.X;
        BlockCollision.Collide(balls, blocks, true);
        Assert.That(ball._Shape.Direction.X == direction);
        Assert.That(blocks.CountEntities() == 0);
    }
}