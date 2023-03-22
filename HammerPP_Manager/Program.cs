﻿using System;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*
             * We need to check if the user has run this program before.
             * If not, we need to define a few prerequisites.
            */

            if (Properties.Settings.Default.FirstStartup)
            {
                Application.Run(new StartupEnterDetails());
            }
            if (!HelpfulTools.SDKSanityCheck(Properties.Settings.Default.SdkPath))
            {
                DialogResult diagresUserWantReconfigure = MessageBox.Show("Source SDK Base 2013 Multiplayer's Path Seems to be broken. This could be due to the installation being uninstalled or moved to a different drive.\nTo continue using this program, please reselect your Source SDK Base 2013 Installation.", "SDK Path not valid! - Hammer++ Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(diagresUserWantReconfigure == DialogResult.Yes)
                {
                    Application.Run(new StartupEnterDetails());
                }
                else
                {
                    Environment.Exit(1);
                }
            }

            //Checks done, now we can run the program

            Application.Run(new MainWindow());
        }
    }
}