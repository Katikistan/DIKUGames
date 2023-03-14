using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;

namespace Galaga;
public class Enemy : Entity {
    private int hitPoints = 3;
    private float speed = 0.01f;
    List<Image> enragedimg = ImageStride.CreateStrides (2, Path.Combine("Assets", "Images", "RedMonster.png"));
    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image){
    }
    public bool IsEnemyDead() {
        hitPoints -=1;
        if (hitPoints <= 0) {
            return true;
        } else if (hitPoints <= 2) {
            Image = new ImageStride (80, enragedimg);
            speed += 0.01f;
        }
        return false;
    }

   
}