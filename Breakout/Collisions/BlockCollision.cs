using Breakout.Balls;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;
namespace Breakout.Collisions;
public static class BlockCollision {
    /// <summary>
    /// Will check for collsions between ball and blocks,
    /// blocks are deleted and ball direction is changed when collisions occur.
    /// </summary>
    public static void Collide(EntityContainer<Ball> balls, EntityContainer<Block> blocks, bool hardBall) {
        balls.Iterate(ball => {
            // Iterating through every block deletes blocks marked for deletion
            blocks.Iterate(block => {
                CollisionData blockCollision = CollisionDetection.Aabb(ball._Shape, block.Shape);
                if (blockCollision.Collision) { // True if there is collision between the ball and block
                    if (hardBall) {
                        block.LoseHealth(block.Health);
                        block.DeleteEntity();
                    }
                    else {
                        block.LoseHealth(1);
                    }
                    CollisionDirection collisionDirection = blockCollision.CollisionDir;
                    Vec2F currentDirection = ball._Shape.Direction;
                    switch (collisionDirection) {
                        case CollisionDirection.CollisionDirUp:
                        case CollisionDirection.CollisionDirDown:
                            if (!hardBall) {
                                ball._Shape.ChangeDirection(
                                    new Vec2F(currentDirection.X, -currentDirection.Y));
                            }
                            break;
                        case CollisionDirection.CollisionDirLeft:
                        case CollisionDirection.CollisionDirRight:
                            if (!hardBall) {
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