using DIKUArcade.Events;
using Breakout;
using Breakout.Blocks;
namespace BreakoutTests.Unit;
[TestFixture]
public class PointsTests {
    private Points points = Points.GetInstance();
    private GameEvent givePoints;
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
    }
    [Test]
    public void TestGetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        points.ProcessEvent(givePoints);
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        points.ProcessEvent(givePoints);
        Assert.That(points.GetPoints(), Is.EqualTo(20));
    }

    [Test]
    public void TestResetPoints() {
        Assert.That(points.GetPoints(), Is.EqualTo(0));
        points.ProcessEvent(givePoints);
        Assert.That(points.GetPoints(), Is.EqualTo(10));
        points.ResetPoints();
        points = Points.GetInstance();
        Assert.That(points.GetPoints(), Is.EqualTo(0));
    }
}