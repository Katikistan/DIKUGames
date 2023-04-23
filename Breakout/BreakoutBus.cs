using DIKUArcade.Events;
namespace Breakout;
public static class BreakoutBus {
    private static GameEventBus ?eventBus;
    public static GameEventBus GetBus() {
        if (BreakoutBus.eventBus == null) {
           BreakoutBus.eventBus = new GameEventBus();
        }
        return BreakoutBus.eventBus;
    }
}

