using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

namespace Breakout.Timers;

public class Timer {
    private StaticTimer timer;
    private int timeLeft;
    private int timeElapsed;
    private int secondsElapsed;
    private int creationTime;
    private Text timerText;
    private Vec2F position;
    private Vec3I white;
    public int TimeLeft {
        get {return timeLeft;}
    }
    public Timer(Vec2F pos, int init) {
        timer = new StaticTimer();
        timeLeft = init;
        position = pos;
        secondsElapsed = 1;
        creationTime = (int)StaticTimer.GetElapsedMilliseconds();
        white = new Vec3I(255, 255, 255);
    }
    private void UpdateTime(){
        timeElapsed = (int)StaticTimer.GetElapsedMilliseconds();
        if ((creationTime + (secondsElapsed*1000)) < timeElapsed) {
            secondsElapsed++;
            timeLeft--;
        }
    }
    private void CreateText() {
        if (timeLeft > 0) {
            UpdateTime();
            timerText = new Text($"Time: {timeLeft}s",
            position, new Vec2F(0.25f, 0.35f));
            timerText.SetColor(white);
        }
        else {
            timerText = new Text($" ",
            position, new Vec2F(0.25f, 0.35f));
        }
    }
    public void Render() {
        CreateText();
        timerText.RenderText();
    }
}
