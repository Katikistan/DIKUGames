using Breakout;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace BreakoutTests.Unit.EntityTests;
[TestFixture]
public class PlayerTests {
    private Player player = null!;
    private Vec2F playerPos = null!;
    private GameEvent eventMoveLeft;
    private GameEvent eventMoveRight;
    private GameEvent eventRealeseLeft;
    private GameEvent eventRealeseRight;
    private float startPosX;
    private float startPosY;
    private float movementSpeed;
    public PlayerTests() {
        CreateGL.CreateOpenGL();
    }
    [SetUp]
    public void Setup() {
        movementSpeed = 0.01f;
        startPosX = 0.425f;
        startPosY = 0.05f;
        player = new Player(
            new DynamicShape(new Vec2F(startPosX, startPosY), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

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
        player.ProcessEvent(eventMoveLeft);
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestMoveRight() {
        player.ProcessEvent(eventMoveRight);
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
    }
    [Test]
    public void TestRealeseLeft() {
        player.ProcessEvent(eventMoveLeft);
        player.Move();
        playerPos = player.GetPosition();
        // Player moved to the left
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
        // Left key is released
        player.ProcessEvent(eventRealeseLeft);
        player.Move();
        // Player pos is the same/hasn't moved
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
    }
    [Test]
    public void TestRealeseRight() {
        player.ProcessEvent(eventMoveRight);
        player.Move();
        playerPos = player.GetPosition();
        // Player moved to the right
        Assert.That(playerPos.X, Is.EqualTo(startPosX + movementSpeed));
        // Right key is released
        player.ProcessEvent(eventRealeseRight);
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
        player.ProcessEvent(eventMoveLeft);
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
        player.ProcessEvent(eventMoveRight);
        player.Move();
        player.Move();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X + player.Shape.Extent.X, Is.LessThanOrEqualTo(1.0f));
    }
    [Test]
    public void TestMoveLeftRight() {
        player.ProcessEvent(eventMoveLeft);
        player.ProcessEvent(eventMoveRight);
        // Both left and right "keys" are pressed
        player.Move();
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX));
    }
    [Test]
    public void TestMoveLeftRight2() {
        player.ProcessEvent(eventMoveLeft);
        // Player moves to the left
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX - movementSpeed));
        // Player realeses left key
        player.ProcessEvent(eventRealeseLeft);
        // Player presses right key
        player.ProcessEvent(eventMoveRight);
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
        player.ProcessEvent(eventMoveLeft);
        player.Move();
        playerPos = player.GetPosition();
        Assert.That(playerPos.X, Is.EqualTo(startPosX));
    }
}