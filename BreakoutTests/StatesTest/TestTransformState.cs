using Breakout.States;
namespace BreakoutTests.States;
[TestFixture]
public class TestTransformState {
    [Test]
    public void TestTransformStringToState() {
        GameStateType GameRunning = StateTransformer.TransformStringToState("GAME_RUNNING");
        GameStateType GamePaused = StateTransformer.TransformStringToState("GAME_PAUSED");
        GameStateType MainMenu = StateTransformer.TransformStringToState("MAIN_MENU");
        GameStateType GameLost = StateTransformer.TransformStringToState("GAME_LOST");
        Assert.That(GameRunning, Is.EqualTo(GameStateType.GameRunning));
        Assert.That(GamePaused, Is.EqualTo(GameStateType.GamePaused));
        Assert.That(MainMenu, Is.EqualTo(GameStateType.MainMenu));
        Assert.That(GameLost, Is.EqualTo(GameStateType.GameLost));
        ArgumentException ExceptionTest = Assert.Throws<ArgumentException>(() =>
        StateTransformer.TransformStringToState(""));
        Assert.That(ExceptionTest?.Message, Is.EqualTo("Invalid GameStateType string"));
    }
}