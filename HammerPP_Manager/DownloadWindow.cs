using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace HammerPP_Manager
{
    public partial class DownloadWindow : Form
    {
        MemoryStream ZipMS;
        Thread DownloadThread;
        public DownloadWindow(bool isUpdate)
        {
            InitializeComponent();

            if (!isUpdate && HelpfulTools.HPPSanityCheck(Properties.Settings.Default.SdkPath))
            {
                DialogResult diagReInstall = MessageBox.Show("Hammer++ is already installed for Source SDK Base 2013!\nUnfortunatley, we need to clean it's installation which means you will lose any configurations and files in the 'hammerplusplus' folder in Source SDK 2013 MP.\nDo you wish to continue?", "H++ Already Installed - Hammer++ Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (diagReInstall == DialogResult.Yes)
                {
                    if (Directory.Exists(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus"))
                        Directory.Delete(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus", true);
                    if (File.Exists(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus.exe"))
                        File.Delete(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus.exe");
                }
                else
                {
                    ConsoleWrite("User Aborted Installation!");
                    Environment.Exit(1);
                }
            }
        }
        internal enum DisclaimerType
        {
            Info,
            Warning,
            Error
        }
        internal void ConsoleWrite(string input, DisclaimerType type = DisclaimerType.Info)
        {
            string disclaimer;
            switch (type)
            {
                case DisclaimerType.Info:
                    disclaimer = "[Info] ";
                    break;
                case DisclaimerType.Warning:
                    disclaimer = "[Warning] ";
                    break;
                case DisclaimerType.Error:
                    disclaimer = "[Error] ";
                    break;
                default:
                    disclaimer = "";
                    break;
            }
            textboxConsole.AppendText(disclaimer + input + "\r\n");
        }

        public class API_Request
        {
            public string url { get; set; }
            public string assets_url { get; set; }
            public string upload_url { get; set; }
            public string html_url { get; set; }
            public string id { get; set; }
            public API_author author { get; set; }
            public string node_id { get; set; }
            public string tag_name { get; set; }
            public string target_commitish { get; set; }
            public string name { get; set; }
            public string draft { get; set; }
            public string prerelease { get; set; }
            public string created_at { get; set; }
            public string published_at { get; set; }
            public IList<API_release> assets { get; set; }
            public string tarball_url { get; set; }
            public string zipball_url { get; set; }
            public string body { get; set; }
        }

        public class API_author
        {
            public string login { get; set; }
            public string id { get; set; }
            public string node_id { get; set; }
            public string avatar_url { get; set; }
            public string gravatar_id { get; set; }
            public string url { get; set; }
            public string html_url { get; set; }
            public string followers_url { get; set; }
            public string following_url { get; set; }
            public string gists_url { get; set; }
            public string starred_url { get; set; }
            public string subscriptions_url { get; set; }
            public string organizations_url { get; set; }
            public string repos_url { get; set; }
            public string events_url { get; set; }
            public string received_events_url { get; set; }
            public string type { get; set; }
            public string site_admin { get; set; }
        }

        public class API_release
        {
            public string url { get; set; }
            public string id { get; set; }
            public string node_id { get; set; }
            public string name { get; set; }
            public string label { get; set; }
            public API_author uploader { get; set; }
            public string content_type { get; set; }
            public string state { get; set; }
            public string size { get; set; }
            public string download_count { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string browser_download_url { get; set; }
        }

        private void DownloadWindow_Load(object sender, EventArgs e)
        {
            this.buttonAbort.Enabled = true;
            //Get ZIP URL of the latest version of Hammer++
            WebClient WebReq = new WebClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebReq.Headers.Add("User-Agent: rmod8-hammerpp_manager");
            string jsonString;
            using (Stream stream = WebReq.OpenRead(@"https://api.github.com/repos/ficool2/HammerPlusPlus-Website/releases/latest"))   //modified from your code since the using statement disposes the stream automatically when done
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }
            ConsoleWrite("API Connection Successful!", DisclaimerType.Info);
            WebReq.Dispose();
            API_Request rqst = JsonConvert.DeserializeObject<API_Request>(jsonString);
            ConsoleWrite("Got current version! Version Tag: " + rqst.tag_name, DisclaimerType.Info);
            string downloadURL = rqst.assets[0].browser_download_url;

            //Used later
            string tagName = rqst.tag_name;
            ConsoleWrite("Downloading latest version of Hammer++...", DisclaimerType.Info);
            
    }
}
