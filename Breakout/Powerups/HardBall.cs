using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using System.IO;
namespace Breakout.Powerups;
/// <summary>
/// Balls destroy blocks regardless of health and don't change direction when hitting blocks.
/// The powerup is timed and therefore temporary
/// </summary>
public class HardBall : Powerup {
    public HardBall(DynamicShape shape) : base(shape, new Image(
        Path.Combine("..", "Breakout", "Assets", "Images", "ExtraBallPowerUp.png"))) {
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "HARD BALL",
            StringArg1 = "START"
        });
        BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "HARD BALL",
            StringArg1 = "END"
        }, TimePeriod.NewSeconds(10.0));
    }
}