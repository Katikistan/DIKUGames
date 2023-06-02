using Breakout.Balls;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;
namespace Breakout.Collisions;
/// <summary>
/// Handles collisions between balls and blocks
/// </summary>
public static class BlockCollision {
    /// <summary>
    /// Will check for collsions between ball and blocks,
    /// blocks lose health and ball direction is changed when collisions occur.
    /// </summary>
    public static void Collide(EntityContainer<Ball> balls, EntityContainer<Block> blocks, bool hardBall) {
        balls.Iterate(ball => {
            // Iterating through every block deletes blocks marked for deletion
            blocks.Iterate(block => {
                CollisionData blockCollision = CollisionDetection.Aabb(ball._Shape, block.Shape);
                if (blockCollision.Collision) { // True if there is collision between the ball and block
                    if (hardBall) {
                        // If hardball powerup is active block should be deleted
                        block.LoseHealth(block.Health);
                        block.DeleteEntity();
                    } else {
                        // If hardball isnt active the block should lose 1 health
                        block.LoseHealth(1);
                    }
                    CollisionDirection collisionDirection = blockCollision.CollisionDir;
                    Vec2F currentDirection = ball._Shape.Direction;
                    switch (collisionDirection) {
                        case CollisionDirection.CollisionDirUp:
                        case CollisionDirection.CollisionDirDown:
                            if (!hardBall) {
                                // If hardball is active, ball shouldnt change direction.
                                ball._Shape.ChangeDirection(
                                    new Vec2F(currentDirection.X, -currentDirection.Y));
                            }
                            break;
                        case CollisionDirection.CollisionDirLeft:
                        case CollisionDirection.CollisionDirRight:
                            if (!hardBall) {
                                // If hardball is active, ball shouldnt change direction.
                                ball._Shape.ChangeDirection(
                                    new Vec2F(-currentDirection.X, currentDirection.Y));
                            }
                            break;
                    }
                }
            });
        });
    }
}