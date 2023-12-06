using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            //Application.Run(new Form1());
        }
    }
}
