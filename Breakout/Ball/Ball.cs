using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Balls;
public class Ball : Entity {

    // private Vec2F startpos;
    // public Vec2F Startpos {
    //     get {
    //         return startpos;
    //     }
    // }
    private DynamicShape shape;
    public DynamicShape _Shape {
        get {
            return shape;
        }
    }
    // private Vec2F direction;

    public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.shape = shape;
    }

    public void Move() {
        _Shape.Move();
    }

    public void Render(){
        this.RenderEntity();
        }
}



