using DIKUArcade.Entities;
using System;

namespace Galaga.MovementStrategy;
public class ZigZagDown : IMovementStrategy {
    private const float a = 0.05f;
    private const float p = 0.045f;
    private const float s = 0.0003f;

    public void MoveEnemy(Enemy enemy) {
        const float x0 = 0.1f;
        const float y0 = 0.0f;
        float pi = (float)Math.PI;
        float yi = enemy.Shape.Position.Y + s;
        float sin = (float)Math.Sin((2 * pi *(y0-yi))/p);
        float xi = x0 + a * sin;
        enemy.Shape.Position.Y -= enemy.Speed;
        enemy.Shape.Position.X = xi;

            
    }
    public void MoveEnemies (EntityContainer<Enemy> enemies) {
        foreach (Enemy enemy in enemies) {
            MoveEnemy(enemy);
        }
    }
}
