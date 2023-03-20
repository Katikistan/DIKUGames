using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
namespace galagaTests;
[TestFixture]
public class TestPlayer {
    private Player player;
    private GameEvent EventMoveLeft;
    private GameEvent EventMoveRight;
    private GameEvent EventNothing;
    [SetUp]
    public void Setup() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        GalagaBus.GetBus().InitializeEventBus(
            new List<GameEventType> { GameEventType.PlayerEvent });
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));
        Vec2F playerPos = player.GetPosition();
        GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        
        EventMoveLeft = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "MOVE LEFT"
        });
        EventMoveRight = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "MOVE RIGHT"
        });
        EventNothing = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = ""
        });
    }
    [Test]
    public void TestMoveLeft() {
        GalagaBus.GetBus().RegisterEvent(EventMoveLeft);
        GalagaBus.GetBus().ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.44f));
    }
    public void TestMoveRight() {
        GalagaBus.GetBus().RegisterEvent(EventMoveRight);
        GalagaBus.GetBus().ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.46f));
    }
    public void TestNotOutOfBoundsLeft() {
        player = new Player(
            new DynamicShape(new Vec2F(0.0f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));
        GalagaBus.GetBus().RegisterEvent(EventMoveLeft);
        GalagaBus.GetBus().ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.0f));
    }
    public void TestNotOutOfBoundsRight() {
        player = new Player(
            new DynamicShape(new Vec2F(1.0f - 0.1f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));
        GalagaBus.GetBus().RegisterEvent(EventMoveRight);
        GalagaBus.GetBus().ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.9f));
    }
    public void TestMoveLeftRight() {
        GalagaBus.GetBus().RegisterEvent(EventMoveLeft);
        GalagaBus.GetBus().RegisterEvent(EventMoveRight);
        GalagaBus.GetBus().ProcessEventsSequentially();
        player.Move();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.45f));
    }
    public void TestMoveWrongLeft() {
        EventMoveLeft = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "move left"
        });
        GalagaBus.GetBus().RegisterEvent(EventMoveLeft);
        GalagaBus.GetBus().ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.45f));
    }
}