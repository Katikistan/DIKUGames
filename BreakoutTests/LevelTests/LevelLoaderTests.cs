using Breakout.Levels;
using System.IO;
namespace BreakoutTests.Levels;
[TestFixture]
public class LevelLoaderTests
{
    private LevelLoader ?levelLoader;
    [SetUp]
    public void Setup() {
        string path = Path.Combine("..", "BreakoutTests", "Assets", "Levels");
        levelLoader = new LevelLoader(path);
    }
// test man kan loade et nyt level
    [Test]
    public void ReadLevelTest() {
        bool validFile = levelLoader.ReadLevel("level1.txt");
        bool invalidFile = levelLoader.ReadLevel("");
        Assert.That(validFile, Is.EqualTo(true));
        Assert.That(invalidFile, Is.EqualTo(false));
    }
    [Test]
    public void ReadMapTest() {
        levelLoader.ReadLevel("level1.txt");
        Assert.That(levelLoader.Map[0][0], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[2][1], Is.EqualTo('a'));
        Assert.That(levelLoader.Map[4][1], Is.EqualTo('0'));
        Assert.That(levelLoader.Map[4][8], Is.EqualTo('0'));
        Assert.That(levelLoader.Map[5][4], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[6][4], Is.EqualTo('1'));
        Assert.That(levelLoader.Map[7][4], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[10][10], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[11][10], Is.EqualTo('-'));
        levelLoader.ReadLevel("level2.txt");
        Assert.That(levelLoader.Map[0][0], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[2][1], Is.EqualTo('h'));
        Assert.That(levelLoader.Map[4][1], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[4][8], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[5][4], Is.EqualTo('j'));
        Assert.That(levelLoader.Map[5][4], Is.EqualTo('i'));
        Assert.That(levelLoader.Map[6][4], Is.EqualTo('j'));
        Assert.That(levelLoader.Map[7][4], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[7][4], Is.EqualTo('k'));
        Assert.That(levelLoader.Map[10][10], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[11][10], Is.EqualTo('-'));
        // levelLoader.ReadLevel("Nomap.txt");
    }
    [Test]
    public void ReadMetaTest() {
        levelLoader.ReadLevel("level1.txt");
        Assert.That(levelLoader.Meta[""], Is.EqualTo(""));
        levelLoader.ReadLevel("level2.txt");
        Assert.That(levelLoader.Meta[""], Is.EqualTo(""));
        // levelLoader.ReadLevel("NoMeta.txt");

    }
    [Test]
    public void ReadLegendTest() {
        levelLoader.ReadLevel("level1.txt");
        Assert.That(levelLoader.Legend['%'], Is.EqualTo("blue-block.png"));
        Assert.That(levelLoader.Legend['0'], Is.EqualTo("grey-block.png"));
        Assert.That(levelLoader.Legend['1'], Is.EqualTo("orange-block.png"));
        Assert.That(levelLoader.Legend['a'], Is.EqualTo("purple-block.png"));
        levelLoader.ReadLevel("level2.txt");
        Assert.That(levelLoader.Legend['h'], Is.EqualTo("green-block.png"));
        Assert.That(levelLoader.Legend['i'], Is.EqualTo("teal-block.png"));
        Assert.That(levelLoader.Legend['j'], Is.EqualTo("blue-block.png"));
        Assert.That(levelLoader.Legend['k'], Is.EqualTo("brown-block.png"));
        // levelLoader.ReadLevel("NoLe.txt");
    }
}

