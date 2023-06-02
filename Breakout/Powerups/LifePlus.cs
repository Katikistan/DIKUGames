using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using System.IO;
namespace Breakout.Powerups;
public class LifePlus : Powerup {
    public LifePlus(DynamicShape shape) :
        base(shape, new Image(
            Path.Combine("..", "Breakout", "Assets", "Images", "LifePickUp.png"))) {
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GET HEALTH",
            IntArg1 = 1
        });
    }
}