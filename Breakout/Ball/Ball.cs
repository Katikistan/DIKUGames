using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Balls;
public class Ball : Entity {
    public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {

    }
}