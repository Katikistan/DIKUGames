using System.IO;
using System.Collections.Generic;
namespace Breakout.Levels;
public class LevelLoader {
    private string levelFile;
    private string path;
    private  string[] txtlines;
    public Dictionary<string,string> ?Meta = null;
    public Dictionary<char,string> ?Legend = null;
    public char[][] ?Map = null;
    public LevelLoader(string path) {
        this.path = path;
    }
    public bool LoadLevel(string level) {
        this.levelFile = Path.Combine (path, level);
        bool fileExists = File.Exists(levelFile);
        if (fileExists) {
            this.txtlines = File.ReadAllLines(levelFile);
            ReadMap();
            ReadMeta();
            ReadLegend();
            return fileExists;
            System.Console.WriteLine(Map[5][3]);
        } else {
            return fileExists;
        }
    }
    private void ReadMap() {
        //tjekker om der er et map i filen
        if (Array.IndexOf(txtlines, "Map:") == -1) {
            throw new ArgumentException("Error: no map found in level file");
        }
        int MapStart = Array.IndexOf(txtlines, "Map:");
        int MapEnd = Array.IndexOf(txtlines, "Map/");
        Map = new char[MapEnd - 2][];
        for (int i = MapStart + 1; i < MapEnd - 1; i++) {
            Map[i-1] = txtlines[i].ToCharArray();
        }
    }
    private void ReadMeta(){
        //tjekker om der er Meta i filen
        if (Array.IndexOf(txtlines, "Meta:") == -1) {
            throw new ArgumentException("Error: no Meta found in level file");
        }
        int MetaStart = Array.IndexOf(txtlines, "Meta:");
        int MetaEnd = Array.IndexOf(txtlines, "Meta/");
        Meta = new Dictionary<string, string>();
        for (int i = MetaStart + 1; i < MetaEnd; i++) {
            string line = txtlines[i];
            string[] parts = line.Split(": ");
            string key = parts[0];
            string value = parts[1];
            Meta[key] = value;
        }
    }
    private void ReadLegend() {
        //tjekker om der er Legend i filen
        if (Array.IndexOf(txtlines, "Legend:") == -1) {
            throw new ArgumentException("Error: no Legend found in level file");
        }
        int legendStart = Array.IndexOf(txtlines, "Legend:");
        int legendEnd = Array.IndexOf(txtlines, "Legend/");
        Legend = new Dictionary<char, string>();
        for (int i = legendStart+1; i < legendEnd; i++) {
            char symbol = txtlines[i][0];
            string imagefile = txtlines[i].Substring(3);
            Legend[symbol] = imagefile;
        }
    }
}


