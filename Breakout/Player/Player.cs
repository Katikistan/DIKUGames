using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Player;
public class Player : Entity, IGameEventProcessor {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }
    public void Render() {
        RenderEntity();
    }
    public void ProcessEvent(GameEvent gameEvent) {}
}