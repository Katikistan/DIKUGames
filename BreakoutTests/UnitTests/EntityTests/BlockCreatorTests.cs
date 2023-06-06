using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Math;
namespace BreakoutTests.Unit.EntityTests;
[TestFixture]
public class BlockCreatorTests {
    private Shape shape;
    private string image;
    private Block block;
    public BlockCreatorTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        // Arrange
        shape = new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f));
        image = "blue-block.png";
    }
    [Test]
    public void TestCreateBlock() {
        //Act
        block = BlockCreator.CreateBlock(shape, image, "");
        // Assert
        Assert.That(block is DefaultBlock);

        block = BlockCreator.CreateBlock(shape, image, "nonexistingBlocktype");
        Assert.That(block is DefaultBlock);

        block = BlockCreator.CreateBlock(shape, image, "Hardened");
        Assert.That(block is Hardened);

        block = BlockCreator.CreateBlock(shape, image, "Unbreakable");
        Assert.That(block is Unbreakable);
    }
}