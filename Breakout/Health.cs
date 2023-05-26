using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
namespace Breakout;
public class Health : IGameEventProcessor {
    private int health;
    private Text display;

    public int _Health {
        get => health;
    }

    public Health() {
        health = 3;
        display = new Text($"Lives: {health}", new Vec2F(0.8f, -0.275f), new Vec2F(0.25f, 0.35f));
        display.SetColor(new Vec3I(255, 255, 255));
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "LOSE HEALTH":
                    LoseHealth();
                    display.SetText("Lives:" + health.ToString());
                    break;
                case "GET HEALTH":
                    health += gameEvent.IntArg1;
                    display.SetText("Lives:" + health.ToString());
                    break;
            }
        }
    }
    /// <summary>
    /// Decrements the health field by one and updates Text
    /// if health is 0 then state switches to game lost.
    /// </summary>
    public void LoseHealth() {
        health -= 1;
        if (health <= 0) {
            health = 0;
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_LOST"
            });
        }
    }
    public void Render() {
        display.RenderText();
    }
}