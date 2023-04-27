namespace Breakout.Levels;
public class LevelLoader {
    private string txtfile = null!;
    private string path;
    private string[] txtlines = null!;
    public Dictionary<string, string> ?Meta;
    public Dictionary<char, string> ?Legend;
    public char[][]? Map = null;
    /// <summary>
    /// A levelLoader used in Level to extract Map, Meta and Legend from a txt file.
    /// </summary>
    /// <param name="path">The file path level files will be read from</param>
    public LevelLoader(string path) {
        this.path = path;
    }
    /// <summary>
    /// Will try to read a level file
    /// </summary>
    /// <param name="level"></param>
    /// <returns>Returns false if level file could not be read else true.</returns>
    public bool ReadLevel(string level) {
        this.txtfile = Path.Combine(path, level);
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

    private void ReadMap() {
        if (Array.IndexOf(txtlines, "Map:") == -1 ||
            Array.IndexOf(txtlines, "Map/") == -1) {
            // txt file dosent contain a start or end to Map section.
            Map = null;
        } else {
            int MapStart = Array.IndexOf(txtlines, "Map:");
            int MapEnd = Array.IndexOf(txtlines, "Map/");
            Map = new char[MapEnd - 2][];
            for (int i = MapStart + 1; i < MapEnd - 1; i++) {
                Map[i - 1] = txtlines[i].ToCharArray();
            }
        }
    }
    private void ReadMeta() {
        if (Array.IndexOf(txtlines, "Meta:") == -1 ||
            Array.IndexOf(txtlines, "Meta/") == -1) {
            // txt file dosent contain a start or end to Meta section.
            Meta = null;
        } else {
            int MetaStart = Array.IndexOf(txtlines, "Meta:");
            int MetaEnd = Array.IndexOf(txtlines, "Meta/");
            Meta = new Dictionary<string, string>();
            for (int i = MetaStart + 1; i < MetaEnd; i++) {
                string[] parts = txtlines[i].Split(": ");
                if (parts.Length == 2) {
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
    }
    /// <summary>
    /// Checks that a loaded level has a Map and Legend data.
    /// </summary>
    /// <returns>Returns true if level file has Map and LegendData else false.</returns>
    public bool MapValid() {
        if (Map != null && Legend != null) {
            return true;
        } else {
            return false;
        }
    }
    private void ReadLegend() {
        if (Array.IndexOf(txtlines, "Legend:") == -1 ||
            Array.IndexOf(txtlines, "Legend/") == -1) {
            // txt file dosent contain a start or end to Legend section.
            Legend = null;
        } else {
            int legendStart = Array.IndexOf(txtlines, "Legend:");
            int legendEnd = Array.IndexOf(txtlines, "Legend/");
            Legend = new Dictionary<char, string>();
            for (int i = legendStart + 1; i < legendEnd; i++) {
                char symbol = txtlines[i][0];
                string imagefile = txtlines[i].Substring(3);
                string imagepath = Path.Combine(path.Replace(@"Levels", "Images\\"), imagefile);
                if (File.Exists(imagepath)) {
                    Legend[symbol] = imagefile;
                }
            }
        }
    }
}