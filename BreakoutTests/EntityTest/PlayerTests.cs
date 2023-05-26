using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Players;
using Breakout;
namespace BreakoutTests.EntityTest;
[TestFixture]
public class TestPlayer {
    private GameEventBus testEventBus = null!;
    private Player player = null!;
    private Vec2F playerPos = null!;
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
        testEventBus = new GameEventBus();
        testEventBus.InitializeEventBus(
            new List<GameEventType> { GameEventType.PlayerEvent });
        player = new Player(
            new DynamicShape(new Vec2F(startPosX, startPosY), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        testEventBus.Subscribe(GameEventType.PlayerEvent, player);

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
            Message = "RELEASE LEFT"
        });
        eventRealeseRight = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "RELEASE RIGHT"
        });
    }
    [Test]
    public void TestPlayerIsCentered() {
        playerPos = player.GetPosition();
        Assert.That(playerPos.X + (player.Shape.Extent.X / 2f), Is.EqualTo(0.5));
    }
    [Test]
    public void TestMoveLeft() {
        testEventBus.RegisterEvent(eventMoveLeft);
        testEventBus.ProcessEvents();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestMoveRight() {
        testEventBus.RegisterEvent(eventMoveRight);
        testEventBus.ProcessEvents();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
    }
    [Test]
    public void TestRealeseLeft() {
        testEventBus.RegisterEvent(eventMoveLeft);
        testEventBus.ProcessEvents();
        player.Move();
        playerPos = player.GetPosition();
        // Player moved to the left
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
        // Left key is released
        testEventBus.RegisterEvent(eventRealeseLeft);
        testEventBus.ProcessEvents();
        player.Move();
        // Player pos is the same/hasn't moved
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestRealeseRight() {
        testEventBus.RegisterEvent(eventMoveRight);
        testEventBus.ProcessEvents();
        player.Move();
        playerPos = player.GetPosition();
        // Player moved to the right
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
        // Right key is released
        testEventBus.RegisterEvent(eventRealeseRight);
        testEventBus.ProcessEvents();
        player.Move();
        playerPos = player.GetPosition();
        // Player pos is the same/hasn't moved
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
    }
    [Test]
    public void TestNotOutOfBoundsLeft() {
        player = new Player(
            new DynamicShape(new Vec2F(0.0f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        testEventBus.RegisterEvent(eventMoveLeft);
        testEventBus.ProcessEvents();
        player.Move();
        player.Move();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.GreaterThanOrEqualTo(0.0f));
    }
    [Test]
    public void TestNotOutOfBoundsRight() {
        player = new Player(
            new DynamicShape(new Vec2F(1.0f - 0.1f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        testEventBus.RegisterEvent(eventMoveRight);
        testEventBus.ProcessEvents();
        player.Move();
        player.Move();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X + player.Shape.Extent.X, Is.LessThanOrEqualTo(1.0f));
    }
    [Test]
    public void TestMoveLeftRight() {
        testEventBus.RegisterEvent(eventMoveLeft);
        testEventBus.RegisterEvent(eventMoveRight);
        testEventBus.ProcessEvents();
        // Both left and right "keys" are pressed
        player.Move();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX));
    }
    [Test]
    public void TestMoveLeftRight2() {
        testEventBus.RegisterEvent(eventMoveLeft);
        testEventBus.ProcessEvents();
        // Player moves to the left
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
        // Player realeses left key
        testEventBus.RegisterEvent(eventRealeseLeft);
        // Player presses right key
        testEventBus.RegisterEvent(eventMoveRight);
        testEventBus.ProcessEvents();
        player.Move();
        playerPos = player.GetPosition();
        // Player moved left and right back to start
        Assert.That(playerPos.X, Is.EqualTo(startPosX));
    }
    [Test]
    public void TestMoveWrongLeft() {
        eventMoveLeft = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "move left"
        });
        testEventBus.RegisterEvent(eventMoveLeft);
        testEventBus.ProcessEvents();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX));
    }
}