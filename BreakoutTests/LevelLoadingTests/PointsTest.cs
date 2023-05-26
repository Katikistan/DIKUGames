using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout;
using Breakout.Levels;
using Breakout.Blocks;
namespace BreakoutTests.EntityTest;
[TestFixture]
public class TestPoints {
    private Points points;
    private GameEvent getPoints;
    private GameEvent resetPoints;
    private Block defaultBlock;
    private Block hardened;
    public TestPoints() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        points = new Points();
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, points);

    }
    [SetUp]
    public void Setup() {
        points = new Points();
        getPoints = (new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET POINTS",
            IntArg1 = 10
        });
        resetPoints = (new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "RESET POINTS"
        });
        defaultBlock = new DefaultBlock(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
        hardened = new Hardened(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
    }
    [Test]
    public void TestGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        BreakoutBus.GetBus().RegisterEvent(getPoints);
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        BreakoutBus.GetBus().RegisterEvent(getPoints);
        BreakoutBus.GetBus().ProcessEvents();
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
        BreakoutBus.GetBus().RegisterEvent(getPoints);
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        BreakoutBus.GetBus().RegisterEvent(resetPoints);
        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(0));
    }
}