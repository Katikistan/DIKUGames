using Breakout.Blocks;
using DIKUArcade.Entities;
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
namespace Breakout.Levels;

public class Level {
    public char[][] ?Map;

    public Dictionary<string,string> ?Meta;

    public Dictionary<char,string> ?Legend;

    public LevelLoader levelLoader;
    public EntityContainer<Block> blocks;
    public Level (string startlevel) {
        this.levelLoader = new LevelLoader(Path.Combine("..","Breakout","Assets","Levels"));
        this.levelLoader.ReadLevel(startlevel);
        this.Map = levelLoader.Map;
        this.Meta = levelLoader.Meta;
        this.Legend = levelLoader.Legend;
    }

    public void NewLevel(string nextlevel) {
        levelLoader.ReadLevel(nextlevel);
        this.Map = levelLoader.Map;
        this.Meta = levelLoader.Meta;
        this.Legend = levelLoader.Legend;
    }
    public void DrawMap() {
       if (Map != null) {
            blocks = new EntityContainer<Breakout.Blocks.Block>(324);
            var block_extentX = 1f/(float)Map[0].Length;
            var block_extentY = block_extentX/3f;
            for (int i = 0; i < Map.Length; i++) {
                for (int j = 0; j < Map[i].Length; j++) {
                    string colour = "";

                    Legend.TryGetValue(Map[i][j], out colour);

                    if (Map[i][j] != '-') {

                        blocks.AddEntity(new Breakout.Blocks.Block(
                            new StationaryShape(new Vec2F((block_extentX * (float)j), 1.0f - (block_extentY * (float)i)), new Vec2F(block_extentX, block_extentY)),
                            new Image(Path.Combine("..","Breakout","Assets", "Images", colour))
                            ));
                        }
                    }
                }
       }
    }
}
