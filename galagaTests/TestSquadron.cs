using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
using Galaga.Squadron;
namespace galagaTests;

[TestFixture]
public class TestSquadron {
    private ISquadron squadron;
    private List<Image> blueMonster;
    private List<Image> greenMonster; 
    [SetUp]
    public void Setup() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        blueMonster = ImageStride.CreateStrides
        (4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
        greenMonster = ImageStride.CreateStrides
        (2, Path.Combine("..", "Galaga", "Assets", "Images", "GreenMonster.png"));
    }

    // Testing the number of enemies in line
    [Test]  
    public void numOfEnemiesLine() {
        squadron = new SquadronLine();
        Assert.AreEqual(6, squadron.MaxEnemies);
    }
    
    // Testing the number of enemies in square
    [Test]  
    public void numOfEnemiesSquare() {
        squadron = new SquadronSquare();
        Assert.AreEqual(8, squadron.MaxEnemies);
    }

    // Testing the number of enemies in triangle
    [Test]  
    public void numOfEnemiesTriangle() {
        squadron = new SquadronTriangle();
        Assert.AreEqual(9, squadron.MaxEnemies);
    }
    
    // Testing if creating enemies adds enemies to the entity container
    [Test]  
    public void createEnemiesTest() {
        // Creating two squadron lines
        squadron = new SquadronLine();
        ISquadron squadronNoEnemies = new SquadronLine();
        
        // both squadrons should have no enemies
        Assert.AreEqual(squadronNoEnemies.Enemies, squadron.Enemies);
        
        // Creating enemies on one of the squadrons
        squadron.CreateEnemies(blueMonster, greenMonster);
        
        // Making sure squadron is not unchanged after creating enemies
        Assert.AreNotEqual(squadronNoEnemies.Enemies, squadron.Enemies);
    }

}