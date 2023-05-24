using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;

namespace Breakout.Powerups;

public static class PowerUpCreator{
    public static Powerup CreatePowerUp(Vec2F pos) {
        Random random = new Random();
        switch (random.Next(1, 8)) {
            case 1:
                return new SlimJim(new DynamicShape(
                pos,
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
            case 2:
                return new SlimJim(new DynamicShape(
                pos,
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
            default:
                return new SlimJim(new DynamicShape(
                pos,
                new Vec2F(0.03f, 0.03f),
                new Vec2F(0.00f, -0.01f)));
        }
    }
}

