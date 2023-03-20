using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
namespace galagaTests;
[TestFixture]
public class TestHealth {
    private Health health;
    [SetUp]
    public void Setup() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        health = new Health(
            new Vec2F(0.04f, -0.42f),
            new Vec2F(0.4f, 0.5f));
    }
    [Test]
    public void TestLoseHealth() {
        Assert.AreEqual(3, health.Lives);
        health.LoseHealth();
        Assert.AreEqual(2, health.Lives);
        health.LoseHealth();
        Assert.AreEqual(1, health.Lives);
        health.LoseHealth();
        Assert.AreEqual(0, health.Lives);
        health.LoseHealth();
        Assert.AreEqual(0, health.Lives);
    }
}