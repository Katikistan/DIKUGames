using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Blocks;
public class  Powerup : Block {
    /// <summary>
    /// Hardened block has 2 health points and grants player 20 points when destroyed.
    /// </summary>
    public Powerup(Shape shape, string imageFile) : base(shape, imageFile) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
    }
    /// <summary>
    /// Decreases Block health, if health is less than 1 the block is marked for deletion.
    /// </summary>
    public override void LoseHealth() {
        health -= 1;
        if (health == 0) {
            DeleteEntity();
            GivePoints();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "CREATE POWERUP",
                // idk hvordan vi sender blockens pos til powerupgeneratoren
        });
        }
    }

}