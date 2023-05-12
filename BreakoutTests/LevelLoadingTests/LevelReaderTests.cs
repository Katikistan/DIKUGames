using Breakout.Levels;
using System;
using System.Collections.Generic;
using System.IO;

namespace BreakoutTests.LevelLoading;
[TestFixture]
public class LevelReaderTests {
    private LevelReader levelReader = null!;
    [SetUp]
    public void Setup() {
        levelReader = new LevelReader();
    }
    // test man kan loade et nyt level
    [Test]
    public void TestReadLevel() {
        bool validFile = levelReader.ReadLevel("level1.txt");
        bool invalidFile = levelReader.ReadLevel("noneExistingLevelFile.txt");
        Assert.That(validFile, Is.EqualTo(true));
        Assert.That(invalidFile, Is.EqualTo(false));
    }
    [Test]
    public void TestReadMap() {
        // Testing map is read correctly
        levelReader.ReadLevel("level1.txt");
        Assert.That(levelReader.Map![0][0], Is.EqualTo('-'));
        Assert.That(levelReader.Map[2][1], Is.EqualTo('a'));
        Assert.That(levelReader.Map[4][1], Is.EqualTo('0'));
        Assert.That(levelReader.Map[4][8], Is.EqualTo('0'));
        Assert.That(levelReader.Map[5][5], Is.EqualTo('%'));
        Assert.That(levelReader.Map[6][5], Is.EqualTo('1'));
        Assert.That(levelReader.Map[7][5], Is.EqualTo('%'));
        Assert.That(levelReader.Map[10][10], Is.EqualTo('%'));
        Assert.That(levelReader.Map[11][10], Is.EqualTo('-'));
        // Test new map is read
        levelReader.ReadLevel("level2.txt");
        Assert.That(levelReader.Map[0][0], Is.EqualTo('-'));
        Assert.That(levelReader.Map[2][1], Is.EqualTo('h'));
        Assert.That(levelReader.Map[4][1], Is.EqualTo('-'));
        Assert.That(levelReader.Map[4][8], Is.EqualTo('-'));
        Assert.That(levelReader.Map[5][4], Is.EqualTo('j'));
        Assert.That(levelReader.Map[5][5], Is.EqualTo('i'));
        Assert.That(levelReader.Map[6][5], Is.EqualTo('j'));
        Assert.That(levelReader.Map[7][5], Is.EqualTo('-'));
        Assert.That(levelReader.Map[8][5], Is.EqualTo('k'));
        Assert.That(levelReader.Map[10][10], Is.EqualTo('-'));
        Assert.That(levelReader.Map[11][10], Is.EqualTo('-'));
    }
    [Test]
    public void TestReadMeta() {
        levelReader.ReadLevel("level1.txt");
        Assert.That(levelReader.Meta!["Name"], Is.EqualTo("LEVEL 1"));
        Assert.That(levelReader.Meta["Time"], Is.EqualTo("300"));
        Assert.That(levelReader.Meta["#"], Is.EqualTo("Hardened"));
        Assert.That(levelReader.Meta["2"], Is.EqualTo("PowerUp"));
        levelReader.ReadLevel("level2.txt");
        // Meta changes to new level
        Assert.That(levelReader.Meta["Name"], Is.EqualTo("LEVEL 2"));
        Assert.That(levelReader.Meta["Time"], Is.EqualTo("180"));
        Assert.That(levelReader.Meta["i"], Is.EqualTo("PowerUp"));
    }

    [Test]
    public void TestReadLegend() {
        levelReader.ReadLevel("level1.txt");
        Assert.That(levelReader.Legend!['%'], Is.EqualTo("blue-block.png"));
        Assert.That(levelReader.Legend['0'], Is.EqualTo("grey-block.png"));
        Assert.That(levelReader.Legend['1'], Is.EqualTo("orange-block.png"));
        Assert.That(levelReader.Legend['a'], Is.EqualTo("purple-block.png"));
        levelReader.ReadLevel("level2.txt");
        Assert.That(levelReader.Legend['h'], Is.EqualTo("green-block.png"));
        Assert.That(levelReader.Legend['i'], Is.EqualTo("teal-block.png"));
        Assert.That(levelReader.Legend['j'], Is.EqualTo("blue-block.png"));
        Assert.That(levelReader.Legend['k'], Is.EqualTo("brown-block.png"));
    }
    [Test]
    public void TestInvalidMeta() {
        levelReader.ReadLevel("NoMeta.txt");
        Assert.That(levelReader.Meta, Is.EqualTo(null));
        string meta;
        levelReader.ReadLevel("level1.txt");
        // Testing that there is meta
        Assert.That(levelReader.Meta != null);
        // Trying to get meta that's not in dictionary
        levelReader.Meta!.TryGetValue("nometa", out meta!);
        Assert.That(meta, Is.EqualTo(null));
    }
    [Test]
    public void TestInvalidLegend() {
        levelReader.ReadLevel("NoLegend.txt");
        Assert.That(levelReader.Legend, Is.EqualTo(null));
        levelReader.ReadLevel("level1.txt");
        Assert.That(levelReader.Legend != null);
        string legend;
        // Asserting that there is no picture for this key
        levelReader.Legend!.TryGetValue('T', out legend!);
        Assert.That(legend, Is.EqualTo(null));
    }
}

