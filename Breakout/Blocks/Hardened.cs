using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Blocks;
public class Hardened : Block, IGameEventProcessor {

    internal new int value = 200;
    internal new int health = 2;
    private string DamagedImg;
    private string Damaged;
    /// <summary>
    /// Amount of points given to player when block is destroyed.
    /// </summary>
    public Hardened(StationaryShape shape, string imageFile) : base(shape, imageFile) {
        position = new Vec2F(shape.Position.X, shape.Position.Y);
        DamagedImg = imageFile.Insert(imageFile.Length-4, "-damaged");
        Damaged = Path.Combine("..", "Breakout", "Assets", "Images", DamagedImg);
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent,this);
    }
    /// <summary>
    /// Decreases Block health, if health is less than 1 the block is marked for deletion.
    /// </summary>
    public void ProcessEvent(GameEvent gameEvent) {
        System.Console.WriteLine("HERE");
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "HIT":
                    System.Console.WriteLine("HERE");
                    LoseHealth();
                    break;
            }
        }
    }
    public override void LoseHealth() {
        health -= 1;
        if (health == 1 && File.Exists(Damaged)) {
            Image = new Image(Damaged);
        } else if (health == 0) {
            DeleteEntity();
            GivePoints();
        }
    }
}