using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Levels;
public class Points : IGameEventProcessor {
    private int points = 0;
    private Text pointText;
    private Vec3I white;
    public Points() {
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        pointText = new Text($"Points: {points}",
            new Vec2F(0.0f, -0.275f), new Vec2F(0.25f, 0.35f));
        white = new Vec3I(255, 255, 255);
        pointText.SetColor(white);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "GET POINTS":
                    points += gameEvent.IntArg1;
                    UpdateText();
                    break;
                case "RESET POINTS":
                    points = 0;
                    UpdateText();
                    break;
            }
        }
    }
    public int GetPoints() {
        return points;
    }
    private void UpdateText() {
        pointText.SetText($"Points: {points}");
    }
    public void Render() {
        pointText.RenderText();
    }
}
