using Breakout.Balls;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout.Powerups;
namespace Breakout.Collisions;
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
            }
        });
        return hit;
    }
}