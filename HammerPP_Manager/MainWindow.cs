using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HammerPP_Manager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            listConfigs.Items.Add(new ListViewItem(new string[] { "Test1", "Test2" }));
            GameConfig configs = new GameConfig(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt");
            foreach(var entry in configs.GameConfigs)
            {
                Console.WriteLine(entry.GameName);
                Console.WriteLine(entry.GameDir);
                Console.WriteLine(entry.GameData[0]);
                Console.WriteLine(entry.TextureFormat);
            }
        }

        private void buttonChangeSDKPath_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            new StartupEnterDetails(false).ShowDialog();
            this.Enabled = true;
        }
    }

    internal struct GameConfig
    {
        internal GameConfigEntry[] GameConfigs;
        internal string SDKVersion;

        /// <summary>
        /// Reads GameConfig from file and returns GameConfig Struct
        /// </summary>
        /// <param name="Path"></param>
        internal GameConfig(string Path)
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException();
            string[] lines = File.ReadAllLines(Path);

            //Remove special characters
            for(int i = 0; i < lines.Length; i++)
            {
                string buffer = "";
                for(int x = 0; x < lines[i].Length; x++)
                {
                    char currentchar = lines[i][x];
                    switch (currentchar)
                    {
                        case '\t':
                        case '\r':
                        case '\n':
                            break;

                        default:
                            buffer += currentchar;
                            break;
                    }
                }
                lines[i] = buffer;
            }

            if (lines.Length < 13)
                throw new EndOfStreamException();
            if (lines[0] != "\"Configs\"" || lines[lines.Length - 1] != "}" || lines[2] != "\"Games\"")
                throw new InvalidDataException();

            //Remove first data
            int counter = 4;
            List<GameConfigEntry> Entries = new List<GameConfigEntry>();
            while (true)
            {
                if (lines[counter] == "}")
                {
                    counter++;
                    this.SDKVersion = new KeyValue(lines[counter]).Value;
                    counter++;
                    break;
                }
                else
                {
                    string GameName = lines[counter].Substring(1, lines[counter].Length - 2);
                    counter += 2;
                    string GameDir = new KeyValue(lines[counter]).Value;
                    counter += 3;

                    //Read GameData instances
                    List<string> GameData = new List<string>();
                    while (true)
                    {
                        KeyValue currentKeyvalue = new KeyValue(lines[counter]);
                        if (currentKeyvalue.Key.Length > 8)
                        {
                            if (currentKeyvalue.Key.Substring(0, 8) == "GameData")
                            {
                                GameData.Add(currentKeyvalue.Value);
                            }
                            else
                                break;
                            counter++;
                        }
                        else
                            break;
                    }
                    string TextureFormat = new KeyValue(lines[counter]).Value;
                    string MapFormat = new KeyValue(lines[counter+1]).Value;
                    string TextureScale = new KeyValue(lines[counter + 2]).Value;
                    string LightMapScale = new KeyValue(lines[counter + 3]).Value;
                    string GameExec = new KeyValue(lines[counter + 4]).Value;
                    string BrushEntity = new KeyValue(lines[counter + 5]).Value;
                    string PointEntity = new KeyValue(lines[counter + 6]).Value;

                    string VBSPPath = new KeyValue(lines[counter + 7]).Value;
                    string VVISPath = new KeyValue(lines[counter + 8]).Value;
                    string VRADPath = new KeyValue(lines[counter + 9]).Value;

                    string GameExecDir = new KeyValue(lines[counter + 10]).Value;
                    string MapDir = new KeyValue(lines[counter + 11]).Value;
                    string BSPDir = new KeyValue(lines[counter + 12]).Value;
                    string PrefabDir = null;
                    if(new KeyValue(lines[counter + 13]).Key == "PrefabDir")
                    {
                        PrefabDir = new KeyValue(lines[counter + 13]).Value;
                        counter++;
                    }
                    string CordonTexture = new KeyValue(lines[counter + 13]).Value;
                    string MaterialExcludeCount = new KeyValue(lines[counter + 14]).Value;
                    string Previous = new KeyValue(lines[counter + 15]).Value;
                    counter += 18;
                    //Holy shit this is stupid!
                    Entries.Add(new GameConfigEntry(GameName, GameDir, GameData.ToArray(), TextureFormat, MapFormat, TextureScale, LightMapScale, GameExec, BrushEntity, PointEntity, VBSPPath, VVISPath, VRADPath, GameExecDir, MapDir, BSPDir, PrefabDir, CordonTexture, MaterialExcludeCount, Previous));
                }


            }
            this.GameConfigs = Entries.ToArray();
        }
    }

    internal struct GameConfigEntry
    {
        //Name of the profile
        internal string GameName;
        //Directory where gameinfo.txt is, alongwith game assets.
        internal string GameDir;
        //FGD files
        internal string[] GameData;
        //Texture Format
        internal string TextureFormat;
        //Map Format
        internal string MapFormat;
        //Default Texture Scale in Hammer Editor
        internal string TextureScale;
        //Default Lightmap Scale in Hammer Editor
        internal string LightMapScale;
        //Path to the game's executable file
        internal string GameExec;
        //Default Brush Entity
        internal string BrushEntity;
        //Default Point Entity
        internal string PointEntity;

        //VBSP Path
        internal string VBSPPath;
        //VVIS Path
        internal string VVISPath;
        //VRAD Path
        internal string VRADPath;

        //Directory of game's executable
        internal string GameExecDir;
        //Default directory maps are prompted to save in
        internal string MapDir;
        //Directory compiled BSPs are put into, probably the 'maps' folder in the game directory.
        internal string BSPDir;
        //Directory for prefabs.
        internal string PrefabDir;
        //Default Cordon Texture
        internal string CordonTexture;
        //Material Exclude Count
        internal string MaterialExcludeCount;
        //Previous (Don't know what this does)
        internal string Previous;

        internal GameConfigEntry(string GameName, string GameDir, string[] GameData, string TextureFormat,
            string MapFormat, string TextureScale, string LightMapScale, string GameExec, string BrushEntity,
            string PointEntity, string VBSPPath, string VVISPath, string VRADPath, string GameExecDir,
            string MapDir, string BSPDir, string PrefabDir, string CordonTexture, string MaterialExcludeCount, string Previous)
        {
            this.GameName = GameName;
            this.GameDir = GameDir;
            this.GameData = GameData;
            this.TextureFormat = TextureFormat;
            this.MapFormat = MapFormat;
            this.TextureScale = TextureScale;
            this.LightMapScale = LightMapScale;
            this.GameExec = GameExec;
            this.BrushEntity = BrushEntity;
            this.PointEntity = PointEntity;
            this.VBSPPath = VBSPPath;
            this.VVISPath = VVISPath;
            this.VRADPath = VRADPath;
            this.GameExecDir = GameExecDir;
            this.MapDir = MapDir;
            this.BSPDir = BSPDir;
            this.PrefabDir = PrefabDir;
            this.CordonTexture = CordonTexture;
            this.MaterialExcludeCount = MaterialExcludeCount;
            this.Previous = Previous;
        }
    }

    internal struct KeyValue
    {
        public string Key { get; }
        public string Value { get; }

        public KeyValue(string input)
        {
            input = input.Substring(1, input.Length - 2);
            this.Key = input.Substring(0, input.IndexOf('"'));
            this.Value = input.Substring(this.Key.Length + 2);
        }
    }

}
