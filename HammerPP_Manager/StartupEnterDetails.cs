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
        //Botch variable
        bool IsContinuing = false;
        bool IsStandalone;
        //Setting up sound objects.
        SoundPlayer sfxGranted = new SoundPlayer(Properties.Resources.granted);
        SoundPlayer sfxDenied = new SoundPlayer(Properties.Resources.denied);
        string defaultSDKPath = @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2013 Multiplayer";

        public StartupEnterDetails(bool IsStandaloneIn)
        {
            this.IsStandalone = IsStandaloneIn;
            InitializeComponent();
            this.ActiveControl = null;

            //Perform scan to see if we can find the directory ourselves

            if(Directory.Exists(defaultSDKPath))
            {
                //This is ubsurdly long
                if(MessageBox.Show("Hammer++ Manager Detected a Source SDK Base 2013 Multiplayer Installation on your C: Drive.\nWould you like to use this path?", "Auto-Select Installation on C: Drive - Hammer++ Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //ChangePaths trims the bottom part because of how OpenFileDialog works. Maybe in
                    //future we trim in the browse button and remove the trimming from ChangePaths.
                    ChangePaths(defaultSDKPath+"\\[Folder Selection]");
                }
            }
            else
            {
                //Scans other drives for sdk installation
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                //If theres more than 1 drive...
                if (allDrives.Length > 1)
                {
                    //For each drive mounted...
                    for (int i = 0; i < allDrives.Length; i++)
                    {
                        //If current drive is not c:...
                        if(allDrives[i].Name != "C:\\")
                        {
                            string otherSDKPath = allDrives[i].Name + "SteamLibrary\\steamapps\\common\\Source SDK Base 2013 Multiplayer";
                            //If drive has path...
                            if (Directory.Exists(otherSDKPath))
                            {
                                //If path is valid...
                                if (HelpfulTools.SDKSanityCheck(otherSDKPath))
                                {
                                    //If user is happy with using the discovered and legit path...
                                    if (MessageBox.Show("Hammer++ Manager Detected a Source SDK Base 2013 Multilayer Installation on your " + allDrives[i].Name.Substring(0, 1) +": Drive.\nWould you like to use this path?", "Auto-Select Installation on "+ allDrives[i].Name.Substring(0, 1)+": Drive - Hammer++ Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        //ChangePaths trims the bottom part because of how OpenFileDialog works. Maybe in
                                        //future we trim in the browse button and remove the trimming from ChangePaths.
                                        ChangePaths(otherSDKPath + "\\[Folder Selection]");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            
            
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            this.IsContinuing = true;
            Properties.Settings.Default.SdkPath = tboxSDKPath.Text;
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

        /// <summary>
        /// Patchtape solution:
        /// Prevents program from exiting if window is used in main program,
        /// but allow program to exit if the window is all alone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickExit(object sender, EventArgs e)
        {
            if(this.IsStandalone && !this.IsContinuing)
                Environment.Exit(1);
        }
    }
}
