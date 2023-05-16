using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

namespace Breakout.Timers;

public class Timer : IGameEventProcessor {
    private StaticTimer timer;
    private int timeLeft;
    private int timePassed;
    private Text timerText;
    private Vec2F position;
    private Vec3I white;
    public Timer(Vec2F pos) {
        timer = new StaticTimer();
        timeLeft = n;
        position = pos;
        white = new Vec3I(255, 255, 255);
    }
    private void CreateText() {
        timePassed = (int)StaticTimer.GetElapsedSeconds();
        timeLeft -= timePassed;
        timerText = new Text($"{timeLeft} s",
        position, new Vec2F(0.25f, 0.35f));
        timerText.SetColor(white);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.TimedEvent) {
            switch (gameEvent.Message) {
                case "LEVEL_TIME":
                    n = gameEvent.StrinArg1;
                    if (n != "") {
                        CreateText();
                    }
                    break;
            }
        }
    }
    public void Render() {
        timerText.RenderText();
    }
}
