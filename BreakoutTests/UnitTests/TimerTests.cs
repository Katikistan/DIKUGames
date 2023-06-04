using DIKUArcade.Math;
namespace BreakoutTests;

[TestFixture]
public class TimerTests {
    private Breakout.Timer timer;
    public TimerTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        timer = new Breakout.Timer(new Vec2F(0.5f, 0.5f), 100);
    }
    [Test]
    public void IterateSecondsTest() {
        Assert.AreEqual(timer.TimeLeft, 100);
        while (timer.TimeLeft == 100) {
            timer.Render();
        }
        Assert.That(99, Is.EqualTo(timer.TimeLeft));
    }
    [Test]
    public void SetTimeTest(){
        Assert.That(100 == timer.TimeLeft);
        timer.SetTime(10);
        Assert.That(10, Is.EqualTo(timer.TimeLeft));
    }
    [Test]
    public void NoTimeLeftTest(){
        timer.SetTime(1);
        Assert.That(timer.TimeLeft, Is.EqualTo(1));
        while (timer.TimeLeft > 0) {
            timer.Render();
        }
        timer.Render();
        Assert.That(timer.TimeLeft, Is.EqualTo(0));
    }
}