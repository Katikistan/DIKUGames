using DIKUArcade.Entities;

namespace Galaga.MovementStrategy;
public class Down : IMovementStrategy {
    public void MoveEnemy(Enemy enemy) {
        enemy.Shape.Position.Y -= enemy.Speed;
            
    }
    public void MoveEnemies (EntityContainer<Enemy> enemies) {
        foreach (Enemy enemy in enemies) {
            MoveEnemy(enemy);
        }
    }
}
