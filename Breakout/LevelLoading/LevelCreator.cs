using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.Collections.Generic;
namespace Breakout.Levels;
/// <summary>
/// Used to create a level from a string levelfile. Uses a level reader to read level file.
/// </summary>
public class LevelCreator {
    private string[] map;
    private Dictionary<string, string> meta;
    private Dictionary<char, string> legend;
    private LevelReader levelReader;
    private int time;
    private bool hasTimer;
    private EntityContainer<Block> blocks;
    public EntityContainer<Block> Blocks {
        get {
            return blocks;
        }
    }
    public int Time {
        get {
            return time;
        }
    }
    public bool HasTimer {
        get {
            return hasTimer;
        }
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
        levelReader.ReadLevel(level);
        if (levelReader.MapValid()) { // LevelData contains map and legend
            this.map = levelReader.Map;
            this.meta = levelReader.Meta;
            this.legend = levelReader.Legend;
            CreateBlocks();
            InitializeTimer();
            return true;
        } else {
            return false;
        }
    }
    /// <summary>
    /// Uses map, legend and meta to draw blocks and apply metadata in the level.
    /// </summary>
    private void CreateBlocks() {
        // map can be filled with blocks without crashing
        blocks = new EntityContainer<Block>(324);
        // pos and extent for blocks
        float x = 1f / 12f;
        float y = (1f / 12f) / 2.5f;
        string colour;
        string metadata;
        Shape shape;
        Block block;
        for (int i = 0; i < map.Length - 1; i++) {
            for (int j = 0; j < map[i].Length; j++) {
                shape = new StationaryShape(
                    new Vec2F((x * (float) j), 1.0f - (y * (float) i)),
                    new Vec2F(x, y));
                if (legend.TryGetValue(map[i][j], out colour!)) {
                    meta.TryGetValue(map[i][j].ToString(), out metadata!);
                    block = BlockCreator.CreateBlock(shape, colour, metadata);
                    blocks.AddEntity(block);
                }
            }
        }
    }
    private void InitializeTimer() {
        string timeval = "";
        meta.TryGetValue("Time", out timeval);
        if (timeval != "" && timeval != null) {
            hasTimer = true;
            time = int.Parse(timeval);
        } else {
            hasTimer = false;
            time = System.Int32.MaxValue;
        }
    }
}