using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
namespace Breakout;
public class Game : DIKUGame, IGameEventProcessor {
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        BreakoutBus.GetBus().InitializeEventBus(
            new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent
            });
        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        private Player Player;
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.StringArg1) {
                case "WINDOW_CLOSE":
                    window.CloseWindow();
                    break;
            }
        }
    }
    public override void Render() {

    }
    public override void Update() {

    }
}