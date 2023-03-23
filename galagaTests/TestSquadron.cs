using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
using Galaga.Squadron;
namespace galagaTests;

[TestFixture]
public class TestSquadron {
    private ISquadron squadron = null!;
    private List<Image> blueMonster = null!;
    private List<Image> greenMonster = null!;
    public TestSquadron() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        // DIKUArcade.GUI.Window.CreateOpenGLContext();
        blueMonster = ImageStride.CreateStrides
        (4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
        greenMonster = ImageStride.CreateStrides
        (2, Path.Combine("..", "Galaga", "Assets", "Images", "GreenMonster.png"));
    }

    // Testing the number of enemies in line
    [Test]
    public void numOfEnemiesLine() {
        squadron = new SquadronLine();
        Assert.That(squadron.MaxEnemies, Is.EqualTo(6));
    }

    // Testing the number of enemies in square
    [Test]
    public void numOfEnemiesSquare() {
        squadron = new SquadronSquare();
        Assert.That(squadron.MaxEnemies, Is.EqualTo(8));
    }

    // Testing the number of enemies in triangle
    [Test]
    public void numOfEnemiesTriangle() {
        squadron = new SquadronTriangle();
        Assert.That(squadron.MaxEnemies, Is.EqualTo(9));
    }

    // Testing if creating enemies adds enemies to the entity container
    [Test]
    public void createEnemiesTest() {
        // Creating two squadron lines
        squadron = new SquadronLine();
        ISquadron squadronNoEnemies = new SquadronLine();

        // both squadrons should have no enemies
        Assert.That(squadron.Enemies, Is.EqualTo(squadronNoEnemies.Enemies));

        // Creating enemies on one of the squadrons
        squadron.CreateEnemies(blueMonster, greenMonster);

        // Making sure squadron is not unchanged after creating enemies
        Assert.That(squadron.Enemies, Is.Not.EqualTo(squadronNoEnemies.Enemies));
    }

}