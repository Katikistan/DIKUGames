using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout.Balls;
public class Ball : Entity {
    private DynamicShape shape;
    public DynamicShape _Shape {
        get {
            return shape;
        }
    }
    public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.shape = shape;
    }

    public void Move() {
        shape.Move();
    }

    public void Render() {
        this.RenderEntity();
    }
}



