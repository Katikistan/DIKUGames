
using DIKUArcade.Math;
namespace BreakoutTests.TimerTest;

[TestFixture]
public class TimerTest {
    private Breakout.Timers.Timer timer;
    public TimerTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        timer = new Breakout.Timers.Timer(new Vec2F(0.5f, 0.5f), 100);
    }
    [Test]
    public void IterateSecondsTest() {
        Assert.That(timer.TimeLeft, Is.EqualTo(100));
        while (timer.TimeLeft == 100) {
            timer.Render();
        }
        Assert.That(timer.TimeLeft, Is.EqualTo(99));
    }
    [Test]
    public void SetTimeTest(){
        Assert.That(timer.TimeLeft, Is.EqualTo(100));
        timer.SetTime(10);
        Assert.That(timer.TimeLeft,Is.EqualTo(10));
    }
}