using DIKUArcade.Graphics;
using DIKUArcade.Math;
public class Health {
    private int health;
    public int Lives {
        get {return health;}
        }
    private Text display;
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text ("Lives: " + health.ToString(), position, extent);
        display.SetColor(new Vec3I(255, 255, 255));
    }
    /// <summary>
    /// Decrements the health field by one and updates Text
    /// such that is has the correct health value
    /// </summary>

    // static active state? til at kunne skifte til game over state bla.
    public void LoseHealth () {
        health -= 1;
        display.SetText("Lives:" + health.ToString());
    }
    public void RenderHealth () {
        display.RenderText();
    }
}