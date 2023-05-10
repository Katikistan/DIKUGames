using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
namespace BreakoutTests.EntityTest;
[TestFixture]
public class BlockTests {
    private Block defaultblock = null!;
    private Block hardened;
    private Block unbreakable;
    public BlockTests() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        defaultblock = new DefaultBlock(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
        hardened = new Hardened(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
        unbreakable = new Unbreakable(
        new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f)), "blue-block.png");
    }
    [Test]
    public void TestIsEntity() {
        Assert.That(defaultblock is Entity);
        Assert.That(hardened is Entity);
        Assert.That(unbreakable is Entity);
        Assert.That(defaultblock is Block);
        Assert.That(hardened is Block);
        Assert.That(unbreakable is Block);
    }
    [Test]
    public void TestHasHealthAndValue() {
        Assert.That(defaultblock.Value, Is.EqualTo(10));
        Assert.That(defaultblock.Health, Is.EqualTo(1));
        Assert.That(hardened.Value, Is.EqualTo(20)); //
        Assert.That(hardened.Health, Is.EqualTo(2));
        Assert.That(unbreakable.Value, Is.EqualTo(10));
        Assert.That(unbreakable.Health, Is.EqualTo(1));
    }
    [Test]
    public void TestDecHealth() {
        // defaultblock
        Assert.That(defaultblock.Health, Is.EqualTo(1));
        defaultblock.LoseHealth();
        Assert.That(defaultblock.Health, Is.EqualTo(0));
        defaultblock.LoseHealth();
        Assert.That(defaultblock.Health, Is.EqualTo(-1));
        // hardened
        Assert.That(hardened.Health, Is.EqualTo(2)); //
        hardened.LoseHealth();
        Assert.That(hardened.Health, Is.EqualTo(1));
        hardened.LoseHealth();
        Assert.That(hardened.Health, Is.EqualTo(0));
        // unbreakable
        for (int i = 0; i < 10; i++) {
            Assert.That(unbreakable.Health, Is.EqualTo(1));
            unbreakable.LoseHealth();
            Assert.That(unbreakable.Health, Is.EqualTo(1));
        }


    }
    [Test]
    public void TestBlockIsDead() {
        Assert.That(!defaultblock.IsDeleted());
        defaultblock.LoseHealth();
        Assert.That(defaultblock.IsDeleted());
        // hardened
        Assert.That(!hardened.IsDeleted());
        hardened.LoseHealth();
        Assert.That(!hardened.IsDeleted());
        hardened.LoseHealth();
        Assert.That(hardened.IsDeleted()); //
        // unbreakable
        for (int i = 0; i < 10; i++) {
            unbreakable.LoseHealth();
            Assert.That(!unbreakable.IsDeleted());
        }
    }
}