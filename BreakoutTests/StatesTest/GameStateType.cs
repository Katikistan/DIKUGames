// using System;
// using Galaga.GalagaStates;
// namespace galagaTests;
// [TestFixture]
// public class TestTransformState {
//     [Test]
//     public void TestTransformStringToState() {
//         GameStateType GameRunning = StateTransformer.TransformStringToState("GAME_RUNNING");
//         GameStateType GamePaused = StateTransformer.TransformStringToState("GAME_PAUSED");
//         GameStateType MainMenu = StateTransformer.TransformStringToState("MAIN_MENU");
//         Assert.That(GameRunning, Is.EqualTo(GameStateType.GameRunning));
//         Assert.That(GamePaused, Is.EqualTo(GameStateType.GamePaused));
//         Assert.That(MainMenu, Is.EqualTo(GameStateType.MainMenu));
//         ArgumentException? ExceptionTest = Assert.Throws<ArgumentException>(() =>
//         StateTransformer.TransformStringToState(""));
//         Assert.That(ExceptionTest?.Message, Is.EqualTo("Invalid GameStateType string"));
//     }
//     [Test]
//     public void TransformStateToString() {
//         string GameRunning = StateTransformer.TransformStateToString(GameStateType.GameRunning);
//         string GamePaused = StateTransformer.TransformStateToString(GameStateType.GamePaused);
//         string MainMenu = StateTransformer.TransformStateToString(GameStateType.MainMenu);
//     }
// }