using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Powerups;
namespace Breakout.Collisions;
/// <summary>
/// Handles collisions between the player and powerups
/// </summary>
public static class PowerUpCollision {
    /// <summary>
    /// Will check for collsions between powerup and the player,
    /// depending on where the ball hit the player, the balls directional vector is changed.
    /// </summary>
    public static bool Collide(EntityContainer<Powerup> powerups, Player player) {
        bool hit = false;
        powerups.Iterate(powerup => {
            CollisionData collision = CollisionDetection.Aabb(
                (DynamicShape)powerup.Shape, player.Shape);
            if (collision.Collision) {
                powerup.Effect();
                hit = true;
                powerup.DeleteEntity();
            }
        });
        return hit;
    }
}