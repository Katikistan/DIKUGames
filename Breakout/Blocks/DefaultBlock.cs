using DIKUArcade.Entities;
namespace Breakout.Blocks;
/// <summary>
/// Default block has 1 health points and grants player 10 points when destroyed.
/// </summary>
public class DefaultBlock : Block {
    public DefaultBlock(Shape shape, string imageFile) : base(shape, imageFile) {
    }
    public override void LoseHealth() {
        health -= 1;
        if (health == 0) {
            DeleteEntity();
            GivePoints();
        }
    }
}