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
    }
    // Remember to explaination your choice as to what happens
    //when losing health.
    public void LoseHealth () {
        health -= 1;

    }
    public void RenderHealth () {
        display.RenderText();
    }
}