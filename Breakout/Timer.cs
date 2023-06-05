using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout;
public class Timer {
    private int timeLeft;
    private Text timerText;
    private int timeElapsed;
    private int previousTime;
    private int n;
    private Vec2F position;
    private Vec3I white;
    public int TimeLeft {
        get => timeLeft;
    }
    public Timer(Vec2F pos, int init) {
        timeLeft = init;
        position = pos;
        white = new Vec3I(255, 255, 255);
        timerText = new Text($"Time: {timeLeft}s",
        position, new Vec2F(0.25f, 0.35f));
        timerText.SetColor(white);
        timeElapsed = 0;
        n = 0;
    }
    /// <summary>
    /// Sets the time left to an input value
    /// </summary>
    public void SetTime(int s) {
        timeLeft = s;
    }
    /// <summary>
    /// Updates the time and decrements the amount of seconds if a second has passed
    /// </summary>
    private void UpdateTime() {
        previousTime = timeLeft;
        timeElapsed = (int) StaticTimer.GetElapsedMilliseconds();
        if (n + 1000 < timeElapsed) {
            timeLeft--;
            n = (int) StaticTimer.GetElapsedMilliseconds();
        }
    }
    /// <summary>
    /// Updates the text for the timer.
    /// </summary>
    private void UpdateText() {
        if (timeLeft > 0) {
            UpdateTime();
            if (previousTime != timeLeft) {
                timerText.SetText($"Time: {timeLeft}s");
            }
        } else {
            timerText.SetText("");
        }
    }
    /// <summary>
    /// Renders the timer on the screen.
    /// </summary>
    public void Render() {
        UpdateText();
        timerText.RenderText();
    }
}
