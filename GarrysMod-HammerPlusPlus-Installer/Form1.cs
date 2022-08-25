using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using System.Net;

namespace GarrysMod_HammerPlusPlus_Installer
{
    public partial class Form1 : Form
    {
        string GmodPath = "";
        string SDKPath = "";
        public Form1()
        {
            InitializeComponent();
            
        }

        internal enum DisclaimerType
        {
            Info,
            Warning,
            Error
        }
        internal void ConsoleWrite(string input, DisclaimerType type)
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
            textboxConsole.AppendText(disclaimer+input + "\r\n");
        }

        
        internal void CheckDirectoryValidity()
        {
            bool GmodValid = true;
            bool SDKValid = true;
            picboxGmodCheck.Image = Properties.Resources.tick;
            picboxSDKCheck.Image = Properties.Resources.tick;
            if (!Directory.Exists(@GmodPath + @"\bin") || Path.GetFileNameWithoutExtension(@GmodPath) != "GarrysMod")
            {
                if (@GmodPath != "")
                    ConsoleWrite("Garry's Mod's path is invalid! Selected folder should be \"GarrysMod\", not \"" + Path.GetFileNameWithoutExtension(@GmodPath)+"\"", DisclaimerType.Warning);
                GmodValid = false;
                picboxGmodCheck.Image = Properties.Resources.cross;
            }
            if (!Directory.Exists(@SDKPath + @"\bin") || Path.GetFileNameWithoutExtension(@SDKPath) != "Source SDK Base 2013 Multiplayer")
            {
                if(@SDKPath != "")
                    ConsoleWrite("SDK 2013 Multiplayer path is invalid! Selected folder should be \"Source SDK Base 2013 Multiplayer\", not \"" + Path.GetFileNameWithoutExtension(@SDKPath) + "\"", DisclaimerType.Warning);
                SDKValid = false;
                picboxSDKCheck.Image = Properties.Resources.cross;
            }
            if(GmodValid && SDKValid)
            {
                if(File.Exists(@SDKPath + @"\bin\hammerplusplus.exe") || Directory.Exists(@SDKPath + @"\bin\hammerplusplus"))
                {
                    if(!File.Exists(@SDKPath + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt")){
                        DialogResult dr = MessageBox.Show("The selected directory contains an invalid HammerPlusPlus installation!\nDo you want to delete it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if(dr == DialogResult.Yes)
                        {
                            ConsoleWrite("Invalid HammerPlusPlus installation detected! Deleting...", DisclaimerType.Warning);
                            if (File.Exists(@SDKPath + @"\bin\hammerplusplus.exe"))
                                File.Delete(@SDKPath + @"\bin\hammerplusplus.exe");
                            if (Directory.Exists(@SDKPath + @"\bin\hammerplusplus"))
                                Directory.Delete(@SDKPath + @"\bin\hammerplusplus", true);
                            ConsoleWrite("Both game paths are valid! You can now install Hammer++", DisclaimerType.Info);
                            buttonInstall.Enabled = true;
                        }
                        else
                        {
                            ConsoleWrite("Invalid HammerPlusPlus installation was detected but the user chose not to delete it.", DisclaimerType.Warning);
                            ConsoleWrite("This program can not continue until the invalid HammerPlusPlus installation is deleted.", DisclaimerType.Info);
                        }
                        
                    }
                    else
                        ConsoleWrite("HammerPlusPlus for Garry's Mod is already installed!", DisclaimerType.Error);
                }
                else
                {
                    ConsoleWrite("Both game paths are valid! You can now install Hammer++", DisclaimerType.Info);
                    buttonInstall.Enabled = true;
                }
                    
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"http://www.famfamfam.com/lab/icons/silk/");
        }

        private void buttonBrowseGmod_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files(*.*)| *.* ";
            ofd.Multiselect = false;
            ofd.FileName = "[FOLDER SELECTION]";
            ofd.CheckFileExists = false;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                GmodPath = Path.GetDirectoryName(ofd.FileName);
                textboxGmodDir.Text = Path.GetDirectoryName(ofd.FileName);
                CheckDirectoryValidity();
            }
            ofd.Dispose();
        }

