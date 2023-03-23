using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron;

public class SquadronTriangle : ISquadron {
    public EntityContainer<Enemy> Enemies {
        get;
    }
    public int MaxEnemies {
        get;
    }

    // Triangle 1 center
    private Vec2F t1 = new Vec2F(0.2f, 0.7f);
    // Triangle 2 center
    private Vec2F t2 = new Vec2F(0.7f, 0.7f);
    // Triangle 3 center
    private Vec2F t3 = new Vec2F(0.5f, 0.9f);
    public SquadronTriangle() {
        MaxEnemies = 9;
        Enemies = new EntityContainer<Enemy>(MaxEnemies);
    }
    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {

        ImageStride blueMonster = new ImageStride(80, enemyStride);
        ImageStride greenMonster = new ImageStride(80, alternativeEnemyStride);
        Vec2F ext = new Vec2F(0.1f, 0.1f);

        // Create triangle 1 (left)
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t1.X, t1.Y + 0.1f), ext), blueMonster));
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t1.X + 0.05f, t1.Y), ext), blueMonster));
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t1.X - 0.05f, t1.Y), ext), blueMonster));

        // Create triangle 2 (right)
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t2.X, t2.Y + 0.1f), ext), blueMonster));
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t2.X + 0.05f, t2.Y), ext), blueMonster));
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t2.X - 0.05f, t2.Y), ext), blueMonster));

        // Create triangle 3 (centre)
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t3.X - ext.X / 2, t3.Y - 0.1f), ext), greenMonster));
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t3.X - ext.X / 2 + 0.05f, t3.Y), ext), greenMonster));
        Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(t3.X - ext.X / 2 - 0.05f, t3.Y), ext), greenMonster));
    }
}