// public enum PowerupTypes {
//     invisible,
//     extralife
// }
// public class PowerUpGenerator {
//     public IPowerup GivePowerup(shape) {
//         int random = 1;
//         switch (random) {
//             case (int)PowerupTypes.invisible:
//                 return new PowerUpInvisible(shape);
//         }

//     }
// }

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Powerups;

public static class PowerUpGenerator{

    public static Powerup MakePowerups() {
            
            return new LifePlus(new DynamicShape(
                new Vec2F(0.45f, 0.5f),
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "LifePickUp.png")));
        }
}

