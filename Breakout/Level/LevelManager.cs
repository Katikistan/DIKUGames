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
    public Player Player {
        get { return player; }
    }
    private EntityContainer<Ball> balls;
    public LevelManager() {
        levelCreator = new LevelCreator();
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        balls = new EntityContainer<Ball>(3);
    }
    public void NewLevel(string level) {
        levelCreator.CreateLevel(level);
        blocks = levelCreator.Blocks;
        balls.ClearContainer();
        balls.AddEntity(BallCreator.CreateBall());
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "CLEAR":
                    blocks.ClearContainer();
                    break;
                case "NEW BALL":
                    System.Console.WriteLine("wow");
                    balls.AddEntity(BallCreator.CreateBall());
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
    private void MoveBalls() {
        foreach (Ball ball in balls) {
            ball.Move();
        }
    }
    private void CheckCollisions() {
        PlayerCollision.Collide(balls, player);
        BlockCollision.Collide(balls, blocks);
        WallCollsion.Collide(balls);
    }
    public void Render() {
        player.Render();
        blocks.RenderEntities();
        balls.RenderEntities();
    }
    public void Update() {
        CheckCollisions();
        player.Move();
        MoveBalls();
    }
}