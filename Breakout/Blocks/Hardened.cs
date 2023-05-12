using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Blocks;
public class Hardened : Block {
    private string DamagedImg;
    private string Damaged;
    /// <summary>
    /// Hardened block has 2 health points and grants player 20 points when destroyed.
    /// </summary>
    public Hardened(Shape shape, string imageFile) : base(shape, imageFile) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
        DamagedImg = imageFile.Insert(imageFile.Length - 4, "-damaged");
        Damaged = Path.Combine("..", "Breakout", "Assets", "Images", DamagedImg);
        health = 2;
        value = 20;
    }
    /// <summary>
    /// Decreases Block health, if health is less than 1 the block is marked for deletion.
    /// </summary>
    public override void LoseHealth() {
        health -= 1;
        if (health == 1 && File.Exists(Damaged)) {
            Image = new Image(Damaged);
        } else if (health == 0) {
            DeleteEntity();
            GivePoints();
        }
    }
}