using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;

namespace Breakout.States;
[TestFixture]
public class GamePausedTest{
    private GamePaused gamePaused; 
    public GamePausedTest(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        gamePaused = new GamePaused();
        gamePaused.InitializeGameState();
    }
    [Test]
    public void TestKeyPress(){
        Assert.AreEqual(gamePaused.ActiveMenuButton, 0);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.AreEqual(gamePaused.ActiveMenuButton, 1);
    }
}