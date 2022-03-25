using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;

namespace HammerPP_Installer_for_GMOD
{
    internal class Program
    {
        static string GmodDirectory;
        static string SdkDirectory;

        enum ColorPrintType
        {
            Information,
            Error,
            Hint
        }

        static void Main(string[] args)
        {
            checkInternetConnection();
            GmodDirectory = EnterPath("Gmod", "Garry's Mod", "GarrysMod");
            SdkDirectory = EnterPath("Source SDK", "Source SDK Base 2013 Multiplayer", "Source SDK Base 2013 Multiplayer");
            if (isHammerPPInstalled())
            {
                ColorPrint("Looks like Hammer++ is already installed!\nIf you wish to install Hammer++ again, please delete:\n\n", ColorPrintType.Error);
                ColorPrint(@SdkDirectory + @"\bin\hammerplusplus.exe" +"\n"+ @SdkDirectory + @"\bin\hammerplusplus" +"\n", ColorPrintType.Error);
                ColorPrint("Press enter to exit...", ColorPrintType.Hint);
                Console.ReadLine();
                Environment.Exit(1);
            }
            ColorPrint("Warning!\nBy pressing 'enter' you agree to install Hammer++ and agree to the license of this program.\nIf you don't, please exit this program!\n", ColorPrintType.Error);
            Thread.Sleep(1000); //cooldown
            Console.ReadLine();
            try
            {
                installHammer(Convert.ToString(getCurrentVersion()));
            }
            catch (Exception ex)
            {
                ColorPrint("Something bad happened and i don't know why!!!!!\nError details listed below:" + ex+ "\n\nOpen up 'Snipping Tool' and take a screenshot of this,\nthen send it to retromod8#9627 on Discord", ColorPrintType.Error);
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        static string EnterPath(string Message, string FullName, string ExpectedFolderName)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Please enter the path to " + Message + "'s Directory");
                ColorPrint("You can find it by right-clicking on '" + FullName + "' then selecting Properties>Local Files>Browse...\n", ColorPrintType.Hint);
                string UserInput = @Console.ReadLine();
                Console.Clear();
                try
                {
                    if (UserInput[0] == '"')
                    {
                        UserInput = UserInput.Substring(1, UserInput.Length - 2);
                    }
                }
                catch (Exception)
                {

                }
                if (Directory.Exists(@UserInput))
                {
                    if (!Directory.Exists(@UserInput + @"\bin"))
                    {
                        ColorPrint("Path does not contain bin folder! Ensure your path is valid!\n", ColorPrintType.Error);
                        continue;
                    }
                    else
                    {
                        try
                        {
                            if (@UserInput.Substring(@UserInput.Length - ExpectedFolderName.Length, ExpectedFolderName.Length) == ExpectedFolderName)
                            {
                                return @UserInput;
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception)
                        {
                            ColorPrint("Incorrect folder name! Please make sure the selected folder is called '" + ExpectedFolderName + "'\n", ColorPrintType.Error);
                            continue;
                        }

                    }
                }
                ColorPrint("Path is incorrect! Please try a different path.\n", ColorPrintType.Error);
            }
        }

        //Colourful text!
        static void ColorPrint(string Message, ColorPrintType Colour)
        {
            ConsoleColor currentTextColor = Console.ForegroundColor;
            ConsoleColor currentBackColor = Console.BackgroundColor;
            ConsoleColor textColor;
            ConsoleColor backColor;
            switch (Colour)
            {
                case ColorPrintType.Information:
                    textColor = ConsoleColor.White;
                    backColor = ConsoleColor.Blue;
                    break;

                case ColorPrintType.Error:
                    textColor = ConsoleColor.Yellow;
                    backColor = ConsoleColor.DarkRed;
                    break;

                case ColorPrintType.Hint:
                    textColor = ConsoleColor.White;
                    backColor = ConsoleColor.Magenta;
                    break;

                default:
                    textColor = currentTextColor;
                    backColor = currentBackColor;
                    break;
            }
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backColor;
            Console.Write(Message);
            Console.ForegroundColor = currentTextColor;
            Console.BackgroundColor = currentBackColor;
        }


        //This gets the current version of Hammer++
        static UInt64 getCurrentVersion()
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent: retr0mod8-gmod_hammerpp_installer"); //This allows us to access the GitHub API
            wc.DownloadFile(@"https://api.github.com/repos/ficool2/HammerPlusPlus-Website/releases/latest", @Path.GetTempPath() + "tempjson");
            while (wc.IsBusy)
            {
                //We gotta wait for the file to download
            }
            string json = @File.ReadAllText(@Path.GetTempPath() + "tempjson"); //Convert file to string
            File.Delete(@Path.GetTempPath() + "tempjson"); //Delete file
            var data = (JObject)JsonConvert.DeserializeObject(json); //Deserialize
            wc.Dispose(); //Don't need this anymore. Bye bye!
            return Convert.ToUInt32(data["tag_name"].Value<string>()); //Find tag_name and return it's value (HammerPP's recent version)
        }

