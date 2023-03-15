using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy;
public interface IMovementStrategy {
    void MoveEnemy (Enemy enemy);
    void MoveEnemies (EntityContainer<Enemy> enemies);
}
