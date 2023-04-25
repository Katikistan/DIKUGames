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
    private bool unbreakable;
    public bool Unbreakable {
        get { return unbreakable; }
        set { unbreakable = value; }
    }
    private bool hardned;
    public bool Hardned {
        get { return hardned; }
        set { hardned = value; }
    }
    private int health = 1;
    private Vec2F position;
    private StationaryShape shape;
    public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    public void applyMeta() {
    }
    public void LoseHealth() {
        if (!Unbreakable) {
            health -= 1;
        }
        if (IsDead()) {
            DeleteEntity();
        }

    }
    public bool IsDead() {
        if (health <= 0) {
            return true;
        } else {
            return false;
        }
    }
    public void Render() {
        RenderEntity();
    }
}