using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        internal byte SDKVersion;
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
        internal byte TextureFormat;
        //Map Format
        internal byte MapFormat;
        //Default Texture Scale in Hammer Editor
        internal float DefTextureScale;
        //Default Lightmap Scale in Hammer Editor
        internal float DefLightMapScale;
        //Path to the game's executable file
        internal string GameExec;
        //Default Brush Entity
        internal string DefBrushEntity;
        //Default Point Entity
        internal string DefPointEntity;

        //VBSP Path
        internal string VBSPPath;
        //VVIS Path
        internal string VVISPath;
        //VRAD Path
        internal string VRADPath;

    }

}
