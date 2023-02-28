using DIKUArcade.Entities;
using DIKUArcade.Graphics;
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
        if (shape.Position.X > 0 && (shape.Position.X + shape.Extent.X) < 1) {
            shape.Move();
        }
        
    }
    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = -MOVEMENT_SPEED;
            UpdateDirection();
        }
        else {
            moveLeft = 0.0f;
        }
    }
    public void SetMoveRight(bool val) {
        if (val) {
            moveRight = MOVEMENT_SPEED;
            UpdateDirection();
        }
        else {
            moveRight = 0.0f;
        }
    }
    
    public void Render() {
        entity.RenderEntity();    
    }
}
