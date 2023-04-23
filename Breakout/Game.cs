using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;

using Breakout.Players;
namespace Breakout;
public class Game : DIKUGame, IGameEventProcessor {
    private Players.Player player;
    private Entity background;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        background = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Breakout", "Assets", "Images", "SpaceBackground.png")));
        player = new Players.Player(
            new DynamicShape(new Vec2F(0.045f, 0.08f), new Vec2F(0.1f, 0.01f)),
            new Image(Path.Combine("..","Breakout","Assets", "Images", "player.png")));
        BreakoutBus.GetBus().InitializeEventBus(
            new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent
            });
        window.SetKeyEventHandler(HandleKeyEvent);
        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.StringArg1) {
                case "WINDOW_CLOSE":
                    window.CloseWindow();
                    break;
            }
        }
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
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE LEFT"
                });
                break;
            case KeyboardKey.Right:
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
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "REALESE LEFT"
                });
                break;
            case KeyboardKey.Right:
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "REALESE RIGHT"
                });
                break;
        }
    }
    public override void Render() {
        background.RenderEntity();
        player.Render();
    }
    public override void Update() {
        BreakoutBus.GetBus().ProcessEventsSequentially();
        player.Move();

    }
}