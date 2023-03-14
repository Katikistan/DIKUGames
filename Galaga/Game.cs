using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;
namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private Player player;
    private GameEventBus eventBus; // For keyboard input
    private EntityContainer<Enemy> enemies;
    // Fields for playershots
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    // Fields for explosion
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        // Allows for user keyboard input
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);

        // Creates a player object for the game
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));

        // Adds enemies to the game
        List<Image> images = ImageStride.CreateStrides 
        (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
        }
        // adds playershots to the game
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        // adds explosions for when enimies are hit
        enemyExplosions = new AnimationContainer(numEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
        Path.Combine("Assets", "Images", "Explosion.png"));
    }
    private void IterateShots() { // Checks if any shots have hit the border or any enemies
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) { // Shot hit border
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => { 
                    DynamicShape dynamicShot = shot.Shape.AsDynamicShape();
                    CollisionData collision = CollisionDetection.Aabb(dynamicShot,enemy.Shape);
                    if (collision.Collision) { // Shot hit enemy
                        shot.DeleteEntity();
                        AddExplosion(enemy.Shape.Position,enemy.Shape.Extent);
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch (gameEvent) {
                case :
                    
                    break;
                case :
                    
                    break;
            }
        }
    }

    private void KeyPress(KeyboardKey key) { // When a key is pressed
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) { // When a key is realeased 
         switch (key) {
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
            case KeyboardKey.Space: 
                playerShots.AddEntity(new PlayerShot(
                    player.GetPosition(), playerShotImage));
                break;
        }
    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        } 
    }
    public void AddExplosion(Vec2F position, Vec2F extent) { 
        ImageStride explosionStride = new ImageStride(EXPLOSION_LENGTH_MS/8,explosionStrides);
        enemyExplosions.AddAnimation(new StationaryShape(position,extent),EXPLOSION_LENGTH_MS,
        explosionStride);
    }

    public void ProcessEvent(GameEvent gameEvent) {
    // Leave this empty for now
    }
    public override void Render() { //Rendering entities
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();

    }
    public override void Update() {
        player.Move();
        IterateShots();
    }
    
}
