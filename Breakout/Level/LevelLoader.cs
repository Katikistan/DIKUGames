using System.IO;
using System.Collections.Generic;

namespace Breakout.Levels;
public class LevelLoader {
    private string txtfile = null!;
    private string path;
    private  string[] txtlines = null!;
    public Dictionary<string,string> ?Meta = null;
    public Dictionary<char,string> ?Legend = null;
    public char[][] ?Map = null;
    public LevelLoader(string path) {
        this.path = path;
    }
    public bool ReadLevel(string level) {
        //tjek om filen eksisterer
        this.txtfile = Path.Combine(path, level);
        if (File.Exists(txtfile)){
            this.txtlines = File.ReadAllLines(txtfile);
            ReadMap();
            ReadMeta();
            ReadLegend();
            return true;
        }
        else {
            return false;
        }
    }
    private void ReadMap() {
        // skal tjekke map filen ikke har en linje der er for lang eller bred
        if (Array.IndexOf(txtlines, "Map:") == -1 || Array.IndexOf(txtlines, "Map/") == -1){
            Map = null;
        } else {
            int MapStart = Array.IndexOf(txtlines, "Map:");
            int MapEnd = Array.IndexOf(txtlines, "Map/");
            Map = new char[MapEnd - 2][];
            for (int i = MapStart + 1; i < MapEnd - 1; i++) {
                Map[i-1] = txtlines[i].ToCharArray();
            }
        }
    }
    private void ReadMeta(){
        //tjekker om der er Meta i filen
        if (Array.IndexOf(txtlines, "Meta:") == -1 || Array.IndexOf(txtlines, "Meta/") == -1){
            Meta = null;
        } else {
            int MetaStart = Array.IndexOf(txtlines, "Meta:");
            int MetaEnd = Array.IndexOf(txtlines, "Meta/");
            Meta = new Dictionary<string, string>();
            for (int i = MetaStart + 1; i < MetaEnd; i++) {
                string[] parts = txtlines[i].Split(": ");
                string key = parts[0];
                string value = parts[1];
                if (value.Length == 1) {
                    Meta[value] = key;
                } else {
                    Meta[key] = value;
                }
            }
        }
    }
    private void ReadLegend() {
        //tjekker om der er Legend i filen
        if (Array.IndexOf(txtlines, "Legend:") == -1 || Array.IndexOf(txtlines, "Legend/") == -1){
            Legend = null;
        } else {
            int legendStart = Array.IndexOf(txtlines, "Legend:");
            int legendEnd = Array.IndexOf(txtlines, "Legend/");
            Legend = new Dictionary<char, string>();
            for (int i = legendStart+1; i < legendEnd; i++){
                char symbol = txtlines[i][0];
                string imagefile = txtlines[i].Substring(3);
                Legend[symbol] = imagefile;
            }
        }
    }
}