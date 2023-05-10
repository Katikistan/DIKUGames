using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks;
public class Unbreakable : Block {
    /// <summary>
    /// Amount of points given to player when block is destroyed.
    /// </summary>
    public Unbreakable(Shape shape, string imageFile) : base(shape, imageFile) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    /// <summary>
    /// Decreases Block health, if health is less than 1 the block is marked for deletion.
    /// </summary>
    public override void LoseHealth() {}
}