        private void buttonBrowseSDK_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files(*.*)| *.* ";
            ofd.Multiselect = false;
            ofd.FileName = "[FOLDER SELECTION]";
            ofd.CheckFileExists = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SDKPath = Path.GetDirectoryName(ofd.FileName);
                textboxSDKDir.Text = Path.GetDirectoryName(ofd.FileName);
                CheckDirectoryValidity();
            }
            ofd.Dispose();
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            string DownloadLink;
            this.Enabled = false;
            buttonInstall.Enabled = false;
            buttonBrowseGmod.Enabled = false;
            buttonBrowseSDK.Enabled = false;
            ConsoleWrite("Beginning installation...", DisclaimerType.Info);

            //First we need to download the Hammer++ zip, we need to contact the GitHub API to get the link...
            try
            {
                WebClient WebReq = new WebClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebReq.Headers.Add("User-Agent: retr0mod8-gmod_hammerpp_installer");
                string jsonString;
                using (Stream stream = WebReq.OpenRead(@"https://api.github.com/repos/ficool2/HammerPlusPlus-Website/releases/latest"))   //modified from your code since the using statement disposes the stream automatically when done
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }
                ConsoleWrite("API Connection Successful!", DisclaimerType.Info);
                WebReq.Dispose();
                API_Request rqst = JsonConvert.DeserializeObject<API_Request>(jsonString);
                ConsoleWrite("Got current version! Version Tag: "+rqst.tag_name, DisclaimerType.Info);
                DownloadLink = rqst.assets[0].browser_download_url;

                //We got the url, time to download it and sort it out.
                //This is to make sure any residual files are not here.
                try
                {
                    if (Directory.Exists(@SDKPath + @"\HammerPPTemporary"))
                        Directory.Delete(@SDKPath + @"\HammerPPTemporary", true);

                    if (File.Exists(@SDKPath + @"\HammerPlusPlus.zip"))
                        File.Delete(@SDKPath + @"\HammerPlusPlus.zip");

                    ConsoleWrite("Making Temporary Folder for Hammer++ Zip Contents", DisclaimerType.Info);
                    Directory.CreateDirectory(@SDKPath + @"\HammerPPTemporary");

                    WebClient FileDownloader = new WebClient();
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    FileDownloader.Headers.Add("User-Agent: retr0mod8-gmod_hammerpp_installer");
                    FileDownloader.DownloadFile(DownloadLink, @SDKPath + @"\HammerPlusPlus.zip");
                    ConsoleWrite("Downloading latest version of Hammer++...", DisclaimerType.Info);
                    while (FileDownloader.IsBusy)
                    {
                        //We gotta wait for the file to download...
                    }
                    ConsoleWrite("Downloaded latest version of Hammer++!", DisclaimerType.Info);
                    try
                    {
                        ConsoleWrite("Extracting download...", DisclaimerType.Info);
                        ZipFile.ExtractToDirectory(@SDKPath + @"\HammerPlusPlus.zip", @SDKPath + @"\HammerPPTemporary");
                        if (File.Exists(@SDKPath + @"\HammerPlusPlus.zip"))
                            File.Delete(@SDKPath + @"\HammerPlusPlus.zip");
                        ConsoleWrite("Download Extracted!", DisclaimerType.Info);
                        File.Move(@SDKPath + @"\HammerPPTemporary\hammerplusplus_2013mp_build" + rqst.tag_name + @"\bin\hammerplusplus.exe", @SDKPath + @"\bin\hammerplusplus.exe");
                        Directory.Move(@SDKPath + @"\HammerPPTemporary\hammerplusplus_2013mp_build" + rqst.tag_name + @"\bin\hammerplusplus", @SDKPath + @"\bin\hammerplusplus");
                        if (Directory.Exists(@SDKPath + @"\HammerPPTemporary"))
                            Directory.Delete(@SDKPath + @"\HammerPPTemporary", true);
                        ConsoleWrite("Hammer++ files moved successfully but we're not finished! A folder should've opened. Find 'hammerplusplus.exe' in there and launch it, select a game config, click 'OK', then close Hammer++ once loaded! After that, click 'Configure Hammer++ for Garry's Mod'.", DisclaimerType.Warning);
                        Process.Start(@SDKPath + @"\bin");
                        MessageBox.Show("Hammer++ files moved successfully but we're not finished!\nA folder should've opened. Find 'hammerplusplus.exe' in there and launch it, select a game config, click 'OK', then close Hammer++ once loaded!\nAfter that, click 'Configure Hammer++ for Garry's Mod'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    catch (Exception ex)
                    {
                        ConsoleWrite("Hammer++ Installation Failed! rmod most likely messed up, sorry :(", DisclaimerType.Error);
                        ConsoleWrite("Exception details below, send these to retromod8#9627 on Discord\r\n" + ex, DisclaimerType.Error);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleWrite("Hammer++ Download Failed! Check your internet connection!", DisclaimerType.Error);
                    ConsoleWrite("Exception details below, send these to retromod8#9627 on Discord\r\n" + ex, DisclaimerType.Error);
                }

            }
            catch (Exception ex)
            {
                ConsoleWrite("API Connection Failed! Check your internet connection!", DisclaimerType.Error);
                ConsoleWrite("Exception details below, send these to retromod8#9627 on Discord\r\n"+ex, DisclaimerType.Error);
                
            }
            if (Directory.Exists(@SDKPath + @"\HammerPPTemporary"))
                Directory.Delete(@SDKPath + @"\HammerPPTemporary", true);
            if (File.Exists(@SDKPath + @"\HammerPlusPlus.zip"))
                File.Delete(@SDKPath + @"\HammerPlusPlus.zip");
            this.Enabled = true;
            buttonConfig.Enabled = true;
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            buttonConfig.Enabled = false;
            try { 
            if (!File.Exists(@SDKPath + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt"))
            {
                ConsoleWrite("The installation cannot continue until you launch Hammer++ once.", DisclaimerType.Error);
                    this.Enabled = true;
                    buttonConfig.Enabled =true;
                return;
            }
            string WordBuffer = "";
            ConsoleWrite("Gameconfig detected! We can now configure Hammer++", DisclaimerType.Info);
            ConsoleWrite("Deserializing GameConfig", DisclaimerType.Info);
            FileStream GameConfigFS = File.OpenRead(@SDKPath + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt");
            MemoryStream GameConfigMS = new MemoryStream();
            GameConfigFS.CopyTo(GameConfigMS);
            ConsoleWrite("GameConfig copied to memory", DisclaimerType.Info);
            GameConfigFS.Dispose();
            GameConfigMS.Position = 0;

            ConsoleWrite("Parsing GameConfig", DisclaimerType.Info);
            
            char current;
            List<string> GameConfigOriginal = new List<string>();
            for (int i = 0; i < GameConfigMS.Length; i++)
            {
                current = Convert.ToChar(Convert.ToByte(GameConfigMS.ReadByte()));
                switch (current)
                {
                    case '\n':
                        GameConfigOriginal.Add(WordBuffer);
                        WordBuffer = "";
                        break;

                    default:
                        WordBuffer += current;
                        break;
                }
            }
            GameConfigMS.Close();
            GameConfigMS.Dispose();
            ConsoleWrite("Parsed GameConfig", DisclaimerType.Info);

            List<string> GameConfigModified = new List<string>();
            for (int i = 0; i < GameConfigOriginal.Count; i++)
            {
                if (GameConfigOriginal[i] != "\t}\r")
                    GameConfigModified.Add(GameConfigOriginal[i]);
                else
                    break;

            }
            ConsoleWrite("Adding Garry's Mod Game Configuration to GameConfig", DisclaimerType.Info);
            GameConfigModified.Add("\t\t\"Garry's Mod\"\r");
            GameConfigModified.Add("\t\t{\r");
            GameConfigModified.Add("\t\t\t" + "\"GameDir\"\t\t\"" + @SDKPath + @"\hl2" + "\"\r");
            GameConfigModified.Add("\t\t\t" + "\"Hammer\"" + "\r");
            GameConfigModified.Add("\t\t\t" + "{" + "\r");
            if(MessageBox.Show("(Optional) Would you like to add all the FGDs in Garry's Mod's Directory?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConsoleWrite("Adding all FGDs in Garry's Mod's 'bin' folder", DisclaimerType.Info);
                //Add all FGDs
                string[] FGDArray = Directory.GetFiles(@GmodPath + @"\bin", "*.fgd");
                for (int i = 0; i < FGDArray.Length; i++)
                {
                    //and add em....
                    addKeyvalueToConfig(GameConfigModified, "GameData" + i, @FGDArray[i]);
                }
                ConsoleWrite("Total of " + FGDArray.Length + " FGDs added!", DisclaimerType.Info);
            }
            else
            {
                ConsoleWrite("Adding only essential FGDs", DisclaimerType.Info);
                //Add only essential
                int FGDCount = 0;
                if(File.Exists(@GmodPath + @"\bin\base.fgd"))
                {
                    addKeyvalueToConfig(GameConfigModified, "GameData" + FGDCount, @GmodPath + @"\bin\base.fgd");
                    FGDCount++;
                }
                if (File.Exists(@GmodPath + @"\bin\garrysmod.fgd"))
                {
                    addKeyvalueToConfig(GameConfigModified, "GameData" + FGDCount, @GmodPath + @"\bin\garrysmod.fgd");
                    FGDCount++;
                }
                ConsoleWrite("Total of " + FGDCount + " FGDs added!", DisclaimerType.Info);

            }
            addKeyvalueToConfig(GameConfigModified, "TextureFormat", "5");
            addKeyvalueToConfig(GameConfigModified, "MapFormat", "4");
            addKeyvalueToConfig(GameConfigModified, "DefaultTextureScale", "0.250000");
            addKeyvalueToConfig(GameConfigModified, "DefaultLightmapScale", "16");
            addKeyvalueToConfig(GameConfigModified, "GameExe", @GmodPath + @"\hl2.exe");
            addKeyvalueToConfig(GameConfigModified, "DefaultSolidEntity", "func_detail");
            addKeyvalueToConfig(GameConfigModified, "DefaultPointEntity", "info_player_start");
            addKeyvalueToConfig(GameConfigModified, "BSP", @SDKPath + @"\bin\vbsp.exe");
            addKeyvalueToConfig(GameConfigModified, "Vis", @SDKPath + @"\bin\vvis.exe");
            addKeyvalueToConfig(GameConfigModified, "Light", @SDKPath + @"\bin\vrad.exe");
            addKeyvalueToConfig(GameConfigModified, "GameExeDir", @GmodPath);
            addKeyvalueToConfig(GameConfigModified, "MapDir", @GmodPath + @"\garrysmod\mapsrc");
            addKeyvalueToConfig(GameConfigModified, "BSPDir", @GmodPath + @"\garrysmod\maps");
            addKeyvalueToConfig(GameConfigModified, "PrefabDir", @GmodPath + @"\bin\Prefabs");
            addKeyvalueToConfig(GameConfigModified, "CordonTexture", "dev/dev_blendmeasure");
            addKeyvalueToConfig(GameConfigModified, "MaterialExcludeCount", "0");
            addKeyvalueToConfig(GameConfigModified, "Previous", "0");
            GameConfigModified.Add("\t\t\t" + "}" + "\r");
            GameConfigModified.Add("\t\t" + "}" + "\r");
            GameConfigModified.Add("\t" + "}" + "\r");
            GameConfigModified.Add("\t" + "\"SDKVersion\"\t\t\"5\"" + "\r");
            GameConfigModified.Add("}" + "\r");

            ConsoleWrite("Added Garry's Mod Game Configuration to GameConfig", DisclaimerType.Info);

            File.Delete(@SDKPath + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt");
            FileStream GameConfigOut = File.Create(@SDKPath + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt");
            GameConfigOut.Position = 0;


            for (int i = 0; i < GameConfigModified.Count; i++)
            {
                GameConfigModified[i] = GameConfigModified[i] += "\n";
                for (int x = 0; x < GameConfigModified[i].Length; x++)
                {
                    GameConfigOut.WriteByte(Convert.ToByte(GameConfigModified[i][x]));
                }
            }
            GameConfigOut.Close();
            GameConfigOut.Dispose();
            GameConfigOriginal.Clear();
            GameConfigModified.Clear();
            ConsoleWrite("GameConfig Configured Successfully!", DisclaimerType.Info);
            WordBuffer = "";
            ConsoleWrite("Deserializing GameInfo", DisclaimerType.Info);
            FileStream GameInfoFS = File.OpenRead(@SDKPath + @"\hl2\gameinfo.txt");
            MemoryStream GameInfoMS = new MemoryStream();
            GameInfoFS.CopyTo(GameInfoMS);
            ConsoleWrite("GameInfo copied to memory", DisclaimerType.Info);
            GameInfoFS.Dispose();
            GameInfoMS.Position = 0;
            ConsoleWrite("Parsing GameInfo", DisclaimerType.Info);

            List<string> GameInfoOriginal = new List<string>();
            List<string> GameInfoModified = new List<string>();

            for (int i = 0; i < GameInfoMS.Length; i++)
            {
                current = Convert.ToChar(Convert.ToByte(GameInfoMS.ReadByte()));
                switch (current)
                {
                    case '\n':
                        GameInfoOriginal.Add(WordBuffer);
                        WordBuffer = "";
                        break;

                    default:
                        WordBuffer += current;
                        break;
                }
            }
            GameInfoMS.Close();
            GameInfoMS.Dispose();

            ConsoleWrite("Parsed GameInfo", DisclaimerType.Info);

            for (int i = 0; i < GameInfoOriginal.Count; i++)
            {
                if (GameInfoOriginal[i] != "\t\t\tplatform\t\t\t|all_source_engine_paths|platform\r")
                    GameInfoModified.Add(GameInfoOriginal[i]);
                else
                {
                    GameInfoModified.Add(GameInfoOriginal[i]);
                    break;
                }

            }
            ConsoleWrite("Adding GarrysMod Assets to GameInfo", DisclaimerType.Info);
            GameInfoModified.Add("\t\t\tgame\t\t\t\t\"" + @GmodPath + @"\garrysmod" + "\"\r");
            GameInfoModified.Add("\t\t\tgame\t\t\t\t\"" + @GmodPath + @"\garrysmod\garrysmod_dir.vpk" + "\"\r");
            GameInfoModified.Add("\t\t}\r");
            GameInfoModified.Add("\t}\r");
            GameInfoModified.Add("}\r");
            ConsoleWrite("Added GarrysMod Assets to GameInfo", DisclaimerType.Info);
            File.Delete(@SDKPath + @"\hl2\gameinfo.txt");
            FileStream GameInfoOut = File.Create(@SDKPath + @"\hl2\gameinfo.txt");
            GameInfoOut.Position = 0;
            for (int i = 0; i < GameInfoModified.Count; i++)
            {
                GameInfoModified[i] = GameInfoModified[i] += "\n";
                for (int x = 0; x < GameInfoModified[i].Length; x++)
                {
                    GameInfoOut.WriteByte(Convert.ToByte(GameInfoModified[i][x]));
                }
            }
            GameInfoOut.Close();
            GameInfoOut.Dispose();
            ConsoleWrite("GameInfo Configured Successfully!", DisclaimerType.Info);

            ConsoleWrite("Sequences now being modified", DisclaimerType.Info);
            List<string> SequencesModified = new List<string>();
            SequencesModified.Add("\"Command Sequences\"\n{\n");

            //GMOD FAST

            SequencesModified.Add("\t\"GarrysMod (Fast)\"\n");
            SequencesModified.Add("\t{\n");

            //VBSP
            SequencesModified.Add("\t\t\"0\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VVIS
            SequencesModified.Add("\t\t\"1\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-fast -game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VRAD
            SequencesModified.Add("\t\t\"2\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-ldr -bounce 2 -noextra -game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //COPY
            SequencesModified.Add("\t\t\"3\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            SequencesModified.Add("\t\t}\n");

            //GAME
            SequencesModified.Add("\t\t\"4\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game " + getEscape() + @GmodPath + @"\garrysmod" + getEscape() + " +map $file\"\n");
            SequencesModified.Add("\t\t}\n");

            SequencesModified.Add("\t}\n");


            //GMOD LDR FINAL

            SequencesModified.Add("\t\"GarrysMod (LDR FINAL)\"\n");
            SequencesModified.Add("\t{\n");

            //VBSP
            SequencesModified.Add("\t\t\"0\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VVIS
            SequencesModified.Add("\t\t\"1\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VRAD
            SequencesModified.Add("\t\t\"2\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-StaticPropLighting -final -ldr -game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //COPY
            SequencesModified.Add("\t\t\"3\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            SequencesModified.Add("\t\t}\n");

            //GAME
            SequencesModified.Add("\t\t\"4\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game " + getEscape() + @GmodPath + @"\garrysmod" + getEscape() + " +map $file\"\n");
            SequencesModified.Add("\t\t}\n");

            SequencesModified.Add("\t}\n");

            //GMOD HDR FINAL

            SequencesModified.Add("\t\"GarrysMod (HDR FINAL)\"\n");
            SequencesModified.Add("\t{\n");

            //VBSP
            SequencesModified.Add("\t\t\"0\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VVIS
            SequencesModified.Add("\t\t\"1\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VRAD
            SequencesModified.Add("\t\t\"2\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-StaticPropLighting -final -hdr -game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //COPY
            SequencesModified.Add("\t\t\"3\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            SequencesModified.Add("\t\t}\n");

            //GAME
            SequencesModified.Add("\t\t\"4\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game " + getEscape() + @GmodPath + @"\garrysmod" + getEscape() + " +map $file\"\n");
            SequencesModified.Add("\t\t}\n");

            SequencesModified.Add("\t}\n");

            //GMOD LDR + HDR FINAL

            SequencesModified.Add("\t\"GarrysMod (LDR + HDR FINAL)\"\n");
            SequencesModified.Add("\t{\n");

            //VBSP
            SequencesModified.Add("\t\t\"0\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VVIS
            SequencesModified.Add("\t\t\"1\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //VRAD
            SequencesModified.Add("\t\t\"2\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-StaticPropLighting -final -both -game $gamedir $path\\$file\"\n");
            SequencesModified.Add("\t\t}\n");

            //COPY
            SequencesModified.Add("\t\t\"3\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            SequencesModified.Add("\t\t}\n");

            //GAME
            SequencesModified.Add("\t\t\"4\"\n");
            SequencesModified.Add("\t\t{\n");
            SequencesModified.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            SequencesModified.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            SequencesModified.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            SequencesModified.Add("\t\t\t\"parms\"\t\t\"-game " + getEscape() + @GmodPath + @"\garrysmod" + getEscape() + " +map $file\"\n");
            SequencesModified.Add("\t\t}\n");

            SequencesModified.Add("\t}\n");




            //THE END

            SequencesModified.Add("}\n");

            File.Delete(@SDKPath + @"\bin\hammerplusplus\hammerplusplus_sequences.cfg");
            FileStream SequencesOut = File.Create(@SDKPath + @"\bin\hammerplusplus\hammerplusplus_sequences.cfg");
            for (int i = 0; i < SequencesModified.Count; i++)
            {
                for (int x = 0; x < SequencesModified[i].Length; x++)
                {
                    SequencesOut.WriteByte(Convert.ToByte(SequencesModified[i][x]));
                }
            }
            SequencesOut.Dispose();
            ConsoleWrite("Sequences modified successfully", DisclaimerType.Info);
            ConsoleWrite("Hammer++ has successfully been installed!", DisclaimerType.Info);
            if(MessageBox.Show("Hammer++ has successfully been installed and configured!\nIf you wish to open it now, press 'Yes'", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start(@SDKPath + @"\bin\hammerplusplus.exe");
                }
            }
            catch (Exception ex)
            {
                ConsoleWrite("Hammer++ Configuration Failed! rmod most likely messed up, sorry :(", DisclaimerType.Error);
                ConsoleWrite("Exception details below, send these to retromod8#9627 on Discord\r\n" + ex, DisclaimerType.Error);
            }
            this.Enabled = true;
        }
            
        static void addKeyvalueToConfig(List<string> list, string key, string value)
        {
            list.Add("\t\t\t\t" + "\"" + key + "\"\t\t\"" + value + "\"\r");
        }
        static char getEscape()
        {
            return Convert.ToChar((byte)27);
        }
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



}
