using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout;
using Breakout.Blocks;
namespace BreakoutTests.Integration;
[TestFixture]
public class PointsTests {
    private Points points = Points.getInstance();
    private GameEvent getPoints;
    private GameEvent resetPoints;
    private Block defaultBlock;
    private Block hardened;
    public PointsTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        points.ResetPoints();
        points = Points.getInstance();
        getPoints = (new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET POINTS",
            IntArg1 = 10
        });
        defaultBlock = new DefaultBlock(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
        hardened = new Hardened(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
    }
    [Test]
    public void TestGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        points.ProcessEvent(getPoints);
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        points.ProcessEvent(getPoints);
        Assert.That(points.GetPoints(), Is.EqualTo(20));
    }
    [Test]
    public void TestBlockGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        defaultBlock.LoseHealth(1);
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        hardened.LoseHealth(1);
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        hardened.LoseHealth(1);
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(30));
    }
    [Test]
    public void TestResetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        points.ProcessEvent(getPoints);
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        points.ResetPoints();
        points = Points.getInstance();
        Assert.That(points.GetPoints(), Is.EqualTo(0));
    }
}