        static void checkInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                    client.OpenRead("http://google.com/generate_204");
            }
            catch (Exception)
            {
                Console.Clear();
                ColorPrint("Device not connected to the internet!\nPlease restart the software when your device is connected.\n", ColorPrintType.Error);
                Console.WriteLine("Press enter to close the program...");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }


        static bool isHammerPPInstalled()
        {
            return (File.Exists(@SdkDirectory + @"\bin\hammerplusplus.exe") || Directory.Exists(@SdkDirectory + @"\bin\hammerplusplus"));
        }



        static void downloadLatestVersion(string version)
        {
            
            using (WebClient wc = new WebClient())
            {
                try
                {
                    Directory.Delete(@SdkDirectory + @"\hammerpp", true);
                }
                catch (Exception)
                {

                }
                string zippath = @SdkDirectory + @"\temp.zip";
                wc.DownloadFile(@"https://github.com/ficool2/HammerPlusPlus-Website/releases/download/" + version + "/hammerplusplus_2013mp_build" + version + ".zip", @zippath);
                while (wc.IsBusy)
                {

                }
                if (Directory.Exists(@SdkDirectory + @"\hammerpp"))
                    Directory.Delete(@SdkDirectory + @"\hammerpp", true);
                ZipFile.ExtractToDirectory(@zippath, @SdkDirectory + @"\hammerpp");
                File.Delete(zippath);
            }
        }

