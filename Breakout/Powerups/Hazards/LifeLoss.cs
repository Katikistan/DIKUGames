using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using System.IO;
namespace Breakout.Powerups;
/// <summary>
/// When picked up, the powerup sends and event decreasing health by 1
/// </summary>
public class LifeLoss : Powerup {
    public LifeLoss(DynamicShape shape) : base(shape, new Image(
        Path.Combine("..", "Breakout", "Assets", "Images", "LoseLife.png"))) {
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "LOSE HEALTH",
        });
    }
}