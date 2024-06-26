using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using System.IO;
namespace Breakout.Powerups;
/// <summary>
/// Ball splits into 3 balls going different directions.
/// </summary>
public class Split : Powerup {
    public Split(DynamicShape shape) : base(shape, new Image(
        Path.Combine("..", "Breakout", "Assets", "Images", "SplitPowerUp.png"))) {
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "SPLIT",
        });
    }
}