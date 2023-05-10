using DIKUArcade.Events;
using Breakout.Levels;
namespace BreakoutTests.EntityTest;
[TestFixture]
public class TestPoints {
    private Points points;
    private GameEvent getPoints;
    private GameEvent resetPoints;
    private GameEventBus testEventBus;
    public TestPoints() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        points = new Points();
        testEventBus = new GameEventBus();
        testEventBus.InitializeEventBus(
            new List<GameEventType> { GameEventType.StatusEvent });
        testEventBus.Subscribe(GameEventType.StatusEvent, points);
        getPoints = (new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET POINTS",
            IntArg1 = 10
        });
        resetPoints = (new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "RESET POINTS"
        });
    }
    [Test]
    public void TestGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        testEventBus.RegisterEvent(getPoints);
        testEventBus.ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        testEventBus.RegisterEvent(getPoints);
        testEventBus.ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(20));
    }
    [Test]
    public void TestResetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        testEventBus.RegisterEvent(getPoints);
        testEventBus.ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        testEventBus.RegisterEvent(resetPoints);
        testEventBus.ProcessEvents();
        Assert.That(points.GetPoints(), Is.EqualTo(0));
    }
}