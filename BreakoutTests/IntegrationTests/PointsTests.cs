using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout;
using Breakout.Blocks;
namespace BreakoutTests.Integration;
[TestFixture]
public class PointsTests {
    private Points points = Points.GetInstance();
    private GameEvent givePoints;
    private Block defaultBlock;
    private Block hardened;
    private Block powerupBlock;
    public PointsTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        points.ResetPoints();
        points = Points.GetInstance();
        givePoints = (new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET POINTS",
            IntArg1 = 10
        });
        defaultBlock = new DefaultBlock(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
        hardened = new Hardened(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
        powerupBlock = new PowerupBlock(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
    }
    [Test]
    public void TestDefaultblockGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        defaultBlock.LoseHealth(1);
        BreakoutBus.GetBus().ProcessEvents();
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
    }

    [Test]
    public void TestHardenedGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        hardened.LoseHealth(2);
        BreakoutBus.GetBus().ProcessEvents();
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(20));
    }
    [Test]
    public void TestPowerupBlockGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        powerupBlock.LoseHealth(1);
        BreakoutBus.GetBus().ProcessEvents();
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
    }
}