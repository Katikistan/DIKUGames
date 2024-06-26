using Breakout.Levels;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;
using System.Collections.Generic;
namespace Breakout.States;
/// <summary>
///  A state for when the game is running.
/// </summary>
public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private LevelManager levelManager = null!;
    private Points points = null!;
    private Health health;
    private Entity background = null!;
    private List<string> levelslst;
    // Public getters for testing
    public Health Health {
        get => health;
    }
    public Entity Background {
        get => background;
    }
    public List<string> Levellst {
        get => levelslst;
    }
    public Points Points {
        get => points;
    }
    public LevelManager LevelManager {
        get => levelManager;
    }
    /// <summary>
    ///  Gets or creates an instance of the GameRunning state
    /// </summary>
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }
    /// <summary>
    ///  Inizializes the Game state, this functions as a constructor for the state
    /// </summary>
    public void InitializeGameState() {
        background = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Breakout", "Assets", "Images", "SpaceBackground.png")));
        levelManager = new LevelManager();
        levelslst = new List<string>();
        Levellst.Add("level1.txt");
        Levellst.Add("level2.txt");
        Levellst.Add("level3.txt");
        Levellst.Add("level4.txt");
        Levellst.Add("wall.txt");
        LevelManager.NewLevel(Levellst[0]);
        points = Points.GetInstance();
        health = new Health();
    }
    /// <summary>
    /// Resets the state
    /// </summary>
    public void ResetState() {
        GameRunning.instance = null;
    }
    /// <summary>
    ///  Renders objects in the state
    /// </summary>
    public void RenderState() {
        background.RenderEntity();
        levelManager.Render();
        points.Render();
        health.Render();
    }
    private void LoadLevels() {
        if (levelslst.Count == 0) { // No levels left to load.
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_WON"
            });
            return;
        } else if (LevelManager.EmptyLevel()) {
            // If level contains no blocks except unbreakable blocks
            levelslst.RemoveAt(0); // Removes current level from level list
            if (Levellst.Count > 0) // Shouldnt try to access index 0 in an empty list
                levelManager.NewLevel(Levellst[0]);
        }
    }
    /// <summary>
    ///  Updates the state and loads new levelslst when the level is empty
    /// </summary>
    public void UpdateState() {
        levelManager.Update();
        LoadLevels();
    }
    /// <summary>
    ///  Handles key input events such as key presses and key realising
    /// </summary>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE LEFT"
                });
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE RIGHT"
                });
                break;
            case KeyboardKey.Escape:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                });
                break;
            case KeyboardKey.K:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "CLEAR",
                });
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "RELEASE LEFT"
                });
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "RELEASE RIGHT"
                });
                break;
        }
    }
}
