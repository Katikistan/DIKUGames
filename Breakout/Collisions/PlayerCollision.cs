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


    // static float[] RotateVector2d(float x, float y, float radians) {
    //     float[] result = new float[2];
    //     result[0] = (float)(x * Math.Cos(radians) - y * Math.Sin(radians));
    //     result[1] = (float)(x * Math.Sin(radians) + y * Math.Cos(radians));
    //     return result;
    // }
    public static void Collide(EntityContainer<Ball>  balls, Player player) {
        Vec2F vec = new Vec2F(0.00f,0.015f);
        Vec2F vec20 = new Vec2F(-0.0051f,0.01409f);
        Vec2F revvec20 = new Vec2F(0.0051f,0.01409f);
        Vec2F vec45 = new Vec2F(-0.0106f,0.0106f);
        Vec2F revvec45 = new Vec2F(0.0106f,0.0106f);
        float playerposx = player.Shape.Position.X;
        balls.Iterate( ball => {
            // Iterating through every block
            CollisionData collision = CollisionDetection.Aabb(ball._Shape, player.Shape); // Collision detection
                if (collision.Collision) { // True if there is collision between the ball and block
                    float ballx = ball._Shape.Position.X;
                    if  (ballx < playerposx + (player.Shape.Extent.X/5)) {
                        ball._Shape.ChangeDirection(vec45);
                    } else if  (ballx < playerposx + (player.Shape.Extent.X/5)*2 && ballx > playerposx + (player.Shape.Extent.X/5)) {
                        ball._Shape.ChangeDirection(vec20);
                    } else if  (ballx < playerposx + (player.Shape.Extent.X/5)*3 && ballx > playerposx + (player.Shape.Extent.X/5)*2) {
                        ball._Shape.ChangeDirection(vec);
                    } else if  (ballx < playerposx + (player.Shape.Extent.X/5)*4 && ballx > playerposx + (player.Shape.Extent.X/5)*3) {
                        ball._Shape.ChangeDirection(revvec20);
                    } else {
                        ball._Shape.ChangeDirection(revvec45);
                    }
                }
            });
    }
}








