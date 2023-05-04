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
                DynamicShape dynamicBall = ball.Shape.AsDynamicShape(); // ball as a dynamic shape
                CollisionData collision = CollisionDetection.Aabb(dynamicBall, block.Shape); // Collision detection
                if (collision.Collision) { // True if there is collision between the ball and block
                    block.LoseHealth(); // Block loses health

                    CollisionDirection collisionDirection = collision.CollisionDir; // the direction of the collision
                    Vec2F currentDirection = dynamicBall.Direction;

                    switch (collisionDirection) {
                        case CollisionDirection.CollisionDirUp:
                            ball.Shape.ChangeDirection((- currentDirection.X, - currentDirection.Y)); 
                            break;
                        case CollisionDirection.CollisionDirDown:
                            // ball.Shape.ChangeDirection();
                            break;
                        case CollisionDirection.CollisionDirLeft:
                            // ball.Shape.ChangeDirection();
                            break;
                        case CollisionDirection.CollisionDirRight:
                            // ball.Shape.ChangeDirection();
                            break;
                        case CollisionDirection.CollisionDirUnchecked:
                            // ball.Shape.ChangeDirection();
                            break;
                    }
                }
            });
        });
    }
}