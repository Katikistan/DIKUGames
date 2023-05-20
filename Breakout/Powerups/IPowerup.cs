namespace Breakout.Powerups;
public interface IPowerup {
    void Update();
    void Render();
    void Remove(); // et eller anden måde skal vi fjerne powerups efter tid.
}
