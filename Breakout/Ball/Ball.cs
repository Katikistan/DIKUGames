using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout.Balls;
/// <summary>
/// A ball entity that can move.
/// Static collision classes can be used to detect collisions with the ball and objects
 /// </summary>
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
    /// <summary>
    /// Moves the ball along it's directional vector contained in shape.
    /// </summary>
    public void Move() {
        shape.Move();
    }

    public void Render() {
        this.RenderEntity();
    }
}



