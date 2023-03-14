using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
namespace Galaga;
public class Player : IGameEventProcessor {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
    
    private Entity entity;
    private DynamicShape shape;
    private GameEventBus eventBus; // For keyboard input
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent });
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
    }
    private void KeyPress(KeyboardKey key) { // When a key is pressed
        eventBus.RegisterEvent (new GameEvent {
            EventType = [from], [to], [message], [StringArg1], [StringArg2],
            [ObjectArg1], [IntArg1], [Id]
            });

        switch (key) {
            case KeyboardKey.Left:
                SetMoveLeft(true);
                break;
            case KeyboardKey.Right:
                SetMoveRight(true);
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) { // When a key is realeased 
         switch (key) {
            case KeyboardKey.Left:
                SetMoveLeft(false);
                break;
            case KeyboardKey.Right:
                SetMoveRight(false);
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

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch (gameEvent) {
                case Left:
                    player.Move
                    break;
                case Right:
                    player.Move
                    break;
            }
        }
    }
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;

    }
    private void Move() {
        shape.Move();
        if (shape.Position.X <= 0.0f) {
            shape.Position.X = 0.0f; 
        }
        else if ((shape.Position.X + shape.Extent.X) >= 1.0f) {
                shape.Position.X = 1.0f - shape.Extent.X;
        }
 
    }
    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = -MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }
    private void SetMoveRight(bool val) {
        if (val) {
            moveRight = MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        }
        UpdateDirection();
    }
    public Vec2F GetPosition() { 
        //Position adjusted to make bullets shot from middle of ship.
        Vec2F position = new Vec2F (shape.Position.X + shape.Extent.X / 2.0f, shape.Position.Y);
        return (position);
    } 
    
    public void Render() {
        entity.RenderEntity();    
    }
}
