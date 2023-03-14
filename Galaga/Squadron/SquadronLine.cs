using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;


namespace Galaga.Squadron;

public class SquadronLine : ISquadron {
    public EntityContainer<Enemy> Enemies {get;}
    public int MaxEnemies {get;}
    
    public SquadronLine () {
        MaxEnemies = 6;
        Enemies = new EntityContainer<Enemy>(MaxEnemies);
    }
    public void CreateEnemies (List<Image> enemyStride) {
        for (int i = 0; i < MaxEnemies/2; i++) {
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride)));
        }
        for (int i = 0; i < MaxEnemies/2; i++) {
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.55f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride)));
        }
    }
}