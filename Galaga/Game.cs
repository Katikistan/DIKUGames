using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System.Collections.Generic;
using System.IO;
using Galaga.Squadron;
namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private GameEventBus eventBus;
    private Player player;    
    
    // enemy fields
    private ISquadron squadron = new SquadronLine();
    private List<Image> blueMonster;
    private List<Image> greenMonster;
    
    // Fields for playershots
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    
    // Fields for explosion
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        // Creates a player object for the game
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));

        // Eventbus and eventypes subscribed to
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(
            new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent, 
                GameEventType.PlayerEvent,
                GameEventType.MovementEvent
            });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        // eventBus.Subscribe(GameEventType.MovementEvent, squadron);

        // Adds enemies to the game
        // images = ImageStride.CreateStrides 
        // (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        blueMonster = ImageStride.CreateStrides 
        (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        greenMonster = ImageStride.CreateStrides 
        (2, Path.Combine("Assets", "Images", "GreenMonster.png"));
        squadron.CreateEnemies(blueMonster,greenMonster);
        
        // adds playershots to the game
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        
        // adds explosions for when enimies are hit
        enemyExplosions = new AnimationContainer(1);
        explosionStrides = ImageStride.CreateStrides(8,
        Path.Combine("Assets", "Images", "Explosion.png"));
    }
    private void IterateShots() { // Checks if any shots have hit the border or any enemies
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) { // Shot hit border
                shot.DeleteEntity();
            } else {
                squadron.Enemies.Iterate(enemy => { 
                    DynamicShape dynamicShot = shot.Shape.AsDynamicShape();
                    CollisionData collision = CollisionDetection.Aabb(dynamicShot,enemy.Shape);
                    if (collision.Collision) { // Shot hit enemy
                        shot.DeleteEntity();
                        if (enemy.IsEnemyDead()) {
                            AddExplosion(enemy.Shape.Position,enemy.Shape.Extent);
                            enemy.DeleteEntity();
                        }
                    }
                });
            }
        });
    }
    private void RandSquad() {
        System.Random rand = new System.Random();
        if (squadron.Enemies.CountEntities() == 0) {
            switch (rand.Next(3)) {
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
            squadron.CreateEnemies(blueMonster,greenMonster);
        }
        
    }
    private void KeyPress(KeyboardKey key) { // When a key is pressed
        switch (key) {
            case KeyboardKey.Escape:
                    eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.WindowEvent,
                    Message = "WINDOW CLOSE"
                    });
                break;
            case KeyboardKey.Left:
                    eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE LEFT"
                    });
                break;
            case KeyboardKey.Right:
                    eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE RIGHT"
                    });
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) { // When a key is realeased 
         switch (key) {
            case KeyboardKey.Left:
                    eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "REALESE LEFT"
                    });
                break;
            case KeyboardKey.Right:
                    eventBus.RegisterEvent (new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "REALESE RIGHT"
                    });
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
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
                    case "WINDOW CLOSE":
                        window.CloseWindow();
                        break;
            }
        }   
    }
    public override void Render() { //Rendering entities
        player.Render();
        squadron.Enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();

    }
    public override void Update() {
        RandSquad();
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }
    
}
