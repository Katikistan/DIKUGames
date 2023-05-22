using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

namespace Breakout.Powerups;

public class PowerUpBus : GameEventBus, IGameEventProcessor {


    public PowerUpBus() {
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "SPAWN POWERUP":
                    break;
            }
        }
    }
}