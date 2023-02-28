using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

class PlayerShot : Entity {
    private static Vec2F extent = new Vec2F(0.008f, 0.021f);
    private static Vec2F direction = new Vec2F(0.0f, 0.1f);
    PlayerShot(Vec2F position,Shape shape, IBaseImage image) : base(shape, image) {
        // entity = new Entity(shape, image);
        // this.shape = shape;
    }

}