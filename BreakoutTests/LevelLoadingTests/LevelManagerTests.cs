using Breakout.Levels;
using Breakout.Blocks;
using Breakout.Balls;
using DIKUArcade.Math;
using Breakout.Players;
using DIKUArcade.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.LevelLoading;

[TestFixture]
public class LevelManagerTests {
    LevelManager levelManager;
    [SetUp]
    public void Setup() {
        levelManager = new LevelManager(); 
        levelManager.NewLevel("level1.txt");
    }
    [Test]
    public void TestNewLevel() {
        Assert.AreEqual(levelManager.EmptyLevel(), true);
        Assert.That(levelManager.blocks is EntityContainer<Block>);
        Assert.That(levelManager.Ball is Ball);
        Assert.That(levelManager.Player is Player);
    }
    [Test]
    public void TestUpdate() {
        Vec2F startPlayer = new Vec2F(levelManager.Player._Shape.Position.X, levelManager.Player._Shape.Position.Y);
        Vec2F startBall = new Vec2F(levelManager.Ball._Shape.Position.X, levelManager.Ball._Shape.Position.Y);
        levelManager.Update();
        Vec2F currentPlayer = new Vec2F(levelManager.Player._Shape.Position.X, levelManager.Player._Shape.Position.Y);
        Vec2F currentBall = new Vec2F(levelManager.Ball._Shape.Position.X, levelManager.Ball._Shape.Position.Y);
        Assert.AreEqual(startPlayer.X, currentPlayer.X);
        Assert.AreEqual(startPlayer.Y, currentPlayer.Y);
        Assert.AreNotEqual(startBall.X, currentBall.X);
        Assert.AreNotEqual(startBall.Y, currentBall.Y);
    }
}