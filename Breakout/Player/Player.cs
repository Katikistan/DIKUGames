using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Players;
public class Player : Entity, IGameEventProcessor {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
    private DynamicShape shape;
    public DynamicShape _Shape {
        get {
            return shape;
        }
    }
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.shape = base.Shape.AsDynamicShape();
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch (gameEvent.Message) {
                case "MOVE LEFT":
                    SetMoveLeft(true);
                    break;
                case "MOVE RIGHT":
                    SetMoveRight(true);
                    break;
                case "RELEASE LEFT":
                    SetMoveLeft(false);
                    break;
                case "RELEASE RIGHT":
                    SetMoveRight(false);
                    break;
                case "SLIM JIM":
                    if (gameEvent.StringArg1 == "START") {
                        shape.Extent.X = shape.Extent.X / 2;
                    } else if (gameEvent.StringArg1 == "END") {
                        shape.Extent.X = shape.Extent.X * 2;
                    }
                    break;
            }
        }
    }
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }
    public void Move() {
        shape.Move();
        if (shape.Position.X <= 0.0f) {
            shape.Position.X = 0.0f;
        } else if ((shape.Position.X + shape.Extent.X) >= 1.0f) {
            shape.Position.X = 1.0f - shape.Extent.X;
        }
    }
    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = -MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }
    private void SetMoveRight(bool val) {
        if (val) {
            moveRight = MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        }
        UpdateDirection();
    }
    public Vec2F GetPosition() {
        Vec2F position = new Vec2F(shape.Position.X, shape.Position.Y);
        return (position);
    }
    public void Render() {
        RenderEntity();
    }
}
