using DIKUArcade.Entities;
namespace Breakout.Blocks;
public static class BlockCreator {
    public static Block CreateBlock(StationaryShape shape, string image, string meta) {
        switch (meta) {
            case "Hardened":
                return new Hardened(shape,image);
            case "Unbreakable":
                return new Unbreakable(shape,image);
            default:
                return new DefaultBlock(shape,image);
        }
    }

}