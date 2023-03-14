using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Galaga.Squadron;
public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}
    void CreateEnemies (List<Image> enemyStride);
}

