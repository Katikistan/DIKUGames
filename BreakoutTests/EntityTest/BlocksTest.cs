using DIKUArcade.Entities;
using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
namespace BreakoutTests.EntityTest;
[TestFixture]
public class TestBlock {
    private Block defaultblock = null!;
    public TestBlock() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        defaultblock = new DefaultBlock(
            new StationaryShape(new Vec2F(0.5f,0.5f), new Vec2F(0.5f,0.5f)),
            new Image(Path.Combine("..","Breakout","Assets", "Images", "blue-block.png")));
    }
    [Test]
    public void TestIsEntity() {
        Assert.That(defaultblock is Entity);
    }
    [Test]
    public void TestHasHealthAndValue() {
        Assert.That(defaultblock.Value, Is.EqualTo(100));
        Assert.That(defaultblock.Health, Is.EqualTo(1));
    }
    [Test]
    public void TestDecHealth() {
        Assert.That(defaultblock.Health, Is.EqualTo(1));
        defaultblock.LoseHealth();
        Assert.That(defaultblock.Health, Is.EqualTo(0));
        defaultblock.LoseHealth();
        Assert.That(defaultblock.Health, Is.EqualTo(-1));
    }
    [Test]
    public void TestBlockIsDead() {
        Assert.That(!defaultblock.IsDeleted());
        defaultblock.LoseHealth();
        Assert.That(defaultblock.IsDeleted());
        defaultblock.LoseHealth();
    }
}