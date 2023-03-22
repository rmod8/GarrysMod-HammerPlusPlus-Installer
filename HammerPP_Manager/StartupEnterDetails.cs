/*
 * Purpose:
 * Allow user to set Source SDK Base 2013 Path or run a scan to potentially find it.
 * A check is performed to see if the selected path is valid.
 * No files are accessed or metadata about any files are saved.
 * Only file precense is detected.
*/

using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    public partial class StartupEnterDetails : Form
    {
        //Setting up sound objects.
        SoundPlayer sfxGranted = new SoundPlayer(Properties.Resources.granted);
        SoundPlayer sfxDenied = new SoundPlayer(Properties.Resources.denied);
        string defaultSDKPath = @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2013 Multiplayer";

        public StartupEnterDetails()
        {
            InitializeComponent();
            this.ActiveControl = null;

            //Perform scan to see if we can find the directory ourselves

            if(Directory.Exists(defaultSDKPath))
            {
                //This is ubsurdly long
                if(MessageBox.Show("Hammer++ Manager Detected a Source SDK Base 2013 Multilayer Installation on your C: Drive.\nWould you like to use this path?", "Auto-Select Installation on C: Drive - Hammer++ Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //ChangePaths trims the bottom part because of how OpenFileDialog works. Maybe in
                    //future we trim in the browse button and remove the trimming from ChangePaths.
                    ChangePaths(defaultSDKPath+"\\[Folder Selection]");
                }
            }
            else
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                if (allDrives.Length > 1)
                {
                    for (int i = 0; i < allDrives.Length; i++)
                    {
                        if(allDrives[i].Name != "C:\\")
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
            }

            
            
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SdkPath = tboxSDKPath.Text;
            Properties.Settings.Default.FirstStartup = false;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void buttonSDKBrowse_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.Multiselect = false;
            ofd.CheckFileExists = false;
            ofd.FileName = "[Folder Selection]";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ChangePaths(ofd.FileName);
            }
            ofd.Dispose();
        }


        /*
         * This is used so we can execute this code outside the button
         * without prompting a OpenFileDialog.
        */
        private void ChangePaths(string inputPath)
        {
            //Check the path to see if it's legit
            string SDKPath = Directory.GetParent(inputPath).ToString();

            if (Path.GetFileNameWithoutExtension(SDKPath) == "Source SDK Base 2013 Multiplayer" && HelpfulTools.SDKSanityCheck(SDKPath))
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

            tboxSDKPath.Text = Directory.GetParent(inputPath).ToString();
        }


        //Prevents selection of textbox, this is purely cosmetic.
        private void tboxSDKPath_Focus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }


        
    }
}
