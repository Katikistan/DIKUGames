using System.IO;
using System.Collections.Generic;

namespace Breakout.Levels;
public class LoadLevel {
    private string path;
    private  string[] txtlines;
    public Dictionary<char,string> Meta;
    public Dictionary<char,string> Legend;
    public char[][] Map;
    public LoadLevel(string level) {
        this.path = Path.Combine(
        "..", "Breakout", "Assets", "Levels", level);
        this.txtlines = File.ReadAllLines(path);
    }
    // static level loader
    public void LoadLevel(path);
    private void ReadMap()
    private void ReadMeta()
    private void ReadLegend()

}


