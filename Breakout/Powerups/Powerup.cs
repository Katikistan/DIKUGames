using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout.Powerups;
/// <summary>
/// An abstract powerup which allows powerups to move and activate an effect.
/// </summary>
public abstract class Powerup : Entity {
    public Powerup(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }
    /// <summary>
    /// Moves the powerup
    /// </summary>
    public void Move() {
        this.Shape.Move();
        if (Shape.Position.Y <= 0.0 - Shape.Extent.Y) {
            DeleteEntity();
        }
    }
    /// <summary>
    /// Activates powerup effect, by sending a GameEvent.
    /// </summary>
    public abstract void Effect();
}