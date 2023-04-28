using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout.Levels;
public class LevelCreator {
    private string[] Map;
    private Dictionary<string, string> Meta;
    private Dictionary<char, string> Legend;
    private LevelReader levelReader;
    private EntityContainer<Block> blocks;
    public EntityContainer<Block> Blocks { get {return blocks;} }
    public LevelReader LevelReader {
        get {return levelReader; }
        set {levelReader = value; }
    }
    public LevelCreator() {
        this.levelReader = new LevelReader();
        this.blocks = new EntityContainer<Block>(0);
    }
    /// <summary>
    /// Reads a file level and creates block in Level.
    /// </summary>
    /// <param name="level">Level text file that will become the new playable level.</param>
    public bool CreateLevel(string level) {
        System.Console.WriteLine("here");
        levelReader.ReadLevel(level);
        if (levelReader.MapValid()) { // LevelData contains map and legend
            System.Console.WriteLine("here");
            this.Map = levelReader.Map;
            this.Meta = levelReader.Meta;
            this.Legend = levelReader.Legend;
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
        // Map can be filled with blocks without crashing
        blocks = new EntityContainer<Block>(324);
        // pos and extent for blocks
        float x = 1f /  12f;
        float y = (1f /  12f) / 3f;
        string colour;
        IBaseImage image;
        StationaryShape shape;
        for (int i = 0; i < Map.Length - 1; i++) {
            for (int j = 0; j < Map[i].Length; j++) {
                shape = new StationaryShape(
                    new Vec2F((x * (float) j), 1.0f - (y * (float) i)),
                    new Vec2F(x, y));
                if (Legend.TryGetValue(Map[i][j], out colour!)) {
                    System.Console.WriteLine("here also");
                    // Key has a vaild colour file
                    image = new Image(
                        Path.Combine("..", "Breakout", "Assets", "Images", colour));
                    blocks.AddEntity(new DefaultBlock(shape, image));
                }
            }
        }
    }
}
