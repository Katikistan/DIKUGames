using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
namespace Breakout.Blocks;
/// <summary>
/// An abstract block type
/// </summary>
public abstract class Block : Entity {
    protected int value = 10;
    /// <summary>
    /// Amount of points given to player when block is destroyed.
    /// </summary>
    public int Value {
        get {
            return value;
        }
    }
    protected int health = 1;
    public int Health {
        get {
            return health;
        }
    }
    protected Vec2F position;
    public Block(Shape shape, string imageFile) :
    base(shape, new Image(
        Path.Combine("..", "Breakout", "Assets", "Images", imageFile))) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    /// <summary>
    /// Decreases Block health, if health is less than 1 the block is marked for deletion.
    /// </summary>
    public abstract void LoseHealth(int amount);
    protected void GivePoints() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET POINTS",
            IntArg1 = value
        });
    }
    public void Render() {
        RenderEntity();
    }
}