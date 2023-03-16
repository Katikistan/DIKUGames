using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
//IGameEventProcessor
public class Enemy : Entity {
    private Vec2F startpos;
    public Vec2F Startpos {
        get {return startpos;}
    }
    private DynamicShape shape;
    public DynamicShape SShape {
        get {return shape;}
    }
    private int hitPoints = 3;
    private float speed = 0.001f;
    public float Speed { get => speed;}
    List<Image> enragedimg = ImageStride.CreateStrides
    (2, Path.Combine("Assets", "Images", "RedMonster.png"));
    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.shape = shape;
        startpos = new Vec2F(shape.Position.X,shape.Position.Y);
    }
    public bool IsEnemyDead() {
        hitPoints -=1;
        if (hitPoints <= 0) {
            return true;
        } else if (hitPoints == 2) {
            Image = new ImageStride (80, enragedimg);
            speed += 0.001f;
        }
        return false;
    }
    public void IncreaseSpeed(float increacse) {
        speed += increacse;
    }

}