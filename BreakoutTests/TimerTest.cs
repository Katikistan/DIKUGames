
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
        Assert.That(99, Is.EqualTo(timer.TimeLeft));
    }
    [Test]
    public void SetTimeTest(){
        Assert.That(100, Is.EqualTo(timer.TimeLeft));
        timer.SetTime(10);
        Assert.That(10, Is.EqualTo(timer.TimeLeft));
    }
}