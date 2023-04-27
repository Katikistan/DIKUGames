using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Players;
namespace BreakoutTests.EntityTest;
[TestFixture]
public class TestPlayer {
    private GameEventBus TesteventBus = null!;
    private Player player = null!;
    private GameEvent eventMoveLeft;
    private GameEvent eventMoveRight;
    private GameEvent eventRealeseLeft;
    private GameEvent eventRealeseRight;
    private float startPosX;
    private float startPosY;
    private float movementSpeed;
    public TestPlayer() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        movementSpeed = 0.01f;
        startPosX = 0.425f;
        startPosY = 0.05f;
        TesteventBus = new GameEventBus();
        TesteventBus.InitializeEventBus(
            new List<GameEventType> { GameEventType.PlayerEvent });
        player = new Player (
            new DynamicShape(new Vec2F(startPosX, startPosY), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..","Breakout","Assets", "Images", "player.png")));
        TesteventBus.Subscribe(GameEventType.PlayerEvent, player);

        eventMoveLeft = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "MOVE LEFT"
        });
        eventMoveRight = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "MOVE RIGHT"
        });
        eventRealeseLeft = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "REALESE LEFT"
        });
        eventRealeseRight = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "REALESE RIGHT"
        });
    }
    [Test]
    public void TestGetPosition() {
        Vec2F playerPos = player.GetPosition();
        Assert.That((playerPos.X), Is.EqualTo(playerPos.X));
        Assert.That((playerPos.Y), Is.EqualTo(playerPos.Y));
    }
    [Test]
    public void TestMoveLeft() {
        TesteventBus.RegisterEvent(eventMoveLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestMoveRight() {
        TesteventBus.RegisterEvent(eventMoveRight);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
    }
    [Test]
    public void TestRealeseLeft() {
        TesteventBus.RegisterEvent(eventMoveLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        TesteventBus.RegisterEvent(eventRealeseLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestRealeseRight() {
        TesteventBus.RegisterEvent(eventMoveRight);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        TesteventBus.RegisterEvent(eventRealeseRight);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
    }
    [Test]
    public void TestNotOutOfBoundsLeft() {
        player = new Player(
            new DynamicShape(new Vec2F(0.0f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Breakout","Assets", "Images", "player.png")));

        TesteventBus.RegisterEvent(eventMoveLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(0.0f));
    }
    [Test]
    public void TestNotOutOfBoundsRight() {
        player = new Player(
            new DynamicShape(new Vec2F(1.0f - 0.1f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Breakout","Assets", "Images", "player.png")));
        TesteventBus.RegisterEvent(eventMoveRight);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(0.9f));
    }
    [Test]
    public void TestMoveLeftRight() {
        TesteventBus.RegisterEvent(eventMoveLeft);
        TesteventBus.RegisterEvent(eventMoveRight);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX));
    }
    [Test]
    public void TestMoveWrongLeft() {
        eventMoveLeft = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "move left"
        });
        TesteventBus.RegisterEvent(eventMoveLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX));
    }
}