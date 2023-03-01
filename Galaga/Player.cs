using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga;
public class Player {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
    

    private Entity entity;
    private DynamicShape shape;
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;

    }
    public void Move() {
        shape.Move();
        if (shape.Position.X <= 0.0f) {
            shape.Position.X = 0.0f; 
        }
        else if ((shape.Position.X + shape.Extent.X) >= 1.0f) {
                shape.Position.X = 1.0f - shape.Extent.X;
        }
 
    }
    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = -MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }
    public void SetMoveRight(bool val) {
        if (val) {
            moveRight = MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        }
        UpdateDirection();
    }
    public Vec2F GetPosition() {
        return shape.Position;
    } 
    
    public void Render() {
        entity.RenderEntity();    
    }
}
