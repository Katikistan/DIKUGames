using System.IO;
using System.Collections.Generic;
using Breakout.Levels;
namespace Breakout;

public class Level {
    public char[][] ?Map;

    public Dictionary<string,string> ?Meta;

    public Dictionary<char,string> ?Legend;

    public LevelLoader levelLoader;

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
}
