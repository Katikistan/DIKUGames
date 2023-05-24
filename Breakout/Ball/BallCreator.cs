using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
namespace Breakout.Balls;
public static class BallCreator {
    public static Ball CreateBall(Vec2F pos, Vec2F dir) {
        System.Console.WriteLine(new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));

        return new Ball(new DynamicShape(
            pos,
            new Vec2F(0.03f, 0.03f),
            dir),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
    }
}