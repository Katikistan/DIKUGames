using Breakout;
using Breakout.Balls;
using Breakout.Blocks;
using Breakout.Collisions;
using Breakout.Levels;
using Breakout.Powerups;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace BreakoutTests.PowerupTests;
[TestFixture]
public class HardBallTests{
    public Player player;
    public Powerup hardballpower;
    private LevelManager levelManager;
    private Ball ball;
    private Block block;
    private EntityContainer<Ball> balls;
    private EntityContainer<Powerup> powerups;

    private EntityContainer<Block> blocks;
    private DynamicShape blockShape;
    public HardBallTests(){
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup(){
        hardballpower = new HardBall(new DynamicShape(
                new Vec2F(0.425f, 0.1f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));

        powerups = new EntityContainer<Powerup>(1);
        powerups.AddEntity(hardballpower);

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

    [Test]
    public void TestHardBallEffect() {
        while (!PowerUpCollision.Collide(powerups, player)){
            powerups.Iterate(powerup => {
                powerup.Move();
            });
        }
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(levelManager.HardBalls, Is.EqualTo(true));
    }
}