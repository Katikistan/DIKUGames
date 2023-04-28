using Breakout.Blocks;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout.Levels;

public class LevelManager {
    private LevelCreator levelCreator;
    public EntityContainer<Block> blocks;
    private Player player;
    public LevelManager() {
        levelCreator = new LevelCreator();
        this.blocks = levelCreator.Blocks;
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.05f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
    }
    public void NewLevel(string level) {
        levelCreator.CreateLevel(level);
    }
    private void IterateBlocks() {
        blocks.Iterate(block => {
            if (block.IsDeleted()) {
                // score += block.Value;
            }
        });
    }
    public void Render() {
        player.Render();
        blocks.RenderEntities();
    }
    public void Update() {
        IterateBlocks();
        player.Move();
    }
}