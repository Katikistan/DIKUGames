using Breakout.Balls;
using DIKUArcade.Entities;
using DIKUArcade.Events;
namespace Breakout.Collisions;
/// <summary>
/// Handles collisions between balls and walls
/// </summary>
public static class WallCollision {
    /// <summary>
    /// Will check for collsions between ball and walls.
    /// If ball hits a wall it's send the opposite direction.
    /// If balls hits bottom, the player loses health and a new ball is spawned.
    /// </summary>
    public static void Collide(EntityContainer<Ball> balls) {
        balls.Iterate(ball => {
            CollideLeftWall(ball);
            CollideRightWall(ball);
            CollideTopWall(ball);
            CollideBottom(ball);
        });
        if (balls.CountEntities() == 0) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "LOSE HEALTH"
            });
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "NEW BALL"
            });
        }

    }
    private static void CollideLeftWall(Ball ball) {
        if (ball._Shape.Position.X < 0) {
            ball._Shape.Direction.X = -ball._Shape.Direction.X;
        }
    }
    private static void CollideRightWall(Ball ball) {
        if (ball._Shape.Position.X + ball._Shape.Extent.X > 1) {
            ball._Shape.Direction.X = -ball._Shape.Direction.X;
        }
    }
    private static void CollideTopWall(Ball ball) {
        if (ball._Shape.Position.Y + ball._Shape.Extent.Y > 1) {
            ball._Shape.Direction.Y = -ball._Shape.Direction.Y;
        }
    }
    private static void CollideBottom(Ball ball) {
        if (ball._Shape.Position.Y <= 0.0 - ball._Shape.Extent.Y) {
            ball.DeleteEntity();
        }
    }
}