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
    [SetUp]
    public void Setup() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));
        Vec2F playerPos = player.GetPosition();
        EventMoveLeft = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "MOVE LEFT"
        });
        EventMoveRight = (new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "MOVE Right"
        });
    }
    [Test]
    public void TestMoveLeft() {
        GalagaBus.GetBus().RegisterEvent(EventMoveLeft);
        player.ProcessEvent(EventMoveLeft);
        player.Move();
        Vec2F playerPos = player.GetPosition();
        Assert.AreEqual(playerPos.X, 0.44f);
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