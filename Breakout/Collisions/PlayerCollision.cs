using Breakout.Balls;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;
namespace Breakout.Collisions;
public static class PlayerCollision {
    // static float[] RotateVector2d(float x, float y, float radians) {
    //     float[] result = new float[2];
    //     result[0] = (float)(x * Math.Cos(radians) - y * Math.Sin(radians));
    //     result[1] = (float)(x * Math.Sin(radians) + y * Math.Cos(radians));
    //     return result;
    // }
    /// <summary>
    /// Will check for collsions between ball and the player,
    /// depending on where the ball hit the player, the balls directional vector is changed.
    /// </summary>
    public static void Collide(EntityContainer<Ball>  balls, Player player) {
        Vec2F vec = new Vec2F(0.0f,0.015f);
        Vec2F vec20 = new Vec2F(-0.0051f,0.01409f);
        Vec2F revVec20 = new Vec2F(0.0051f,0.01409f);
        Vec2F vec45 = new Vec2F(-0.0106f,0.0106f);
        Vec2F revVec45 = new Vec2F(0.0106f,0.0106f);
        float playerposx = player.Shape.Position.X;
        balls.Iterate( ball => {
            // Iterating through every block
            CollisionData collision = CollisionDetection.Aabb(ball._Shape, player.Shape);
                if (collision.Collision) { // True if there is collision between the ball and player
                    float ballx = ball._Shape.Position.X+(ball._Shape.Extent.X/2); //Middle of ball
                    float playerExtentX = player.Shape.Extent.X;
                    if  (ballx < playerposx + (playerExtentX/5)) {
                        ball._Shape.ChangeDirection(vec45);
                    } else if  (ballx < playerposx + (playerExtentX/5)*2 &&
                                ballx > playerposx + (player.Shape.Extent.X/5)) {
                        ball._Shape.ChangeDirection(vec20);
                    } else if  (ballx < playerposx + (playerExtentX/5)*3 &&
                                ballx > playerposx + (playerExtentX/5)*2) {
                        ball._Shape.ChangeDirection(vec);
                    } else if  (ballx < playerposx + (playerExtentX/5)*4 &&
                                ballx > playerposx + (playerExtentX/5)*3) {
                        ball._Shape.ChangeDirection(revVec20);
                    } else {
                        ball._Shape.ChangeDirection(revVec45);
                    }
                }
            });
    }
}








