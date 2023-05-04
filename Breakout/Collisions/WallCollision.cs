using Breakout.Balls;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Collisions;
public static class WallCollsion {

    public static void Collide(Ball ball) {
        CollideLeftWall(ball);
        CollideRightWall(ball);
        CollideTopWall(ball);
        CollideBottom(ball);
    }

    private static void CollideLeftWall(Ball ball) {
        if (ball._Shape.Position.X < 0) {
            ball._Shape.Direction.X = -ball._Shape.Direction.X; 
        }
    }

    private static void CollideRightWall(Ball ball) {
        if (ball._Shape.Position.X+ball._Shape.Extent.X > 1) {
            ball._Shape.Direction.X = -ball._Shape.Direction.X; 
        }
    }
    private static void CollideTopWall(Ball ball) {
        if (ball._Shape.Position.Y+ball._Shape.Extent.Y > 1) {
            ball._Shape.Direction.Y = -ball._Shape.Direction.Y; 
        }
    }

    private static void CollideBottom(Ball ball) {
        if (ball._Shape.Position.Y <= 0.0 - ball._Shape.Extent.Y) {
            ball.DeleteEntity();
        }
    }
}