using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;
/// <summary>
/// Default block has 1 health points and grants player 100 points when destroyed.
/// </summary>
public class DefaultBlock : Block {
    public DefaultBlock(StationaryShape shape, string imageFile) : base(shape, imageFile) {
    }
    public override void LoseHealth() {
        health -= 1;
        if (health == 0) {
            DeleteEntity();
            GivePoints();
        }
    }
}