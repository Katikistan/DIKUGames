using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
//IGameEventProcessor 
public class Enemy : Entity {
    private int hitPoints = 3;
    private bool enraged = false;
    public bool Enraged { get => enraged;}
    private float speed = 0.01f;
    public float Speed { get => speed;}
    List<Image> enragedimg = ImageStride.CreateStrides 
    (2, Path.Combine("Assets", "Images", "RedMonster.png"));
    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }
    public bool IsEnemyDead() {
        hitPoints -=1;
        if (hitPoints <= 0) {
            return true;
        } else if (hitPoints <= 2) {
            enraged = true;
            Image = new ImageStride (80, enragedimg);
            speed += 0.01f;
        }
        return false;
    }

   
}