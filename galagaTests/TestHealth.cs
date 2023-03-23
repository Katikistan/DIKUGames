using DIKUArcade.Math;
using Galaga;
namespace galagaTests;
[TestFixture]
public class TestHealth {
    private Health health = null!;
    public TestHealth() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        health = new Health(
            new Vec2F(0.04f, -0.42f),
            new Vec2F(0.4f, 0.5f));
    }
    [Test]
    public void TestLoseHealth() {
        Assert.That(health.Lives, Is.EqualTo(3));
        health.LoseHealth();
        Assert.That(health.Lives, Is.EqualTo(2));
        health.LoseHealth();
        Assert.That(health.Lives, Is.EqualTo(1));
        health.LoseHealth();
        Assert.That(health.Lives, Is.EqualTo(0));
        health.LoseHealth();
        Assert.That(health.Lives, Is.EqualTo(0));
    }
}