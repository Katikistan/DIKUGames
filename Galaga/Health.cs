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
        display = new Text ("Lives:" + health.ToString(), position, extent);
        display.SetColor(new Vec3I(255, 255, 255));
       
    }

    public void LoseHealth () {
       health -= 1;
       display.SetText("Lives:" + health.ToString());
    }
    public void RenderHealth () {
        display.RenderText();
    }
}