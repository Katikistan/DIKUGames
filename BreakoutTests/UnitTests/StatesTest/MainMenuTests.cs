using Breakout.States;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Input;
namespace BreakoutTests.Unit.StatesTests;
[TestFixture]
public class MainMenuTests {
    StateMachine statemachine;

    private MainMenu mainMenu;
    public MainMenuTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        statemachine = new StateMachine();

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
    [Test]
    public void TestmainMenuEnterKey() {
        mainMenu.InitializeGameState();

        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);

        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(statemachine.ActiveState, Is.EqualTo(GameRunning.GetInstance()));

    }
}