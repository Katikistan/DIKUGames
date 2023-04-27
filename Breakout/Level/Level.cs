using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Levels;
public class Level {
    private char[][] Map = null!;
    private Dictionary<string, string> Meta = null!;
    private Dictionary<char, string> Legend = null!;
    private LevelLoader levelLoader;
    public EntityContainer<Block> blocks;
    public EntityContainer<Block> Blocks { get {return blocks;} }
    public Level() {
        this.levelLoader = new LevelLoader(Path.Combine("..", "Breakout", "Assets", "Levels"));
        this.blocks = new EntityContainer<Block>(0);
    }
    /// <summary>
    /// Reads a file level and creates block in Level.
    /// </summary>
    /// <param name="level">Level text file that will become the new playable level.</param>
    public bool NewLevel(string level) {
        //next map if level input is not valid
        levelLoader.ReadLevel(level);
        this.Map = levelLoader.Map!;
        this.Meta = levelLoader.Meta!;
        this.Legend = levelLoader.Legend!;
        if (levelLoader.MapValid()) {
            System.Console.WriteLine(levelLoader.MapValid());
            CreateBlocks();
            return true;
        } else {
            return false;
        }
    }
    /// <summary>
    /// Uses Map, Legend and Meta to draw blocks and apply metadata in the level.
    /// </summary>
    private void CreateBlocks() {
        blocks = new EntityContainer<Block>(324);
        float block_extentX = 1f / (float) 12;
        float block_extentY = block_extentX / 3f;
        string colour;
        IBaseImage image;
        StationaryShape shape;
        for (int i = 0; i < Map.Length - 1; i++) {
            for (int j = 0; j < Map[i].Length; j++) {
                shape = new StationaryShape(
                    new Vec2F((block_extentX * (float) j), 1.0f - (block_extentY * (float) i)),
                    new Vec2F(block_extentX, block_extentY));
                Legend.TryGetValue(Map[i][j], out colour!);
                if (colour != null) {
                    image = new Image(
                        Path.Combine("..", "Breakout", "Assets", "Images", colour));
                    blocks.AddEntity(new DefaultBlock(shape, image));
                }
            }
        }
    }
    public void Render() {
        blocks.RenderEntities();
    }
}
