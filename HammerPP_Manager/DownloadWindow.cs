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
using System.IO.Compression;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HammerPP_Manager
{
    public partial class DownloadWindow : Form
    {
        MemoryStream ZipMS;
        Thread DownloadThread;
        bool publicIsUpdate;
        public DownloadWindow(bool isUpdate)
        {
            this.publicIsUpdate = isUpdate;
            InitializeComponent();

            if (!isUpdate && HelpfulTools.HPPSanityCheck(Properties.Settings.Default.SdkPath))
            {
                DialogResult diagReInstall = MessageBox.Show("Hammer++ is already installed for Source SDK Base 2013!\nUnfortunatley, we need to clean it's installation which means you will lose any configurations and files in the 'hammerplusplus' folder in Source SDK 2013 MP.\nDo you wish to continue?", "H++ Already Installed - Hammer++ Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (diagReInstall == DialogResult.Yes)
                {
                    try
                    {
                        if (Directory.Exists(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus"))
                            Directory.Delete(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus", true);
                        if (File.Exists(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus.exe"))
                            File.Delete(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus.exe");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Close Hammer++ and try again!", "", MessageBoxButtons.OK);
                        if (isUpdate)
                            this.Close();
                        else
                            Environment.Exit(1);
                    }
                    
                }
                else
                {
                    ConsoleWrite("User Aborted Installation!");
                    Thread.Sleep(1000);
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
            try
            {
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
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;

                webClient.DownloadDataAsync(new Uri(downloadURL));
            }
            catch (WebException)
            {
                if (!publicIsUpdate)
                {
                    MessageBox.Show("Failed to connect to GitHub API.\nCheck your internet connection or check if GitHub is down.\nPress \'OK\' to close this application.");
                    ConsoleWrite("No Connection!");
                    Thread.Sleep(1000);
                    Environment.Exit(1);
                }
                else
                {
                    MessageBox.Show("Failed to connect to GitHub API.\nCheck your internet connection or check if GitHub is down.\nPress \'OK\' to return to the main menu.");
                    this.Close();
                }
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Shows fancy facts & logic for the progress bar.
            //Thanks ChatGPT for writing this code.
            pbDownload.Value = e.ProgressPercentage;
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double speed = bytesIn / e.ProgressPercentage;
            string speedText = string.Format("{0} KB/s", (speed / 1024).ToString("0.00"));
            string downloadedText = string.Format("{0} MB / {1} MB", (bytesIn / (1024 * 1024)).ToString("0.00"), (totalBytes / (1024 * 1024)).ToString("0.00"));

            labelProgress.Text = $"Downloading... {downloadedText} ({speedText})";


        }

        private void WebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("An error occurred during download: " + e.Error.Message+"\nCheck your internet connection!", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!publicIsUpdate)
                    Environment.Exit(1);
                else
                    this.Close();
            }
            else
            {
                ConsoleWrite("Download successful, deserializing ZIP...");
                byte[] downloadedData = e.Result;

                //Extract ZIP file
                using (MemoryStream stream = new MemoryStream(downloadedData))
                {
                    using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                    {
                        ConsoleWrite("Extracting ZIP files to Source SDK Base 2013 Multiplayer directory...");
                        long totalUncompressedSize = 0;

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            totalUncompressedSize += entry.Length;
                        }

                        DriveInfo destinationDrive = new DriveInfo(Path.GetPathRoot(Properties.Settings.Default.SdkPath));
                        long freeSpace = destinationDrive.AvailableFreeSpace;

                        if (freeSpace < totalUncompressedSize)
                        {
                            MessageBox.Show("Error: Not enough free space on the destination drive to extract the contents.");
                            if (publicIsUpdate)
                                this.Close();
                            else
                                Environment.Exit(1);
                        }

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            //Console.WriteLine("File: " + entry.FullName.Substring(entry.FullName.IndexOf('/') + 1));
                            //Console.WriteLine("Size: " + entry.Length + " bytes");
                            //Extract file

                            string extractedFilePath = Path.Combine(Properties.Settings.Default.SdkPath, entry.FullName.Substring(entry.FullName.IndexOf('/') + 1));
                            try
                            {
                                string directoryPath = Path.GetDirectoryName(extractedFilePath);
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                entry.ExtractToFile(extractedFilePath, true);
                                Console.WriteLine("Extracted to: " + extractedFilePath);
                            }
                            catch (IOException ex)
                            {
                                //ConsoleWrite("Error extracting file: " + ex.Message, DisclaimerType.Error);
                            }
                            //Console.WriteLine("-----------------------");
                        }
                        ConsoleWrite("Extracted " + archive.Entries.Count + " files. A total of ~" + ((totalUncompressedSize/1024)/1024).ToString("0.00") + "MBs of data.");
                    }
                }

                MessageBox.Show("To properly setup the program, we need to launch the program atleast once.\nWhen you press 'OK', Hammer++ will open. Select any game configuration, then close Hammer++ when the Hammer++ logo disappears.", "Important!", MessageBoxButtons.OK);
                Process.Start(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus.exe");
                //Launch the program to generate configurations
                while (!File.Exists(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus\\hammerplusplus_sequences.cfg"))
                {
                    MessageBox.Show("Please select a game configuration in Hammer++\nWhen done, click 'OK' button here.", "", MessageBoxButtons.OK);
                    if (!HelpfulTools.IsHPPOpen() && !File.Exists(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus\\hammerplusplus_sequences.cfg"))
                    {
                        Process.Start(Properties.Settings.Default.SdkPath + "\\bin\\hammerplusplus.exe");
                    }
                }
                ConsoleWrite("Hammer++ Config Detected! We are ready to go!");
                this.Close();
            }
        }

       
    }
}
