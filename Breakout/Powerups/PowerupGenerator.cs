using DIKUArcade.Events;
namespace Breakout.Powerups;
public class PowerUpGenerator : IGameEventProcessor {
    public IPowerup GivePowerup(shape) {
        int random = 1;
        switch (random) {
            case 1:
                return new Extralife(shape,image);
        }

    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "GET POWERUP":
                    GivePowerup();
                    break;
            }
        }
    }
}

