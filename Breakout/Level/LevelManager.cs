using Breakout.Blocks;
using DIKUArcade.Events;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using Breakout.Collisions;
namespace Breakout.Levels;
using Breakout.Balls;

public class LevelManager : IGameEventProcessor {
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
        ball = new Ball(new DynamicShape(new Vec2F(0.45f, 0.2f), new Vec2F(0.03f, 0.03f), new Vec2F(0.001f,0.015f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png")));
        balls = new EntityContainer<Ball>(2);
        balls.AddEntity(ball);
    }
    public void NewLevel(string level) {
        levelCreator.CreateLevel(level);
        blocks = levelCreator.Blocks;
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "CLEAR":
                blocks.ClearContainer();
                break;
            }
        }
    }
    public bool EmptyLevel() {
        foreach (Block block in blocks) {
            if (block is not Unbreakable) {
                return false;
            }
        }
        return true;
    }
    private void CheckCollisions() {
        PlayerCollision.Collide(balls, player);
        BlockCollision.Collide(balls, blocks);
        WallCollsion.Collide(balls);
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