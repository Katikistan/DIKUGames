using DIKUArcade.Events;
using System.Collections.Generic;
namespace Breakout;
/// <summary>
/// A static eventBus used for registering and handling gameEvents
/// </summary>
public static class BreakoutBus {
    private static GameEventBus eventBus;
    /// <summary>
    /// Retrieves the static Eventbus, allowing for use of eventBus methods.
    /// </summary>
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

