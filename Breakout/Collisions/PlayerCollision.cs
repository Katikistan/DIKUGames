using Breakout.Balls;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;

namespace Breakout.Collisions;
public static class PlayerCollision {



    static float[] RotateVector2d(float x, float y, float radians) {
        float[] result = new float[2];
        result[0] = (float)(x * Math.Cos(radians) - y * Math.Sin(radians));
        result[1] = (float)(x * Math.Sin(radians) + y * Math.Cos(radians));
        return result;
    }
    public static void Collide(EntityContainer<Ball>  balls, Player player) {
        balls.Iterate( ball => {
            // Iterating through every block
            CollisionData collision = CollisionDetection.Aabb(ball._Shape, player.Shape); // Collision detection
                if (collision.Collision) { // True if there is collision between the ball and block
                    if (ball._Shape.Position.X < player.Shape.Position.X + (player.Shape.Extent.X/2)) {
                        ball._Shape.Direction.Y = -ball._Shape.Direction.Y;
                        float[] rot = RotateVector2d(ball._Shape.Direction.X, ball._Shape.Direction.Y, .262f);
                        ball._Shape.Direction.X = rot[0];
                        ball._Shape.Direction.Y = rot[1];
                    } else {
                        ball._Shape.Direction.Y = -ball._Shape.Direction.Y;
                        float[] rot = RotateVector2d(ball._Shape.Direction.X, ball._Shape.Direction.Y, -.262f);
                        ball._Shape.Direction.X = rot[0];
                        ball._Shape.Direction.Y = rot[1];
                    }
                }
            });
    }
}








