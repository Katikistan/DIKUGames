using Breakout.Levels;
using Breakout;

namespace BreakoutTests;

public class Tests
{
    public LevelLoader ?level;
    [SetUp]
    public void Setup() {
        level = new LevelLoader();
        // level.LoadLevel("level1.txt");
        // level.LoadLevel("emoptylevel.txt");

    }

    [Test]
    public void Test1() {
        Level a = level.LoadLevel("level1.txt");
        Assert.Pass(a.Map[5][3] == '0');
    }
}