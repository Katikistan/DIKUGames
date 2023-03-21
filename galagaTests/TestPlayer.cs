using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
namespace galagaTests;
[TestFixture]
public class TestPlayer {
    private GameEventBus TesteventBus = null!;
    private Player player = null!;
    private GameEvent EventMoveLeft;
    private GameEvent EventMoveRight;
    private GameEvent EventNothing;
    public TestPlayer() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    [SetUp]
    public void Setup() {
        TesteventBus = new GameEventBus();
        TesteventBus.InitializeEventBus(
            new List<GameEventType> { GameEventType.PlayerEvent });

        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));
        Vec2F playerPos = player.GetPosition();
       TesteventBus.Subscribe(GameEventType.PlayerEvent, player);

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
        TesteventBus.RegisterEvent(EventMoveLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.44f));
    }
    public void TestMoveRight() {
        TesteventBus.RegisterEvent(EventMoveRight);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.46f));
    }
    public void TestNotOutOfBoundsLeft() {
        player = new Player(
            new DynamicShape(new Vec2F(0.0f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));
            
        TesteventBus.RegisterEvent(EventMoveLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.0f));
    }
    public void TestNotOutOfBoundsRight() {
        player = new Player(
            new DynamicShape(new Vec2F(1.0f - 0.1f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));
            
        TesteventBus.RegisterEvent(EventMoveRight);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.9f));
    }
    public void TestMoveLeftRight() {
        TesteventBus.RegisterEvent(EventMoveLeft);
        TesteventBus.RegisterEvent(EventMoveRight);
        TesteventBus.ProcessEventsSequentially();
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
        TesteventBus.RegisterEvent(EventMoveLeft);
        TesteventBus.ProcessEventsSequentially();
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.That(playerPos.X,Is.EqualTo(0.45f));
    }
}