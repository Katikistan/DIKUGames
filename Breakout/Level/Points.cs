using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Levels;
public class Points : IGameEventProcessor {
    private int points = 0;
    private Text pointText;
    private Vec3I red;
    public Points() {
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        pointText = new Text($"Points: {points}",
        new Vec2F(0.0f, -0.275f), new Vec2F(0.25f, 0.35f));
        red = new Vec3I(200, 0, 0);
        pointText.SetColor(red);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "GET POINTS":
                    points += gameEvent.IntArg1;
                    CreateText();
                    break;
                case "RESET POINTS":
                    points = gameEvent.IntArg1;
                    CreateText();
                    break;
            }
        }
    }
    public int GetPoints() {
        return points;
    }
    private void CreateText() {
        pointText = new Text($"Points: {points}",
        new Vec2F(0.0f, -0.275f), new Vec2F(0.25f, 0.35f));
        pointText.SetColor(red);
    }
    public void Render() {
        pointText.RenderText();
    }
}
