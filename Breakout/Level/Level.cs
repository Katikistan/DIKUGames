using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Levels;

public class Level {
    private char[][] ?Map;
    private Dictionary<string,string> ?Meta;
    private Dictionary<char,string> Legend;
    private LevelLoader levelLoader;
    public EntityContainer<Block> blocks;
    public Level () {
        this.levelLoader = new LevelLoader(Path.Combine("..","Breakout","Assets","Levels"));
        this.blocks = new EntityContainer<Block>(0);
    }
    public void NewLevel(string level) {
        //next map if level input is not valid
        levelLoader.ReadLevel(level);
        this.Map = levelLoader.Map;
        this.Meta = levelLoader.Meta;
        this.Legend = levelLoader.Legend;
        CreateBlocks();
    }
    private void CreateBlocks() {
       if (Map != null && Legend != null) {
            blocks = new EntityContainer<Breakout.Blocks.Block>(324);
            float block_extentX = 1f/(float)Map[0].Length;
            float block_extentY = block_extentX/3f;
            for (int i = 0; i < Map.Length; i++) {
                for (int j = 0; j < Map[i].Length; j++) {
                    string colour = "";
                    Legend.TryGetValue(Map[i][j], out colour);
                    if (Map[i][j] != '-') {
                        blocks.AddEntity(new DefaultBlock(
                            new StationaryShape(
                            new Vec2F((block_extentX * (float)j), 1.0f - (block_extentY * (float)i)), new Vec2F(block_extentX, block_extentY)),
                            new Image(Path.Combine("..","Breakout","Assets", "Images", colour))
                            ));
                    }
                }
            }
       }
    }
}
