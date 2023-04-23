using System.IO;
using System.Collections.Generic;

namespace Breakout.Levels;
public class LevelLoader {
    private string path;
    private  string[] txtlines;
    public Dictionary<string,string> Meta;
    public Dictionary<char,string> Legend;
    public char[][] Map;
    public LevelLoader() {
    }
    public void LoadLevel(string level) {
        //tjek om filen eksisterer
        this.path = Path.Combine(
        "..", "Breakout", "Assets", "Levels", level);
        this.txtlines = File.ReadAllLines(path);
        ReadMap();
        System.Console.WriteLine(Map[5][3]);
        ReadMeta();
        System.Console.WriteLine(Meta["Name"]);
        ReadLegend();
        System.Console.WriteLine(Legend['a']);

    }
    private void ReadMap() {
        int MapStart = Array.IndexOf(txtlines, "Map:");
        int MapEnd = Array.IndexOf(txtlines, "Map/");
        Map = new char[MapEnd - 2][];
        for (int i = MapStart + 1; i < MapEnd - 1; i++) {
            Map[i-1] = txtlines[i].ToCharArray();
        }
    }
    private void ReadMeta(){
        int MetaStart = Array.IndexOf(txtlines, "Meta:");
        int MetaEnd = Array.IndexOf(txtlines, "Meta/");
        Meta = new Dictionary<string, string>();
        for (int i = MetaStart + 1; i < MetaEnd; i++)
        { // ikke rigtigt det skal ind i en dictonary
            string line = txtlines[i];
            string[] parts = line.Split(": ");
            string key = parts[0];
            string value = parts[1];
            Meta[key] = Value;
        }
    }
    private void ReadLegend() {
        int legendStart = Array.IndexOf(txtlines, "Legend:");
        int legendEnd = Array.IndexOf(txtlines, "Legend/");
        Legend = new Dictionary<char, string>();
        for (int i = legendStart+1; i < legendEnd; i++){
            char symbol = txtlines[i][0];
            string imagefile = txtlines[i].Substring(2);
            Legend[symbol] = imagefile;
        }
    }
}


