using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using Breakout.Levels;
using Breakout.States;
using Breakout.Players;
using DIKUArcade.Physics;
namespace Breakout.Powerups;

public abstract class Powerup : Entity{

    public Powerup(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }
    
    public void Move() {
        this.Shape.Move();
    }
    public abstract void Effect();
    public bool Collide(Player player) {
        CollisionData collision = CollisionDetection.Aabb((DynamicShape)this.Shape, player.Shape);
        if (collision.Collision) {
            this.Effect();
            this.DeleteEntity();
            return true;
        } else {
            return false;
        }
    }
    

    
}