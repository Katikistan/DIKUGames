using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout.Powerups;
/// <summary>
/// Default block has 1 health points and grants player 10 points when destroyed.
/// </summary>
public class Extralife : Entity, IPowerup {
    public Extralife(Shape shape) : base(shape, new Image()) {

    }
    public void Update() {}
    public void Render() {}
    public void Remove() {}
}