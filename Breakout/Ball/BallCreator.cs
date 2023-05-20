using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
namespace Breakout.Balls;
public static class BallCreator {
    public static Ball CreateBall() {
        System.Console.WriteLine(new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        return new Ball(new DynamicShape(
            new Vec2F(0.45f, 0.2f),
            new Vec2F(0.03f, 0.03f),
            new Vec2F(0.001f, 0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        System.Console.WriteLine(new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
    }
}
// xml