using DIKUArcade.Entities;
using Breakout.Powerups;
namespace Breakout.Blocks;




public static class BlockCreator {
    /// <summary>
    /// Creates a block
    /// </summary>
    /// <param name="shape">The shape of the block.</param>
    /// <param name="image">Image file used for the block.</param>
    /// <param name="meta">Metadata that determines what type of block should be created.</param>
    public static Block CreateBlock(Shape shape, string image, string meta) {
        switch (meta) {
            case "Hardened":
                return new Hardened(shape, image);
            case "Unbreakable":
                return new Unbreakable(shape, image);
            case "PowerUp":
                PowerupBlock PBlock = new PowerupBlock(shape, image);
                PBlock.powerup = PowerUpCreater.CreatePowerUp(shape.Position);            
                return PBlock;
            default:
                return new DefaultBlock(shape, image);
        }
    }
}