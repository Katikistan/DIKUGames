using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
namespace Breakout.Balls;
/// <summary>
///  Creates a ball entity
/// </summary>
public static class BallCreator {
    /// <summary>
    ///  Creates a ball using a position and a directional vector
    /// </summary>
    public static Ball CreateBall(Vec2F pos, Vec2F dir) {
        return new Ball(new DynamicShape(
            pos,
            new Vec2F(0.03f, 0.03f),
            dir),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
    }
}