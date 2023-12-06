using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HammerPP_Manager
{
    internal static class HelpfulTools
    {
        //Used to check sdk directory for most files and directories to confirm it's authentic
        internal static bool SDKSanityCheck(string basePath)
        {
            string[] DirCheckList = {
            "\\bin",
            "\\hl2",
            "\\hl2mp",
            "\\platform",
            "\\sdktools",
            "\\sourcetest"
            };

            string[] FileCheckList = {
                "\\hl2.exe",
                "\\bin\\vbsp.exe",
                "\\bin\\vvis.exe",
                "\\bin\\vrad.exe"
            };

            for (int i = 0; i < DirCheckList.Length; i++)
            {
                if (!Directory.Exists(basePath + DirCheckList[i]))
                {
                    return false;
                }
            }

            for (int i = 0; i < FileCheckList.Length; i++)
            {
                if (!File.Exists(basePath + FileCheckList[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }

}
