using DIKUArcade.Entities;
using DIKUArcade.Math;
namespace Breakout.Blocks;
/// <summary>
/// Unbreakable block, can't be destroyed
/// </summary>
public class Unbreakable : Block {
    public Unbreakable(Shape shape, string imageFile) : base(shape, imageFile) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    /// <summary>
    /// Does nothing, beacuse unbreakable blocks can't lose health and die
    /// </summary>
    public override void LoseHealth(int amount) {
    }
}