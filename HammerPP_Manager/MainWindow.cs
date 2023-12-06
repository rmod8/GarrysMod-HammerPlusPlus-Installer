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

    internal class GameConfig
    {


        /// <summary>
        /// Get's changed to a more friendly name read from gameinfo.txt
        /// </summary>
        string displayName;
        string internalName;
        List<GameConfigEntry> Entries;
    }

    internal class GameConfigEntry
    {
        bool isChecked;
        string mountPath;
    }
}
