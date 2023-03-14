using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga;
public class Enemy : Entity {
    private bool isEnraged;
    private int hitPoints = 3;
    private float speed;
    private IBaseImage img;
    public int HitPoints {
        get { return hitPoints;}
        set { hitPoints = value;}
        }
    public float Speed {
        get { return speed;}
        set { speed = value;}
        }
    public bool IsEnraged {
        get { return isEnraged;}
        set { isEnraged = value;}
        }

    public void CollisionUpdate() {
        hitPoints -=1;
        if (hitPoints == 1) {
            isEnraged = true;
            speed += 1;
        }
        if (isEnraged == true) {
            img = "RedMonster.png";
        }
    }
    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image){
        img = image;
    }
    List<Image> images = ImageStride.CreateStrides (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
}