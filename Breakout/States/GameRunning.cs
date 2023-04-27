using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Levels;
using Breakout.Players;

namespace Breakout.States;
public class GameRunning : IGameState {
    private static GameRunning ?instance = null;
    private Player player = null!;
    private Level level = null!;
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
        player = new Player (
            new DynamicShape(new Vec2F(0.425f, 0.05f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..","Breakout","Assets", "Images", "player.png")));
        // lvl liste
        level = new Level(); //lvllst][0]
        level.NewLevel("central-mass.txt");
    }
    public void ResetState() {
        GameRunning.instance = null;
    }
    public void RenderState() {
        background.RenderEntity();
        player.Render();
        level.blocks.RenderEntities();
    }
    /// <summary>
    /// </summary>
    private void IterateBlocks() {
        level.blocks.Iterate(block => {
            if (block.IsDeleted()) {
                score += block.Value;
            }
        });
    }
    public void UpdateState() {
        player.Move();
        IterateBlocks();
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
                    EventType = GameEventType.WindowEvent,
                    StringArg1 = "WINDOW_CLOSE"
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
