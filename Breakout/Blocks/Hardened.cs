using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
namespace Breakout.Blocks;
/// <summary>
/// Block has 2 health points and grants player 20 points when destroyed.
/// </summary>
public class Hardened : Block {
    private string DamagedImg;
    private string Damaged;
    public Hardened(Shape shape, string imageFile) : base(shape, imageFile) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
        DamagedImg = imageFile.Insert(imageFile.Length - 4, "-damaged");
        Damaged = Path.Combine("..", "Breakout", "Assets", "Images", DamagedImg);
        health = 2;
        value = 20;
    }
    /// <summary>
    /// Decreases Block health, if health is 0 the block is marked for deletion
    /// </summary>
    public override void LoseHealth(int amount) {
        health -= amount;
        if (health == 1 && File.Exists(Damaged)) {
            Image = new Image(Damaged);
        } else if (health == 0) {
            DeleteEntity();
            GivePoints();
        }
    }
}