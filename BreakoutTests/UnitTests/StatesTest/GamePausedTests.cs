using Breakout.States;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
namespace BreakoutTests.Unit.StatesTests;
[TestFixture]
public class GamePausedTests {
    StateMachine statemachine;
    private GamePaused gamePaused;
    public GamePausedTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        statemachine = new StateMachine();

        gamePaused = new GamePaused();
        gamePaused.InitializeGameState();
    }
    [Test]
    public void TestInitializeGameState() {
        Assert.That(gamePaused.Background is Entity);
        Assert.That(gamePaused.PauseText is Text);
        Assert.That(gamePaused.ActiveMenuButton, Is.EqualTo(0));
    }
    [Test]
    public void TestKeyPress() {
        // Pressing down changes active button
        Assert.That(gamePaused.ActiveMenuButton, Is.EqualTo(0));
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.That(gamePaused.ActiveMenuButton, Is.EqualTo(1));
        // Pressing down again does not change the active button
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.That(gamePaused.ActiveMenuButton, Is.EqualTo(1));
        // Pressing up changes the active button
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        Assert.That(gamePaused.ActiveMenuButton, Is.EqualTo(0));
    }
    [Test]
    public void TestgamePausedEnterKey() {
        gamePaused.InitializeGameState();

        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Up);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Enter);

        BreakoutBus.GetBus().ProcessEvents();
        Assert.That(statemachine.ActiveState, Is.EqualTo(GameRunning.GetInstance()));

        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Enter);
    }
}