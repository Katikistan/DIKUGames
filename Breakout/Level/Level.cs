using System;
using System.IO;
using System.Collections.Generic;
namespace Breakout.Levels;

public class Level {
    public char[][] ?Map;

    public Dictionary<string,string> ?Meta;

    public Dictionary<char,string> ?Legend;

    public LevelLoader levelLoader;
    public EntityContainer<Block> Blocks;

    public Level (string startlevel) {
        this.levelLoader = new LevelLoader();
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
        for (int i = 0; i < Map.Length; i++) {
            for (int j = 0; i < Map[i].Length; j++) {
                // lav block og giv den farve først, dernæst når block er lavet så apply den meta data der evt skal tilføjes
                string value = "";
                if (Legend.TryGetValue(Map[i][j], out value)) {
                    System.Console.WriteLine(value);
                }
                if (Meta.TryGetValue(Char.ToString(Map[i][j]), out value)) {
                    System.Console.WriteLine(value);
                }
            }
        }
    }
}
