using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;

namespace Breakout.States;
[TestFixture]
public class MainMenuTest{
    private MainMenu mainMenu; 
    public MainMenuTest(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        mainMenu = new MainMenu();
        mainMenu.InitializeGameState();
    }
    [Test]
    public void TestKeyPress(){
        Assert.AreEqual(mainMenu.ActiveMenuButton, 0);
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        Assert.AreEqual(mainMenu.ActiveMenuButton, 1);
    }
}