using Breakout.Levels;

namespace BreakoutTests.Levels;
[TestFixture]
public class LevelLoaderTests
{
    private LevelLoader ?levelLoader;
    [SetUp]
    public void Setup() {
        levelLoader = new LevelLoader();
    }

    [Test]
    public void Test1() {
        levelLoader.ReadLevel("level1.txt");
        Assert.That(levelLoader.Map[5][3], Is.EqualTo('0'));
    }
}