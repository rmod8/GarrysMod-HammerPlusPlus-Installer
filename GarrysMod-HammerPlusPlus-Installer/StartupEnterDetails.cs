/*
 * Purpose:
 * Allow user to set Source SDK Base 2013 Path or run a scan to potentially find it.
 * A check is performed to see if the selected path is valid.
 * No files are accessed or metadata about any files are saved.
*/

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
using System.Media;

namespace HammerPP_Manager
{
    public partial class StartupEnterDetails : Form
    {
        //Setting up sound objects.
        SoundPlayer sfxGranted = new SoundPlayer(Properties.Resources.granted);
        SoundPlayer sfxDenied = new SoundPlayer(Properties.Resources.denied);

        public StartupEnterDetails()
        {
            InitializeComponent();
            this.ActiveControl = null;
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            //Finish this
            this.Close();
        }

        private void buttonSDKBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.Multiselect = false;
            ofd.CheckFileExists = false;
            ofd.FileName = "[Folder Selection]";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                //Check the path to see if it's legit
                string SDKPath = Directory.GetParent(ofd.FileName).ToString();

                if(Path.GetFileNameWithoutExtension(SDKPath) == "Source SDK Base 2013 Multiplayer" && Directory.Exists(SDKPath+"\\bin"))
                {
                    //Check passed; Path is valid
                    pboxOkay.Image.Dispose();
                    pboxOkay.Image = Properties.Resources.tick;
                    buttonContinue.Enabled = true;
                    sfxGranted.Play();
                }
                else
                {
                    //Check failed; Path is invalid
                    pboxOkay.Image.Dispose();
                    pboxOkay.Image = Properties.Resources.cross;
                    buttonContinue.Enabled = false;
                    sfxDenied.Play();
                }

                tboxSDKPath.Text = Directory.GetParent(ofd.FileName).ToString();
                
            }
            ofd.Dispose();
        }


        //Prevents selection of textbox, this is purely cosmetic.
        private void tboxSDKPath_Focus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }


        //Used to check sdk directory for most files and directories to confirm it's authentic
        private bool SDKSanityCheck(string basePath)
        {
            string[] DirCheckList = { 
            "\\bin",
            ""
            };

            return false;
        }
    }
}
