using Breakout.Blocks;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using Breakout.Collisions;
namespace Breakout.Levels;
using Breakout.Balls;

public class LevelManager {
    private LevelCreator levelCreator;
    public EntityContainer<Block> blocks;
    private Player player;
    private EntityContainer<Ball> balls;
    public LevelManager() {
        levelCreator = new LevelCreator();
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        // balls = levelCreator.Balls;
    }
    public void NewLevel(string level) {
        levelCreator.CreateLevel(level);
        blocks = levelCreator.Blocks;
    }
    private void CheckCollisions() {
        PlayerCollision.Collide(balls, player);
        BlockCollision.Collide(balls, blocks);
        WallCollision.Collide(balls);
    }
    public void Render() {
        player.Render();
        blocks.RenderEntities();
    }
    public void Update() {
        CheckCollisions();
        player.Move();
    }
}