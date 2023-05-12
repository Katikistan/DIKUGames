using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Levels;
using System.IO;
using System.Collections.Generic;
namespace Breakout.States;
public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private LevelManager levelManager = null!;
    private Points points = null!;
    private Health health;
    private Entity background = null!;
    private List<string> levels;
    // Public getters for testing
    public Health Health { get => health; }
    public Entity Background { get => background; }
    public List<string> Levels { get => levels; }
    public Points Points { get => points; }
    public LevelManager LevelManager { get => levelManager;  }
    public static GameRunning Instance { get => instance;}

    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }
    public void InitializeGameState() {
        background = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Breakout", "Assets", "Images", "SpaceBackground.png")));
        levelManager = new LevelManager();
        levels = new List<string>();
        Levels.Add("level1.txt");
        Levels.Add("level2.txt");
        Levels.Add("level3.txt");
        LevelManager.NewLevel(Levels[0]);
        points = new Points();
        health = new Health();
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, LevelManager);
    }
    public void ResetState() {
        GameRunning.instance = null;
    }
    public void RenderState() {
        background.RenderEntity();
        levelManager.Render();
        points.Render();
        health.Render();
    }
    private void LoadLevels() {
        if (levels.Count == 0) { // No levels left to load.
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "MAIN_MENU"
            });
            return;
        } else if (LevelManager.EmptyLevel()) {
            // If level contains no blocks except unbreakable blocks
            levels.RemoveAt(0); // Removes current level form level list
            if (Levels.Count > 0) // Shouldnt try to access index 0 in an empty list
                levelManager.NewLevel(Levels[0]);
        }
    }
    public void UpdateState() {
        levelManager.Update();
        LoadLevels();
    }

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
