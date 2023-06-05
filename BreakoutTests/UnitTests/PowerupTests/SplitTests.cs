using Breakout;
using Breakout.Balls;
using Breakout.Collisions;
using Breakout.Levels;
using Breakout.Players;
using Breakout.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.PowerupTests;
[TestFixture]
public class SplitTests {
    public Ball ball;
    public Powerup split;
    public EntityContainer<Powerup> powerups;
    public EntityContainer<Ball> balls;

    public Player player;
    public LevelManager levelmanager;
    public SplitTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]

    public void Setup() {
        levelmanager = new LevelManager();
        ball = BallCreator.CreateBall(new Vec2F(0.45f, 0.2f), new Vec2F(0.001f, 0.015f));
        split = new Split(new DynamicShape(
                new Vec2F(0.425f, 0.1f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        powerups = new EntityContainer<Powerup>(5);
        powerups.AddEntity(split);

    }
    [Test]
    public void TestSplit() {
        levelmanager.Balls.AddEntity(ball);
        levelmanager.Powerups.AddEntity(split);
        Assert.That(levelmanager.Balls.CountEntities() == 1);
        // Split powerup drops until collision with player
        while (!PowerUpCollision.Collide(levelmanager.Powerups, player)) {
            levelmanager.Powerups.Iterate(powerup => {
                powerup.Move();
            });
        }
        BreakoutBus.GetBus().ProcessEvents();
        //checking if the nmumber of balls tripled as expected after the gameevent is proccessed
        Assert.That(levelmanager.Balls.CountEntities(), Is.EqualTo(3));
    }
}