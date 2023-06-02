using DIKUArcade.Entities;
using DIKUArcade.Events;
namespace Breakout.Blocks;
/// <summary>
/// Has 1 health points and grants player 10 points when destroyed and drops a powerup/hazard.
/// </summary>
public class PowerupBlock : Block {
    public PowerupBlock(Shape shape, string imageFile) : base(shape, imageFile) {
    }
    /// <summary>
    /// Decreases Block health, if health is 0 the block is marked for deletion
    /// and a power-up will be created
    /// </summary>
    public override void LoseHealth(int amount) {
        health -= amount;
        if (health == 0) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "SPAWN POWERUP",
            ObjectArg1 = (object)Shape.Position
            });
            DeleteEntity();
            GivePoints();
        }
    }
}