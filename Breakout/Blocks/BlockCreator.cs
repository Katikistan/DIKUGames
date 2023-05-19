using DIKUArcade.Entities;
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
                return new Unbreakable(shape, image);
            default:
                return new DefaultBlock(shape, image);
        }
    }
}