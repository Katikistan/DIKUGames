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

public abstract class Powerup : Entity {

    public Powerup(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    public void Move() {
        this.Shape.Move();
        if (Shape.Position.Y <= 0.0 - Shape.Extent.Y) {
            DeleteEntity();
        }
    }
    public abstract void Effect();
}