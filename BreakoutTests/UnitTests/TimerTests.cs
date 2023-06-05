using DIKUArcade.Math;
namespace BreakoutTests.Unit;

[TestFixture]
public class TimerTests {
    private Breakout.Timer timer;
    public TimerTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        timer = new Breakout.Timer(new Vec2F(0.5f, 0.5f), 100);
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
        Assert.That(100 == timer.TimeLeft);
        timer.SetTime(10);
        Assert.That(timer.TimeLeft, Is.EqualTo(10));
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