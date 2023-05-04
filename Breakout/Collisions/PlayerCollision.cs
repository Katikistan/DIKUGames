using Breakout.Balls;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace Breakout.Collisions;
public static class PlayerCollision {
    public static void Collide(EntityContainer<Ball>  balls, Player player) {
        balls.Iterate( ball => {
            // Iterating through every block
            CollisionData collision = CollisionDetection.Aabb(ball._Shape, player.Shape); // Collision detection
                if (collision.Collision) { // True if there is collision between the ball and block
                    if (ball._Shape.Position.X < player.Shape.Position.X + (player.Shape.Extent.X/2)) {
                        ball._Shape.Direction = ball._Shape.Direction * 0.0873f;
                        ball._Shape.Direction.X = ball._Shape.Direction.X * 0.0873f;
                    } else {
                        ball._Shape.Direction.Y = -ball._Shape.Direction.Y;
                        ball._Shape.Direction.X = -ball._Shape.Direction.X + 0.002f;

                    }
                }
            });
    }
}
    

        
    



    
