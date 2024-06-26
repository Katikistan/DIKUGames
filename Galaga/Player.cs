using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga;
public class Player : IGameEventProcessor {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
    private Entity entity;
    private DynamicShape shape;
    // måske skal de have livet her istedet i game
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
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
                case "REALESE LEFT":
                    SetMoveLeft(false);
                    break;
                case "REALESE RIGHT":
                    SetMoveRight(false);
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
    public Vec2F GetPositionMiddle() {
        //Position adjusted to make bullets shot from middle of ship.
        Vec2F position = new Vec2F(shape.Position.X + shape.Extent.X / 2.0f, shape.Position.Y);
        return (position);
    }
    public Vec2F GetPosition() {
        //Position adjusted to make bullets shot from middle of ship.
        Vec2F position = new Vec2F(shape.Position.X, shape.Position.Y);
        return (position);
    }
    public void Render() {
        entity.RenderEntity();
    }
}
