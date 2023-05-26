using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;

namespace Breakout.States;
[TestFixture]
public class GamePausedTest {
    private GamePaused gamePaused;
    public GamePausedTest() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        gamePaused = new GamePaused();
        gamePaused.InitializeGameState();
    }
    [Test]
    public void TestInitializeGameState() {
        Assert.That(gamePaused.Background is Entity);
        Assert.That(gamePaused.PauseText is Text);
        Assert.AreEqual(gamePaused.ActiveMenuButton, 0);
    }
    [Test]
    public void TestKeyPress() {
        // Pressing down changes active button
        Assert.AreEqual(gamePaused.ActiveMenuButton, 0);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.AreEqual(gamePaused.ActiveMenuButton, 1);
        // Pressing down again does not change the active button
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.AreEqual(gamePaused.ActiveMenuButton, 1);
        // Pressing up changes the active button
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        Assert.AreEqual(gamePaused.ActiveMenuButton, 0);
    }
}