using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Galaga.MovementStrategy;
using Galaga.Squadron;
using System.Collections.Generic;
using System.IO;
namespace Galaga.GalagaStates;
public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private GameOver gameOverScreen;
    private Entity backGroundImage;
    private int level = 1;
    private Player player;
    private Health health;
    // enemy fields
    private ISquadron squadron = new SquadronLine();
    private int squadronNum = 0;
    private List<Image> blueMonster;
    private List<Image> greenMonster;
    private IMovementStrategy movestrat;
    // Fields for playershots
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;

    // Fields for explosion
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }

    public void InitializeGameState() {
        backGroundImage = new Entity(
            new StationaryShape(
                new Vec2F(0.0f, 0.0f),
                new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine(
                "..", "Galaga", "Assets", "Images", "SpaceBackground.png")));
        // Creates a player object for the game
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));

        // Adds health and text in bottom of window
        health = new Health(
            new Vec2F(0.04f, -0.42f),
            new Vec2F(0.4f, 0.5f));
        // Adds enemies to the game
        blueMonster = ImageStride.CreateStrides
        (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        greenMonster = ImageStride.CreateStrides
        (2, Path.Combine("Assets", "Images", "GreenMonster.png"));

        squadron.CreateEnemies(blueMonster, greenMonster);
        movestrat = new NoMove();

        // adds playershots to the game
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        // adds explosions for when enimies are hit
        enemyExplosions = new AnimationContainer(1);
        explosionStrides = ImageStride.CreateStrides(8,
        Path.Combine("Assets", "Images", "Explosion.png"));
        gameOverScreen = new GameOver();
        GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }
    public void AddExplosion(Vec2F position, Vec2F extent) {
        ImageStride explosionStride = new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides);
        enemyExplosions.AddAnimation(new StationaryShape
            (position, extent),
            EXPLOSION_LENGTH_MS,
            explosionStride);
    }
    private void IterateShots() { // Checks if any shots have hit the border or any enemies
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) { // Shot hit border
                System.Console.WriteLine("delete shot");
                shot.DeleteEntity();
            } else {
                squadron.Enemies.Iterate(enemy => {
                    DynamicShape dynamicShot = shot.Shape.AsDynamicShape();
                    CollisionData collision = CollisionDetection.Aabb(dynamicShot, enemy.Shape);
                    if (collision.Collision) { // Shot hit enemy
                        System.Console.WriteLine("delete shot");
                        shot.DeleteEntity();
                        if (enemy.IsEnemyDead()) {
                            System.Console.WriteLine("delete enemy");
                            AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            enemy.DeleteEntity();
                        }
                    }
                });
            }
        });
    }
    private void NewMoveStrat() {
        if (level == 2) {
            movestrat = new Down();
        }
        else if (level == 5) {
            movestrat = new ZigZagDown();
        }
    }

    private void NewSquad() {
        if (squadron.Enemies.CountEntities() == 0 && health.Lives > 0) {
            squadronNum = (squadronNum + 1) % 3;
            level += 1;
            gameOverScreen.SetLevel(level);
            switch (squadronNum) {
                case 0:
                    squadron = new SquadronLine();
                    break;
                case 1:
                    squadron = new SquadronSquare();
                    break;
                case 2:
                    squadron = new SquadronTriangle();
                    break;
            }
            NewMoveStrat();
            squadron.CreateEnemies(blueMonster, greenMonster);
            foreach (Enemy enemy in squadron.Enemies) {
                enemy.IncreaseSpeed(level * 0.0002f);
            }
        }
    }
    public void ResetState() {
        instance = new GameRunning();
        instance.InitializeGameState();
    }
    public void UpdateState() {
        if (health.Lives > 0) {
            squadron.Enemies.Iterate(enemy => {
                if (enemy.Shape.Position.Y < 0.0f) {
                    enemy.DeleteEntity();
                    health.LoseHealth();
                }
            });
            NewSquad();
            movestrat.MoveEnemies(squadron.Enemies);
            player.Move();
            IterateShots();
        }
    }
    public void RenderState() {
        backGroundImage.RenderEntity();
        if (health.Lives > 0) {
            player.Render();
            squadron.Enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
            health.RenderHealth();
        } else {
            gameOverScreen.Render();
        }
    }
    private void KeyPress(KeyboardKey key) { // When a key is pressed
        switch (key) {
            case KeyboardKey.Left:
                GalagaBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE LEFT"
                });
                break;
            case KeyboardKey.Right:
                GalagaBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE RIGHT"
                });
                break;
            case KeyboardKey.Escape:
                GalagaBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                });
                    break;
        }
    }
    private void KeyRelease(KeyboardKey key) { // When a key is realeased
        switch (key) {
            case KeyboardKey.Left:
                GalagaBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "REALESE LEFT"
                });
                break;
            case KeyboardKey.Right:
                GalagaBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "REALESE RIGHT"
                });
                break;
            case KeyboardKey.Space:
                playerShots.AddEntity(new PlayerShot(
                    player.GetPositionMiddle(), playerShotImage));
                break;
        }
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }
}
