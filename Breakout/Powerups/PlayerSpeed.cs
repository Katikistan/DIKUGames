using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using System.IO;
namespace Breakout.Powerups;
/// <summary>
/// When picked up, player movement speed increases by sending a PlayerEvent.
/// The powerup is timed and therefore temporary
/// </summary>
public class PlayerSpeed : Powerup {
    public PlayerSpeed(DynamicShape shape) : base(shape, new Image(
        Path.Combine("..", "Breakout", "Assets", "Images", "DoubleSpeedPowerUp.png"))) {
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SPEED",
            StringArg1 = "START"
        });
        BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "SPEED",
            StringArg1 = "END"
        }, TimePeriod.NewSeconds(10.0));
    }
}