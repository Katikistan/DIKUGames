using Breakout.Levels;
using System;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.LevelLoading;
[TestFixture]
public class LevelLoaderTests {
    private LevelLoader levelLoader = null!;
    [SetUp]
    public void Setup() {
        levelLoader = new LevelLoader();
        levelLoader.ChangePath(Path.Combine(@"../../../../", "Breakout", "Assets", "Levels"));
    }
    // test man kan loade et nyt level
    [Test]
    public void TestReadLevel() {
        bool validFile = levelLoader.ReadLevel("level1.txt");
        bool invalidFile = levelLoader.ReadLevel("noneExistingLevelFile.txt");
        Assert.That(validFile, Is.EqualTo(true));
        Assert.That(invalidFile, Is.EqualTo(false));
    }
    [Test]
    public void TestReadMap() {
        // Testing map is read correctly
        levelLoader.ReadLevel("level1.txt");
        Assert.That(levelLoader.Map![0][0], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[2][1], Is.EqualTo('a'));
        Assert.That(levelLoader.Map[4][1], Is.EqualTo('0'));
        Assert.That(levelLoader.Map[4][8], Is.EqualTo('0'));
        Assert.That(levelLoader.Map[5][5], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[6][5], Is.EqualTo('1'));
        Assert.That(levelLoader.Map[7][5], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[10][10], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[11][10], Is.EqualTo('-'));
        // Test new map is read
        levelLoader.ReadLevel("level2.txt");
        Assert.That(levelLoader.Map[0][0], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[2][1], Is.EqualTo('h'));
        Assert.That(levelLoader.Map[4][1], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[4][8], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[5][4], Is.EqualTo('j'));
        Assert.That(levelLoader.Map[5][5], Is.EqualTo('i'));
        Assert.That(levelLoader.Map[6][5], Is.EqualTo('j'));
        Assert.That(levelLoader.Map[7][5], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[8][5], Is.EqualTo('k'));
        Assert.That(levelLoader.Map[10][10], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[11][10], Is.EqualTo('-'));
    }
    [Test]
    public void TestReadMeta() {
        levelLoader.ReadLevel("level1.txt");
        Assert.That(levelLoader.Meta!["Name"], Is.EqualTo("LEVEL 1"));
        Assert.That(levelLoader.Meta["Time"], Is.EqualTo("300"));
        Assert.That(levelLoader.Meta["Hardened"], Is.EqualTo("#"));
        Assert.That(levelLoader.Meta["PowerUp"], Is.EqualTo("2"));
        levelLoader.ReadLevel("level2.txt");
        // Meta changes to new level
        Assert.That(levelLoader.Meta["Name"], Is.EqualTo("LEVEL 2"));
        Assert.That(levelLoader.Meta["Time"], Is.EqualTo("180"));
        Assert.That(levelLoader.Meta["PowerUp"], Is.EqualTo("i"));
    }

    [Test]
    public void TestReadLegend() {
        levelLoader.ReadLevel("level1.txt");
        Assert.That(levelLoader.Legend!['%'], Is.EqualTo("blue-block.png"));
        Assert.That(levelLoader.Legend['0'], Is.EqualTo("grey-block.png"));
        Assert.That(levelLoader.Legend['1'], Is.EqualTo("orange-block.png"));
        Assert.That(levelLoader.Legend['a'], Is.EqualTo("purple-block.png"));
        levelLoader.ReadLevel("level2.txt");
        Assert.That(levelLoader.Legend['h'], Is.EqualTo("green-block.png"));
        Assert.That(levelLoader.Legend['i'], Is.EqualTo("teal-block.png"));
        Assert.That(levelLoader.Legend['j'], Is.EqualTo("blue-block.png"));
        Assert.That(levelLoader.Legend['k'], Is.EqualTo("brown-block.png"));
    }
}

