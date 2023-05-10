using Breakout.Balls;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace Breakout.Collisions;
public static class BlockCollision {
    public static void Collide(EntityContainer<Ball> balls, EntityContainer<Block> blocks) {
        // Iterating through every ball
        balls.Iterate( ball => {
            // Iterating through every block
            blocks.Iterate( block => {
                CollisionData collision = CollisionDetection.Aabb(ball._Shape, block.Shape); // Collision detection
                if (collision.Collision) { // True if there is collision between the ball and block
                    block.LoseHealth(); // Block loses health
                    CollisionDirection collisionDirection = collision.CollisionDir; // the direction of the collision
                    Vec2F currentDirection = ball._Shape.Direction;
                    if ((collisionDirection == CollisionDirection.CollisionDirUp)
                    | (collisionDirection == CollisionDirection.CollisionDirDown)){
                        ball._Shape.ChangeDirection(new Vec2F(currentDirection.X, - currentDirection.Y));
                    }
                    else if ((collisionDirection == CollisionDirection.CollisionDirLeft)
                    | (collisionDirection == CollisionDirection.CollisionDirRight)) {
                        ball._Shape.ChangeDirection(new Vec2F(-currentDirection.X, currentDirection.Y));
                    }
                }
            });
        });
    }
}