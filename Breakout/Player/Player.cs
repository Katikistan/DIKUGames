using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Players;
/// <summary>
///  A player entity that can move around
/// </summary>
public class Player : Entity, IGameEventProcessor {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private float movementSpeed = 0.01f;
    private DynamicShape shape;
    public DynamicShape _Shape {
        get {
            return shape;
        }
    }
    public float MovementSpeed {
        get => movementSpeed; set => movementSpeed = value;
    }

    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.shape = base.Shape.AsDynamicShape();
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
    }
    /// <summary>
    ///  processes playerEvents such as power-ups and movement events
    /// </summary>
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
                        shape.Extent.X = 0.075f;
                    } else if (gameEvent.StringArg1 == "END") {
                        shape.Extent.X = 0.15f;
                    }
                    break;
                case "WIDE":
                    if (gameEvent.StringArg1 == "START") {
                        shape.Extent.X = 0.30f;
                    } else if (gameEvent.StringArg1 == "END") {
                        shape.Extent.X = 0.15f;
                    }
                    break;
                case "SPEED":
                    if (gameEvent.StringArg1 == "START") {
                        movementSpeed = 0.02f;
                    } else if (gameEvent.StringArg1 == "END") {
                        movementSpeed = 0.01f;
                    }
                    break;
            }
        }
    }
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }
    /// <summary>
    ///  Moves player based on directional vector
    /// </summary>
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
            moveLeft = -movementSpeed;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }
    private void SetMoveRight(bool val) {
        if (val) {
            moveRight = movementSpeed;
        } else {
            moveRight = 0.0f;
        }
        UpdateDirection();
    }
    /// <summary>
    ///  Gets vector position of the player
    /// </summary>
    public Vec2F GetPosition() {
        Vec2F position = new Vec2F(shape.Position.X, shape.Position.Y);
        return (position);
    }
    /// <summary>
    ///  Renders the player
    /// </summary>
    public void Render() {
        RenderEntity();
    }
}
