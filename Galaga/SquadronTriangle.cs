using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Galaga.Squadron;

public class SquadronTrinagle : ISquadron {
    public EntityContainer<Enemy> Enemies {get;}
    public int MaxEnemies {get;}
    public void CreateEnemies (List<Image> enemyStride,
        List<Image> alternativeEnemyStride) {

        }

}