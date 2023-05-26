using DIKUArcade.Events;
using System.Collections.Generic;
namespace Breakout;
public static class BreakoutBus {
    private static GameEventBus eventBus;
    public static GameEventBus GetBus() {
        if (BreakoutBus.eventBus == null) {
           BreakoutBus.eventBus = new GameEventBus();
            BreakoutBus.GetBus().InitializeEventBus(
                new List<GameEventType> {
                    GameEventType.InputEvent,
                    GameEventType.WindowEvent,
                    GameEventType.PlayerEvent,
                    GameEventType.GameStateEvent,
                    GameEventType.StatusEvent
                });
        }
        return BreakoutBus.eventBus;
    }
}

