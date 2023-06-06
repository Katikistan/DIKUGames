using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using System.IO;
namespace Breakout.Powerups;
/// <summary>
/// When picked up, player size is decreased by sending out a PlayerEvent.
/// The powerup is timed and therefore temporary
/// </summary>
public class SlimJim : Powerup {
    public SlimJim(DynamicShape shape) : base(shape, new Image(
        Path.Combine("..", "Breakout", "Assets", "Images", "SlimJim.png"))) {
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SLIM JIM",
            StringArg1 = "START"
        });
        BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SLIM JIM",
            StringArg1 = "END"
        }, TimePeriod.NewSeconds(10.0));
    }
}