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
        levels.Add("level1.txt");
        levels.Add("level2.txt");
        levels.Add("level3.txt");
        levelManager.NewLevel(levels[0]);
        points = new Points();
        health = new Health();
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, levelManager);
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
        if (levels.Count == 0) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "MAIN_MENU"
            });
            return;
        } else if (levelManager.EmptyLevel()) {
            levels.RemoveAt(0);
            if (levels.Count > 0)
                levelManager.NewLevel(levels[0]);
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
