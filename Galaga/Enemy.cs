using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;

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

    public bool CollisionUpdate() {
        hitPoints -=1;
        if (hitPoints == 1) {
            isEnraged = true;
            speed += 1;
        }
        if (isEnraged == true) {
            Image = new ImageStride (80, enragedimg);
        }
        if (hitPoints <= 0) {
            return true;
        }
        return false;
    }
    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image){
        img = image;
    }
    List<Image> enragedimg = ImageStride.CreateStrides (2, Path.Combine("Assets", "Images", "RedMonster.png"));
}