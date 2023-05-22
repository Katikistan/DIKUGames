using Breakout.Blocks;
using DIKUArcade.Events;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using Breakout.Collisions;
using Breakout.Timers;
namespace Breakout.Levels;
using Breakout.Balls;
using Breakout.Powerups;

public class LevelManager : IGameEventProcessor {
    private LevelCreator levelCreator;
    private EntityContainer<Block> blocks;
    private EntityContainer<Ball> balls;
    public EntityContainer<Powerup> powerups;

    private Player player;
    private Timer levelTimer;
    private int timer;
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

    public LevelManager() {
        levelCreator = new LevelCreator();
        player = new Player(
            new DynamicShape(new Vec2F(0.425f, 0.06f), new Vec2F(0.15f, 0.04f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        balls = new EntityContainer<Ball>(3);
        blocks = new EntityContainer<Block>(0);

        powerups = new EntityContainer<Powerup>(10); // midlertidigt, til proof of concept til powerups

        timer = 0;
        levelTimer = new Timer(new Vec2F(0.0f, -0.23f), timer);
    }
    /// <summary>
    /// Removes balls and creates newlevel using string levelfile
    /// </summary>
    /// <param name="level">Name of the level file that will be loaded</param>
    public void NewLevel(string level) {
        balls.ClearContainer();
        levelCreator.CreateLevel(level);
        blocks = levelCreator.Blocks;
        balls.AddEntity(BallCreator.CreateBall());
        // powerups.AddEntity(PowerUpCreator.CreatePowerUp(new Vec2F(0.03f, 0.05f)));
        string time = "";
        levelCreator.Meta.TryGetValue("Time", out time);
        if (time != "") {
            timer = int.Parse(time);
        }
        levelTimer.SetTime(timer);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "CLEAR":
                    blocks.ClearContainer();
                    break;
                case "NEW BALL":
                    balls.AddEntity(BallCreator.CreateBall());
                    break;
                case "SPAWN POWERUP":
                    Vec2F pos = (Vec2F)gameEvent.ObjectArg1;
                    // Vec2F pos = new Vec2F(float.Parse(gameEvent.StringArg1),float.Parse(gameEvent.StringArg2));
                    powerups.AddEntity(PowerUpCreator.CreatePowerUp(pos));
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
    /// <summary>
    /// Moves balls in the entitycontainer
    /// </summary>
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
        BlockCollision.Collide(balls, blocks);
        WallCollision.Collide(balls);
        powerups.Iterate(powerup => { // midlertidigt, til proof of concept til powerups
            powerup.Collide(player);
        });
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
        levelTimer.Render();
    }
    public void Update() {
        CheckCollisions();
        CheckTime();
        player.Move();
        MoveBalls();
        MovePowerups();

    }
}