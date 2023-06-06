using System;
using System.Collections.Generic;
using System.IO;
namespace Breakout.Levels;
/// <summary>
/// A levelReader used in Level to extract Map, Meta and Legend from a txt file.
/// </summary>
public class LevelReader {
    private string path;
    private string[] txtlines;
    private Dictionary<string, string> meta;
    private Dictionary<char, string> legend;
    public string[] Map;

    public Dictionary<char, string> Legend {
        get => legend;
    }
    public Dictionary<string, string> Meta {
        get => meta;
    }
    public LevelReader() {
        this.path = Path.Combine(Constants.MAIN_PATH, "Assets", "Levels");
    }
    /// <summary>
    /// Will try to read a level file. Reads mapdata, leveldata and metadata.
    /// </summary>
    /// <param name="level">the name of the level file that will be read</param>
    /// <returns>false if level file could not be read, else true.</returns>
    public bool ReadLevel(string level) {
        string txtfile = Path.Combine(path, level);
        if (File.Exists(txtfile)) {
            this.txtlines = File.ReadAllLines(txtfile);
            ReadMap();
            ReadMeta();
            ReadLegend();
            return true;
        } else {
            return false;
        }
    }
    /// <summary>
    /// Checks if current level is valid i.e that it contains a map, legendata and metadata.
    /// </summary>
    /// <returns>true if level has map, meta and legendata, else false.</returns>
    public bool MapValid() {
        if (Map != null && Legend != null && Meta != null) {
            return true;
        } else {
            return false;
        }
    }
    private void ReadMap() {
        if (Array.IndexOf(txtlines, "Map:") == -1 ||
            Array.IndexOf(txtlines, "Map/") == -1) {
            // txt file dosent contain a start or end to Map section.
            Map = null;
        } else {
            int MapStart = Array.IndexOf(txtlines, "Map:");
            int MapEnd = Array.IndexOf(txtlines, "Map/");
            Map = new string[MapEnd - 2];
            for (int i = MapStart + 1; i < MapEnd - 1; i++) {
                Map[i - 1] = txtlines[i];
            }
        }
    }
    private void ReadMeta() {
        if (Array.IndexOf(txtlines, "Meta:") == -1 ||
            Array.IndexOf(txtlines, "Meta/") == -1) {
            // txt file dosent contain a start or end to Meta section.
            meta = null;
        } else {
            int MetaStart = Array.IndexOf(txtlines, "Meta:");
            int MetaEnd = Array.IndexOf(txtlines, "Meta/");
            meta = new Dictionary<string, string>();
            for (int i = MetaStart + 1; i < MetaEnd; i++) {
                string[] parts = txtlines[i].Split(": ");
                if (parts.Length == 2) {
                    // meta section contains ": " and can be spilt in 2
                    string key = parts[0];
                    string value = parts[1];
                    if (value.Length == 1) {
                        // value is a block symbol therefore switched around
                        Meta[value] = key;
                    } else {
                        Meta[key] = value;
                    }
                }
            }
        }
    }
    private void ReadLegend() {
        if (Array.IndexOf(txtlines, "Legend:") == -1 ||
            Array.IndexOf(txtlines, "Legend/") == -1) {
            // txt file dosent contain a start or end to Legend section.
            legend = null;
        } else {
            int legendStart = Array.IndexOf(txtlines, "Legend:");
            int legendEnd = Array.IndexOf(txtlines, "Legend/");
            legend = new Dictionary<char, string>();
            for (int i = legendStart + 1; i < legendEnd; i++) {
                char symbol = txtlines[i][0];
                string imagefile = txtlines[i].Substring(3);
                string imagepath = Path.Combine(
                    path.Replace(@"Levels", "Images/"), imagefile);
                if (File.Exists(imagepath)) {
                    Legend[symbol] = imagefile;
                }
            }
        }
    }
}