using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks;

public class Block : Entity {
    private int value;
    public int Value {
        get { return value; }
        set { Value = value; }
    }
    private bool isUnbreakable;
    public bool IsUnbreakable {
        get { return isUnbreakable; }
        set { isUnbreakable = value; }
    }
    private int health;
    private Vec2F position;
    public Block(DynamicShape shape, IBaseImage image) : base(shape, image) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    public void LoseHealth() {
        if (isUnbreakable == false) {
            health -= 1;
        }
    }
    public bool IsDead() {
        if (health <= 0 && isUnbreakable == false) {
            return true;
        } else {
            return false;   
        }
    }
}