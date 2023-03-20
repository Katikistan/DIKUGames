using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
namespace galagaTests;

public class TestPlayer {
    private Player player;
    private GameEventBus eventbus;
    private Vec2F playerPos;
    [SetUp]
    public void Setup() {
        // måske lav en eventbus istedet
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        playerPos = player.GetPosition();
        // EventMoveRight = GalagaBus.GetBus().RegisterEvent(new GameEvent {
        //     EventType = GameEventType.PlayerEvent,
        //     Message = "MOVE Right"
        // });

        // GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
        // GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
    }
    [Test]
    public void TestMoveLeft() {
        // EventMoveLeft = GalagaBus.GetBus().RegisterEvent(new GameEvent {
        //     EventType = GameEventType.PlayerEvent,
        //     Message = "MOVE LEFT"
        // });
        // player.RegisterEvent(EventMoveLeft);
        // player.ProcessEvent(gameEvent.Message);
        // (work in progress, men samme format kan måske bruges andre steder)
        player.Move();
        Assert.IsEqual(playerPos.X == 0.44f);
    }


    public void TestMoveRight() {
        Assert.Pass();
    }
    public void TestRealeseLeft() {
        Assert.Pass();
    }
    public void TestRealeseRight() {
        Assert.Pass();
    }
    public void TestNotOutOfBoundsLeft() {
        Assert.Pass();
    }
    public void TestNotOutOfBoundsRight() {
        Assert.Pass();
    }
}