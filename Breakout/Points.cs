using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout;
/// <summary>
/// In-game points to be rendered on screen.
/// </summary>
public class Points : IGameEventProcessor {
    private static Points instance = null;
    private int points = 0;
    private Text pointText;
    private Vec3I white;
    public Points() {
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        pointText = new Text($"Points: {points}",
            new Vec2F(0.4f, -0.285f), new Vec2F(0.25f, 0.35f));
        white = new Vec3I(255, 255, 255);
        pointText.SetColor(white);
    }
    /// <summary>
    /// Retrieves or creates and instance of points
    /// </summary>
    public static Points getInstance() {
        if (Points.instance == null) {
            Points.instance = new Points();
        }
        return Points.instance;
    }
    /// <summary>
    /// Procceses statusevents to get points
    /// </summary>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "GET POINTS":
                    points += gameEvent.IntArg1;
                    UpdateText();
                    break;
            }
        }
    }
    /// <summary>
    /// Resets point score
    /// </summary>
    public void ResetPoints() {
        Points.instance = null;
    }
    /// <summary>
    /// Gets the point score
    /// </summary>
    public int GetPoints() {
        return points;
    }
    /// <summary>
    /// Updates points text to be rendered on screen.
    /// </summary>
    private void UpdateText() {
        pointText.SetText($"Points: {points}");
    }
    /// <summary>
    /// Renders Points on screen.
    /// </summary>
    public void Render() {
        pointText.RenderText();
    }
}
