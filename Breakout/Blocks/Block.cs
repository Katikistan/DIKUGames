using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Blocks;
public abstract class Block : Entity {
    internal int value = 100;
    public int Value { get { return value; } }
    internal int health = 1;
    public int Health { get { return health; } }
    internal Vec2F position; // why safe the pos

    public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    public void LoseHealth() {
        health -= 1;
        if (health <= 0) {
            DeleteEntity();
        }
    }
    public void Render() {
        RenderEntity();
    }
}