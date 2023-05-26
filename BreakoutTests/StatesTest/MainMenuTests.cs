using DIKUArcade.Entities;
using DIKUArcade.Input;

namespace Breakout.States;
[TestFixture]
public class MainMenuTest {
    private MainMenu mainMenu;
    public MainMenuTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        mainMenu = new MainMenu();
        mainMenu.InitializeGameState();
    }
    [Test]
    public void TestInitializeGameState() {
        Assert.That(mainMenu.BackGround is Entity);
        Assert.That(mainMenu.ActiveMenuButton, Is.EqualTo(0));
    }
    [Test]
    public void TestKeyPress() {
        // Pressing down changes active button
        Assert.That(mainMenu.ActiveMenuButton, Is.EqualTo(0));
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.That(mainMenu.ActiveMenuButton, Is.EqualTo(1));
        // Pressing down again does not change the active button
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.That(mainMenu.ActiveMenuButton, Is.EqualTo(1));
        // Pressing up changes the active button
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        Assert.That(mainMenu.ActiveMenuButton, Is.EqualTo(0));
    }
}