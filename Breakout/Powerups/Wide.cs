using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Timers;
namespace Breakout.Powerups;

public class Wide : Powerup {
    public Wide(DynamicShape shape) : base(shape, new Image (
        Path.Combine("..", "Breakout", "Assets", "Images", "WidePowerUp.png"))) {
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "Wide",
            StringArg1 = "START"
        });
        BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "Wide",
            StringArg1 = "END"
            }, TimePeriod.NewSeconds(10.0));
    }
}