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
            new GameConfig(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt");
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
        GameConfigEntry[] GameConfigs;
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

            if (lines[0].Length != 9)
                throw new EndOfStreamException();
            if (lines[0] != "\"Configs\"")
                throw new InvalidDataException();

            //To-Do: Finish this!
            this.SDKVersion = null;
            this.GameConfigs = null;
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
        //Default Cordon Texture
        internal string CordonTexture;
        //Material Exclude Count
        internal string MaterialExcludeCount;
        //Previous (Don't know what this does)
        internal string Previous;

        internal GameConfigEntry(string GameName, string GameDir, string[] GameData, string TextureFormat,
            string MapFormat, string TextureScale, string LightMapScale, string GameExec, string BrushEntity,
            string PointEntity, string VBSPPath, string VVISPath, string VRADPath, string GameExecDir,
            string MapDir, string BSPDir, string CordonTexture, string MaterialExcludeCount, string Previous)
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
            this.CordonTexture = CordonTexture;
            this.MaterialExcludeCount = MaterialExcludeCount;
            this.Previous = Previous;
        }
    }

}
