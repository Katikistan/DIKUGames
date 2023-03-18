using DIKUArcade.Events.Generic;
using DIKUArcade.Events;
namespace Galaga;
public static class GalagaBus {
    private static GameEventBus<GameEventType> eventBus;
    public static GameEventBus<GameEventType> GetBus() {
        return GalagaBus.eventBus ?? (GalagaBus.eventBus =
        new GameEventBus<GameEventType>());
    }
}

