using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout.States;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Powerups;

public class LifePlus : Powerup {
    public LifePlus(DynamicShape shape, IBaseImage image) : base(shape, image) {
        
    }
    public override void Effect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "LIFE PLUS",
            IntArg1 = 1
        });
    }

}