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
using Breakout.Powerups;
/// <summary>
/// Class that creates and manages game objects, such as blocks, balls and the player.
/// </summary>
public class LevelManager : IGameEventProcessor {
    private LevelCreator levelCreator;
    private EntityContainer<Block> blocks;
    private EntityContainer<Ball> balls;
    public EntityContainer<Powerup> powerups;
    private Player player;
    private bool hardBalls = false;
    private Timer levelTimer;
    public Player Player {
        get {
            return player;
        }
    }
    public EntityContainer<Block> Blocks {
        get => blocks;
    }
    public EntityContainer<Ball> Balls {
        get => balls;
    }
    public LevelCreator LevelCreator {
        get => levelCreator;
    }
    public Timer LevelTimer {
        get => levelTimer;
    }
    public bool HardBalls {
        get => hardBalls;
    }
    public LevelManager() {
        levelCreator = new LevelCreator();
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        balls = new EntityContainer<Ball>(18);
        blocks = new EntityContainer<Block>(0);

        powerups = new EntityContainer<Powerup>(10);
        levelTimer = new Timer(new Vec2F(0.0f, -0.285f), 0);
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
    }
    /// <summary>
    /// Removes balls and creates newlevel using string levelfile
    /// </summary>
    /// <param name="level">Name of the level file that will be loaded</param>
    public void NewLevel(string level) {
        balls.ClearContainer(); // Reseting balls
        levelCreator.CreateLevel(level); // Creating new level
        blocks = levelCreator.Blocks; // Block container for new level becomes current block container
        balls.AddEntity(BallCreator.CreateBall(new Vec2F(0.45f, 0.2f), new Vec2F(0.001f, 0.015f)));
        levelTimer.SetTime(levelCreator.Timer); // Timer for level is set
    }
    /// <summary>
    /// Proceeses StatusEvents
    /// </summary>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "CLEAR":
                    blocks.ClearContainer();
                    break;
                case "NEW BALL":
                    balls.AddEntity(BallCreator.CreateBall(new Vec2F(0.45f, 0.2f), new Vec2F(0.001f, 0.015f)));
                    break;
                case "SPAWN POWERUP":
                    Vec2F pos = (Vec2F) gameEvent.ObjectArg1;
                    powerups.AddEntity(PowerUpCreator.CreatePowerUp(pos));
                    break;
                case "HARD BALL":
                    if (gameEvent.StringArg1 == "START") {
                        hardBalls = true;
                    } else if (gameEvent.StringArg1 == "END") {
                        hardBalls = false;
                    }
                    break;
                case "SPLIT":
                    balls.Iterate(ball => {
                        if (balls.CountEntities() < 18) { // amount of balls that can be added is capped to 18
                            pos = ball.Shape.Position;
                            // 2 balls are added that go in different directions
                            balls.AddEntity(BallCreator.CreateBall(pos, new Vec2F(-0.0106f, 0.0106f)));
                            balls.AddEntity(BallCreator.CreateBall(pos, new Vec2F(0.0106f, 0.0106f)));
                        }
                    });
                    break;
            }
        }
    }
    /// <summary>
    /// Checks wheter if the level is containing any blocks that arent unbreakable.
    /// </summary>
    /// <returns>false if level has blocks other than unbreakable blocks, else true.</returns>
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
    private void MovePowerups() {
        foreach (Powerup powerup in powerups) {
            powerup.Move();
        }
    }
    private void CheckCollisions() {
        PlayerCollision.Collide(balls, player);
        BlockCollision.Collide(balls, blocks, hardBalls);
        WallCollision.Collide(balls);
        PowerUpCollision.Collide(powerups, player);
    }
    private void CheckTime() {
        if (levelTimer.TimeLeft < 1) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_LOST"
            });
        }
    }
    public void Render() {
        player.Render();
        blocks.RenderEntities();
        balls.RenderEntities();
        powerups.RenderEntities();
        if (levelCreator.HasTimer) {
            levelTimer.Render();
        }
    }
    public void Update() {
        CheckCollisions();
        CheckTime();
        player.Move();
        MoveBalls();
        MovePowerups();
    }
}