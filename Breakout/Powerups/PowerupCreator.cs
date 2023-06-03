using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;

namespace Breakout.Powerups;
/// <summary>
///  Creates a Powerup
/// </summary>
public static class PowerUpCreator {
    private static Vec2F extent = new Vec2F(0.03f, 0.03f);
    private static Vec2F dir = new Vec2F(0.00f, -0.01f);
    /// <summary>
    ///  Creates a random powerup
    /// </summary>
    public static Powerup CreatePowerUp(Vec2F pos) {
        Random random = new Random();
        switch (random.Next(1, 8)) {
            case 1:
                return new LifePlus(new DynamicShape(
                pos,
                extent,
                dir));
            case 2:
                return new LifeLoss(new DynamicShape(
                pos,
                extent,
                dir));
            case 3:
                return new Wide(new DynamicShape(
                pos,
                extent,
                dir));
            case 4:
                return new SlimJim(new DynamicShape(
                pos,
                extent,
                dir));
            case 5:
                return new PlayerSpeed(new DynamicShape(
                pos,
                extent,
                dir));
            case 6:
                return new Split(new DynamicShape(
                pos,
                extent,
                dir));
            case 7:
                return new HardBall(new DynamicShape(
                pos,
                extent,
                dir));
            default:
                return new LifePlus(new DynamicShape(
                pos,
                extent,
                dir));
        }
    }
}

