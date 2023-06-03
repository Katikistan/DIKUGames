using Breakout.Balls;
using Breakout.Blocks;
using Breakout.Collisions;
using Breakout.Levels;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
namespace BreakoutTests.Integration.PowerupTests;
[TestFixture]
public class HardBallTests{
    private LevelManager levelManager;
    private Ball ball;
    private Block block;
    private EntityContainer<Ball> balls;
    private EntityContainer<Block> blocks;
    private DynamicShape blockShape;
    public HardBallTests(){
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
        Assert.That(!levelManager.HardBalls);
        levelManager.ProcessEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "HARD BALL",
                    StringArg1 = "START"
                });
        Assert.That(levelManager.HardBalls);
    }
    [Test]
    public void TestCollision() {
        float direction = ball._Shape.Direction.X;
        BlockCollision.Collide(balls, blocks, true);
        Assert.That(ball._Shape.Direction.X == direction);
        Assert.That(blocks.CountEntities() == 0);
    }
}