        static void installHammer(string version)
        {
            Console.Clear();
            ColorPrint("Downloading latest version...\n", ColorPrintType.Information);
            downloadLatestVersion(version);
            ColorPrint("Installing...\n", ColorPrintType.Information);
            File.Move(@SdkDirectory + @"\hammerpp\hammerplusplus_2013mp_build" + version + @"\bin\hammerplusplus.exe", @SdkDirectory + @"\bin\hammerplusplus.exe");
            Directory.Move(@SdkDirectory + @"\hammerpp\hammerplusplus_2013mp_build" + version + @"\bin\hammerplusplus", @SdkDirectory + @"\bin\hammerplusplus");
            Directory.Delete(@SdkDirectory + @"\hammerpp", true);
            //files installed, now we gotta configure crap
            Console.Clear();
            while (true)
            {
                Process.Start(@SdkDirectory + @"\bin");
                ColorPrint("Please open \"hammerplusplus.exe\", select a game config, click 'OK', then close it once it finishes loading,\nthen press enter here.\n", ColorPrintType.Hint);
                Console.ReadLine();
                Console.Clear();
                if (!File.Exists(@SdkDirectory + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt"))
                {
                    ColorPrint("Please launch \"hammerplusplus.exe\"!\n", ColorPrintType.Error);
                }
                else
                    break;
            }
            //hammer++ gameconfig has been generated, time to modify it!
            FileStream fs = File.OpenRead(@SdkDirectory + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt");
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);
            fs.Close();
            fs.Dispose();
            //code above copies file to memorystream, makes searching faster!
            ms.Position = 0; //set position to 0 otherwise exception for some reason
            string buffer = ""; //this variable is declared here so it doesn't get constantly thrown out of scope in the for loop, wasting time
            char current; //ditto
            //first we're gonna split the file into a list of strings, makes it easier to process
            List<string> WordList = new List<string>();
            for (int i = 0; i < ms.Length; i++)
            {
                current = Convert.ToChar(Convert.ToByte(ms.ReadByte()));
                switch (current)
                {
                    case '\n':
                        WordList.Add(buffer);
                        buffer = "";
                        break;

                    default:
                        buffer += current;
                        break;
                }
            }
            //now we've scanned the file into a list, lets drop the memory stream
            ms.Close();
            ms.Dispose();

            /*
             * Now we're gonna basically scan each word and add it to
             * a new list until we get to the end of the group
             * which defines game configs. This is the only
             * foreseable way of doing it. I tried to convert
             * the file into an object using structs but the way
             * i did it did not work, and i can't be arsed to find the issue.
             */
            List<string> TheCoolerWordList = new List<string>();
            for(int i = 0; i < WordList.Count; i++)
            {
                if (WordList[i] != "\t}\r")
                    TheCoolerWordList.Add(WordList[i]);
                else
                    break;
                
            }

            //Boilerplate code below

            TheCoolerWordList.Add("\t\t\"Gmod\"\r");
            TheCoolerWordList.Add("\t\t{\r");
            TheCoolerWordList.Add("\t\t\t" +    "\"GameDir\"\t\t\"" + @SdkDirectory + @"\hl2" + "\"\r");
            TheCoolerWordList.Add("\t\t\t" +    "\"Hammer\"" + "\r");
            TheCoolerWordList.Add("\t\t\t" +    "{" + "\r");

            //Get all fgd files in gmod bin directory....
            string[] fgds = Directory.GetFiles(@GmodDirectory + @"\bin", "*.fgd");
            for(int i = 0; i < fgds.Length; i++)
            {
                //and add em....
                addKeyvalueToConfig(TheCoolerWordList, "GameData" + i, @fgds[i]);
            }
            addKeyvalueToConfig(TheCoolerWordList, "TextureFormat", "5");
            addKeyvalueToConfig(TheCoolerWordList, "MapFormat", "4");
            addKeyvalueToConfig(TheCoolerWordList, "DefaultTextureScale", "0.250000");
            addKeyvalueToConfig(TheCoolerWordList, "DefaultLightmapScale", "16");
            addKeyvalueToConfig(TheCoolerWordList, "GameExe", @GmodDirectory + @"\hl2.exe");
            addKeyvalueToConfig(TheCoolerWordList, "DefaultSolidEntity", "func_detail");
            addKeyvalueToConfig(TheCoolerWordList, "DefaultPointEntity", "info_player_start");
            addKeyvalueToConfig(TheCoolerWordList, "BSP", @SdkDirectory + @"\bin\vbsp.exe");
            addKeyvalueToConfig(TheCoolerWordList, "Vis", @SdkDirectory + @"\bin\vvis.exe");
            addKeyvalueToConfig(TheCoolerWordList, "Light", @SdkDirectory + @"\bin\vrad.exe");
            addKeyvalueToConfig(TheCoolerWordList, "GameExeDir", @GmodDirectory);
            addKeyvalueToConfig(TheCoolerWordList, "MapDir", @GmodDirectory + @"\garrysmod\mapsrc");
            addKeyvalueToConfig(TheCoolerWordList, "BSPDir", @GmodDirectory + @"\garrysmod\maps");
            addKeyvalueToConfig(TheCoolerWordList, "PrefabDir", @GmodDirectory + @"\bin\Prefabs");
            addKeyvalueToConfig(TheCoolerWordList, "CordonTexture", "dev/dev_blendmeasure");
            addKeyvalueToConfig(TheCoolerWordList, "MaterialExcludeCount", "0");
            addKeyvalueToConfig(TheCoolerWordList, "Previous", "0");
            TheCoolerWordList.Add("\t\t\t" + "}" + "\r");
            TheCoolerWordList.Add("\t\t" + "}" + "\r");
            TheCoolerWordList.Add("\t" + "}" + "\r");
            TheCoolerWordList.Add("\t" + "\"SDKVersion\"\t\t\"5\"" + "\r");
            TheCoolerWordList.Add("}" + "\r");

            File.Delete(@SdkDirectory + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt");
            FileStream output = File.Create(@SdkDirectory + @"\bin\hammerplusplus\hammerplusplus_gameconfig.txt");
            output.Position = 0;
            for(int i = 0; i < TheCoolerWordList.Count; i++)
            {
                TheCoolerWordList[i] = TheCoolerWordList[i] += "\n";
                for(int x = 0; x < TheCoolerWordList[i].Length; x++)
                {
                    output.WriteByte(Convert.ToByte(TheCoolerWordList[i][x]));
                }
            }
            output.Close();
            output.Dispose();

            ColorPrint("GameConfig modified", ColorPrintType.Hint);

            //Here is were we're gonna change gameinfo to include gmod assets
            WordList.Clear();
            TheCoolerWordList.Clear();
            FileStream fs2 = File.OpenRead(@SdkDirectory + @"\hl2\gameinfo.txt");
            MemoryStream ms2 = new MemoryStream();
            fs2.CopyTo(ms2);
            fs2.Close();
            fs2.Dispose();
            ms2.Position = 0;
            for(int i = 0; i < ms2.Length; i++)
            {
                current = Convert.ToChar(Convert.ToByte(ms2.ReadByte()));
                switch (current)
                {
                    case '\n':
                        WordList.Add(buffer);
                        buffer = "";
                        break;

                    default:
                        buffer += current;
                        break;
                }
            }
            ms2.Close();
            ms2.Dispose();

            for (int i = 0; i < WordList.Count; i++)
            {
                if (WordList[i] != "\t\t\tplatform\t\t\t|all_source_engine_paths|platform\r")
                    TheCoolerWordList.Add(WordList[i]);
                else
                {
                    TheCoolerWordList.Add(WordList[i]);
                    break;
                }
                    
            }

            TheCoolerWordList.Add("\t\t\tgame\t\t\t\t\""+@GmodDirectory + @"\garrysmod" + "\"\r");
            TheCoolerWordList.Add("\t\t\tgame\t\t\t\t\""+@GmodDirectory + @"\garrysmod\garrysmod_dir.vpk" + "\"\r");
            TheCoolerWordList.Add("\t\t}\r");
            TheCoolerWordList.Add("\t}\r");
            TheCoolerWordList.Add("}\r");

            File.Delete(@SdkDirectory + @"\hl2\gameinfo.txt");
            FileStream output2 = File.Create(@SdkDirectory + @"\hl2\gameinfo.txt");
            output2.Position = 0;
            for (int i = 0; i < TheCoolerWordList.Count; i++)
            {
                TheCoolerWordList[i] = TheCoolerWordList[i] += "\n";
                for (int x = 0; x < TheCoolerWordList[i].Length; x++)
                {
                    output2.WriteByte(Convert.ToByte(TheCoolerWordList[i][x]));
                }
            }
            output2.Close();
            output2.Dispose();

            ColorPrint("\nGameInfo modified", ColorPrintType.Hint);

            //////// SEQUENCES ////////
            ///

            WordList.Clear();
            TheCoolerWordList.Clear();
            FileStream fs3 = File.OpenRead(@SdkDirectory + @"\bin\hammerplusplus\hammerplusplus_sequences.cfg");
            MemoryStream ms3 = new MemoryStream();
            fs3.CopyTo(ms3);
            fs3.Close();
            fs3.Dispose();
            ms3.Position = 0;
            for (int i = 0; i < ms3.Length; i++)
            {
                current = Convert.ToChar(Convert.ToByte(ms3.ReadByte()));
                switch (current)
                {
                    case '\n':
                        buffer += current;
                        WordList.Add(buffer);
                        buffer = "";
                        break;

                    default:
                        buffer += current;
                        break;
                }
            }
            ms3.Close();
            ms3.Dispose();

            for (int i = 0; i < WordList.Count; i++)
            {
                if (WordList[i] != "}\n")
                    TheCoolerWordList.Add(WordList[i]);
                else
                    break;
            }


            //GMOD FAST

            TheCoolerWordList.Add("\t\"GarrysMod (Fast)\"\n");
            TheCoolerWordList.Add("\t{\n");

            //VBSP
            TheCoolerWordList.Add("\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VVIS
            TheCoolerWordList.Add("\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-fast -game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VRAD
            TheCoolerWordList.Add("\t\t\"2\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-ldr -bounce 2 -noextra -game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //COPY
            TheCoolerWordList.Add("\t\t\"3\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //GAME
            TheCoolerWordList.Add("\t\t\"4\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game "+getEscape() + @GmodDirectory + @"\garrysmod" + getEscape() + " +map $file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            TheCoolerWordList.Add("\t}\n");


            //GMOD LDR FINAL

            TheCoolerWordList.Add("\t\"GarrysMod (LDR FINAL)\"\n");
            TheCoolerWordList.Add("\t{\n");

            //VBSP
            TheCoolerWordList.Add("\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VVIS
            TheCoolerWordList.Add("\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VRAD
            TheCoolerWordList.Add("\t\t\"2\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-StaticPropLighting -final -ldr -game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //COPY
            TheCoolerWordList.Add("\t\t\"3\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //GAME
            TheCoolerWordList.Add("\t\t\"4\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game " + getEscape() + @GmodDirectory + @"\garrysmod" + getEscape() + " +map $file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            TheCoolerWordList.Add("\t}\n");

            //GMOD HDR FINAL

            TheCoolerWordList.Add("\t\"GarrysMod (HDR FINAL)\"\n");
            TheCoolerWordList.Add("\t{\n");

            //VBSP
            TheCoolerWordList.Add("\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VVIS
            TheCoolerWordList.Add("\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VRAD
            TheCoolerWordList.Add("\t\t\"2\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-StaticPropLighting -final -hdr -game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //COPY
            TheCoolerWordList.Add("\t\t\"3\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //GAME
            TheCoolerWordList.Add("\t\t\"4\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game " + getEscape() + @GmodDirectory + @"\garrysmod" + getEscape() + " +map $file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            TheCoolerWordList.Add("\t}\n");

            //GMOD LDR + HDR FINAL

            TheCoolerWordList.Add("\t\"GarrysMod (LDR + HDR FINAL)\"\n");
            TheCoolerWordList.Add("\t{\n");

            //VBSP
            TheCoolerWordList.Add("\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$bsp_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VVIS
            TheCoolerWordList.Add("\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$vis_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //VRAD
            TheCoolerWordList.Add("\t\t\"2\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$light_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-StaticPropLighting -final -both -game $gamedir $path\\$file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //COPY
            TheCoolerWordList.Add("\t\t\"3\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"257\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"$path\\$file.bsp $bspdir\\$file.bsp\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            //GAME
            TheCoolerWordList.Add("\t\t\"4\"\n");
            TheCoolerWordList.Add("\t\t{\n");
            TheCoolerWordList.Add("\t\t\t\"enable\"\t\t\"1\"\n");
            TheCoolerWordList.Add("\t\t\t\"specialcmd\"\t\t\"0\"\n");
            TheCoolerWordList.Add("\t\t\t\"run\"\t\t\"$game_exe\"\n");
            TheCoolerWordList.Add("\t\t\t\"parms\"\t\t\"-game " + getEscape() + @GmodDirectory + @"\garrysmod" + getEscape() + " +map $file\"\n");
            TheCoolerWordList.Add("\t\t}\n");

            TheCoolerWordList.Add("\t}\n");




            //THE END

            TheCoolerWordList.Add("}\n");


            

            File.Delete(@SdkDirectory + @"\bin\hammerplusplus\hammerplusplus_sequences.cfg");
            FileStream output3 = File.Create(@SdkDirectory + @"\bin\hammerplusplus\hammerplusplus_sequences.cfg");
            for (int i = 0; i < TheCoolerWordList.Count; i++)
            {
                for (int x = 0; x < TheCoolerWordList[i].Length; x++)
                {
                    output3.WriteByte(Convert.ToByte(TheCoolerWordList[i][x]));
                }
            }
            output3.Close();
            output3.Dispose();

            ColorPrint("\nHammerPlusPlus Sequences modified", ColorPrintType.Hint);

            ColorPrint("\n\nHammer++ Installed Successfully! WOOO HOOO!!", ColorPrintType.Information);
            Console.WriteLine("\nPress enter to exit the installer and start Hammer++...");
            Console.ReadLine();
            Process.Start(@SdkDirectory + @"\bin\hammerplusplus.exe");
            Environment.Exit(0);
        }

        static void addKeyvalueToConfig(List<string> list, string key, string value)
        {
            list.Add("\t\t\t\t" + "\""+key + "\"\t\t\"" + value + "\"\r");
        }

        static char getEscape()
        {
            return Convert.ToChar((byte)27);
        }
    }
}
