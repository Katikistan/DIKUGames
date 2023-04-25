using Breakout.Levels;
using System.IO;
namespace BreakoutTests.Levels;
[TestFixture]
public class LevelLoaderTests
{
    private LevelLoader ?levelLoader;
    [SetUp]
    public void Setup() {
        levelLoader = new LevelLoader(Path.Combine ("..","..","..","Assets", "Levels"));
    }
// test man kan loade et nyt level
    [Test]
    public void LoadLevelTest() {
        bool validFile = levelLoader.LoadLevel("level1.txt");
        bool invalidFile = levelLoader.LoadLevel("nolevel.txt");
        Assert.That(validFile, Is.EqualTo(true));
        Assert.That(invalidFile, Is.EqualTo(false));
    }
    [Test]
    public void ReadMapTest() {
        levelLoader.LoadLevel("level1.txt");
        Assert.That(levelLoader.Map[0][0], Is.EqualTo('-'));
        Assert.That(levelLoader.Map[2][1], Is.EqualTo('a'));
        Assert.That(levelLoader.Map[4][1], Is.EqualTo('0'));
        Assert.That(levelLoader.Map[4][8], Is.EqualTo('0'));
        Assert.That(levelLoader.Map[5][5], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[6][5], Is.EqualTo('1'));
        Assert.That(levelLoader.Map[7][5], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[10][10], Is.EqualTo('%'));
        Assert.That(levelLoader.Map[11][10], Is.EqualTo('-'));
        levelLoader.LoadLevel("level2.txt");
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
        ArgumentException? ExceptionTest = Assert.Throws<ArgumentException>(() =>
        levelLoader.LoadLevel("NoMap.txt"));
        Assert.That(ExceptionTest?.Message, Is.EqualTo("Error: no map found in level file"));
    }
    [Test]
    public void ReadMetaTest() {
        levelLoader.LoadLevel("level1.txt");
        Assert.That(levelLoader.Meta["Name"], Is.EqualTo("LEVEL 1"));
        Assert.That(levelLoader.Meta["Time"], Is.EqualTo("300"));
        Assert.That(levelLoader.Meta["#"], Is.EqualTo("Hardened"));
        Assert.That(levelLoader.Meta["2"], Is.EqualTo("PowerUp"));
        levelLoader.LoadLevel("level2.txt");
        Assert.That(levelLoader.Meta["Name"], Is.EqualTo("LEVEL 2"));
        Assert.That(levelLoader.Meta["Time"], Is.EqualTo("180"));
        Assert.That(levelLoader.Meta["i"], Is.EqualTo("PowerUp"));
        ArgumentException? ExceptionTest = Assert.Throws<ArgumentException>(() =>
        levelLoader.LoadLevel("NoMeta.txt"));
        Assert.That(ExceptionTest?.Message, Is.EqualTo("Error: no Meta found in level file"));
    }
    [Test]
    public void ReadLegendTest() {
        levelLoader.LoadLevel("level1.txt");
        Assert.That(levelLoader.Legend['%'], Is.EqualTo("blue-block.png"));
        Assert.That(levelLoader.Legend['0'], Is.EqualTo("grey-block.png"));
        Assert.That(levelLoader.Legend['1'], Is.EqualTo("orange-block.png"));
        Assert.That(levelLoader.Legend['a'], Is.EqualTo("purple-block.png"));
        levelLoader.LoadLevel("level2.txt");
        Assert.That(levelLoader.Legend['h'], Is.EqualTo("green-block.png"));
        Assert.That(levelLoader.Legend['i'], Is.EqualTo("teal-block.png"));
        Assert.That(levelLoader.Legend['j'], Is.EqualTo("blue-block.png"));
        Assert.That(levelLoader.Legend['k'], Is.EqualTo("brown-block.png"));
        ArgumentException? ExceptionTest = Assert.Throws<ArgumentException>(() =>
        levelLoader.LoadLevel("NoLegend.txt"));
        Assert.That(ExceptionTest?.Message, Is.EqualTo("Error: no Legend found in level file"));
    }
}

