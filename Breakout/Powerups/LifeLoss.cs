using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

namespace Breakout.Powerups;

public class LifeLoss : Powerup {
    public LifeLoss(DynamicShape shape, IBaseImage image) : base(shape, image) {
        
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "LIFE LOSS",
            ObjectArg1 = 1
        });
    }

}