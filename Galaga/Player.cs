using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Galaga;
public class Player {
    // TODO: Add private fields
    private Entity entity;
    private DynamicShape shape;
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }
    public void Move() {
    // TODO: move the shape and guard against the window borders
    }
    public void SetMoveLeft(bool val) {
    // TODO:set moveLeft appropriately and call UpdateDirection()
    }
    public void SetMoveRight(bool val) {
    // TODO:set moveRight appropriately and call UpdateDirection()
    }
    public void Render() {
        entity.RenderEntity();    
    }
}
