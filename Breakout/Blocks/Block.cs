using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks;

public class Block : Entity {
    private int value;
    private int health;
    private Vec2F position;
    public Block(DynamicShape shape, IBaseImage image) : base(shape, image) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    public void LoseHealth() {
        health -= 1;
    }
    public bool IsDead() {
        if (health <= 0) {
            return true;
        } else {
            return false;   
        }
    }
}