using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

namespace Breakout.Timers;

public class Timer {
    private int timeLeft;
    private Text timerText;
    private int timeElapsed;
    private int previousTime;
    private Vec2F position;
    private Vec3I white;
    public int TimeLeft {
        get {return timeLeft;}
    }
    public Timer(Vec2F pos, int init) {
        timeLeft = init;
        position = pos;
        white = new Vec3I(255, 255, 255);
        timerText = new Text($"Time: {timeLeft}s", 
        position, new Vec2F(0.25f, 0.35f));
        timerText.SetColor(white);
        timeElapsed = 0;
    }
    public void SetTime(int s) {
        timeLeft = s;
    }
    private void UpdateTime(){
        previousTime = timeLeft;
        timeElapsed = (int)StaticTimer.GetElapsedMilliseconds();
        if (1000 < timeElapsed) {
            timeLeft--;
            StaticTimer.RestartTimer();
        }
    }
    private void UpdateText() {
        if (timeLeft > 0) {
            UpdateTime();
            if (previousTime != timeLeft){
                timerText.SetText($"Time: {timeLeft}s");
            }
        }
        else {
            timerText.SetText("");
        }
    }
    public void Render() {
        UpdateText();
        timerText.RenderText();
    }
}
