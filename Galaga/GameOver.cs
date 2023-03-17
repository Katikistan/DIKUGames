using DIKUArcade.Graphics;
using DIKUArcade.Math;
public class GameOver {
    private Text gameOverText;
    private Text levelText;
    private int level = 1;
    public GameOver() {
        gameOverText = new Text(
            "Dead",
            new Vec2F(0.39f, 0.3f),
            new Vec2F(0.4f, 0.4f));
        gameOverText.SetColor(new Vec3I(255, 255, 255));

        levelText = new Text(
            $"Level: {level.ToString()}",
            new Vec2F(0.35f, 0.2f),
            new Vec2F(0.4f, 0.4f));
        levelText.SetColor(new Vec3I(255, 255, 255));
    }
    public void SetLevel(int level) {
        levelText.SetText($"Level: {level.ToString()}");
    }
    public void Render() {
        levelText.RenderText();
        gameOverText.RenderText();
    }
}