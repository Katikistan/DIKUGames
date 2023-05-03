using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Levels;
using System.IO;
namespace Breakout.States;
public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private LevelManager levelManager = null!;
    private Points points = null!;
    private Entity background = null!;
    public int score = 0;
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
        levelManager.NewLevel("level1.txt");
        points = new Points();
    }
    public void ResetState() {
        GameRunning.instance = null;
    }
    public void RenderState() {
        background.RenderEntity();
        levelManager.Render();
        points.Render();
    }
    public void UpdateState() {
        levelManager.Update();
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
            case KeyboardKey.Space:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "GET POINTS",
                    IntArg1 = 100
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
                    Message = "REALESE LEFT"
                });
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "REALESE RIGHT"
                });
                break;
        }
    }
}
