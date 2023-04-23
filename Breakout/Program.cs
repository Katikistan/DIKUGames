using DIKUArcade.GUI;
using System;

namespace Breakout;
class Program {
    static void Main(string[] args) {
            var windowArgs = new WindowArgs() {Title = "Breakout"};
            var game = new Game(windowArgs);
            game.Run();
        }
}
