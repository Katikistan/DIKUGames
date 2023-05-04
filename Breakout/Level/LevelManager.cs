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
    private Ball ball;
    public LevelManager() {
        levelCreator = new LevelCreator();
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        ball = new Ball(new DynamicShape(new Vec2F(0.45f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f,0.008f)), 
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
    }
    public void NewLevel(string level) {
        levelCreator.CreateLevel(level);
        blocks = levelCreator.Blocks;
    }
    private void CheckCollisions() {
        PlayerCollsion.Collide(balls, player);
        BlockCollsion.Collide(balls, blocks);
        WallCollsion.Collide(ball);


    }
    public void Render() {
        player.Render();
        blocks.RenderEntities();
        ball.Render();
    }
    public void Update() {
        CheckCollisions();
        player.Move();
        ball.Move();
    }
}