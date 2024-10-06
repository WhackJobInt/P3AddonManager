using Gameloop.Vdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P3AddonManager
{
    public static class Utils
    {
        public static string? GetExeLocation()
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess()?.MainModule?.FileName);
        }

        public static void Reset()
        {
            //exePath = null;
            P3Hash.Current_Client = "";
            P3Hash.Current_Server = "";
            P3Hash.Current_Exe = "";

            ZOOM = false;
            STEAM = false;
            ULTRAPATCH = false;
            ANGEL = false;
            UNKNOWN = false;
            RETAIL = false;
            NODRM = false;

            UNSUPPORTED = false;
        }

        static string? exePath;
        public static void SetExePath(string path) { exePath = path; }
        public static string GetExePath() { return exePath ?? ""; }
        public static string GetAddonList() { return GetExePath() + "p3\\addonlist.txt"; }
        public static string GetAddonFolder() { return GetExePath() + "p3\\addons\\"; }

        public static string GameFolder = "p3";

        public static string GetGameFolder() { return GameFolder; }

        public static P3Hash.P3Version CurrentGameVersion = P3Hash.P3Version.Unknown;
        public static P3Hash.P3Version GetGameVersion() { return CurrentGameVersion; }

        // Avoid MD5 calculation
        static bool ZOOM = false;
        static bool STEAM = false;
        static bool ULTRAPATCH = false;
        static bool ANGEL = false;

        static bool RETAIL = false;
        static bool NODRM = false;

        static bool UNKNOWN = false;

        static bool UNSUPPORTED = false;

        public static void DetermineInstallation(Label gameVersionLabel)
        {
            P3Hash.Current_Client = P3Hash.GenerateMD5(GetExePath() + $"{GetGameFolder()}\\bin\\client.dll");
            P3Hash.Current_Server = P3Hash.GenerateMD5(GetExePath() + $"{GetGameFolder()}\\bin\\server.dll");
            P3Hash.Current_Exe = P3Hash.GenerateMD5(GetExePath() + "p3.exe");

            P3Hash.Current_Client = P3Hash.Current_Client.ToUpper();
            P3Hash.Current_Server = P3Hash.Current_Server.ToUpper();
            P3Hash.Current_Exe = P3Hash.Current_Exe.ToUpper();

            Console.WriteLine(P3Hash.Current_Client);

            if (CheckZOOM())
            {
                ZOOM = true;
            }
            else if (CheckSTEAM())
            {
                STEAM = true;
            }
            else if (CheckRETAIL())
            {
                RETAIL = true;
            }
            else if (CheckNODRM())
            {
                NODRM = true;
            }
            else
            {
                UNKNOWN = true;

                for (P3Hash.P3Version i = P3Hash.P3Version.Unknown; i < P3Hash.P3Version.MAX; i++)
                {
                    if (P3Hash.Current_Client == P3Hash.P3_Clients[(int)i] &&
                        P3Hash.Current_Server == P3Hash.P3_Servers[(int)i] &&
                        P3Hash.Current_Exe == P3Hash.P3_Exes[(int)i])
                    {
                        UNKNOWN = false;
                        CurrentGameVersion = i;
                        break;
                    }
                }
            }

            if (CheckUNSUPPORTED())
            {
                UNSUPPORTED = true;
            }

            gameVersionLabel.Text = $"Detected Version: {GetGameVersion().ToString().Replace("_", ".")}";

            // Funny easter egg (2007 leaked build)
            if (P3Hash.Current_Client == "359BA3E5674A52F392C3A374F3D907C4" &&
                P3Hash.Current_Server == "2961E70A8E4A22DE92869E2B0B86B19C")
            {
                gameVersionLabel.Text = $"Detected Version: WTF?";
            }

            if (IsUNSUPPORTED())
            {
                gameVersionLabel.ForeColor = cUNSUPPORTED;
            }
            else if (IsUNKNOWN())
            {
                gameVersionLabel.ForeColor = cUNKNOWN;
            }
            else
            {
                gameVersionLabel.ForeColor = cPASS;
            }
        }

        public static Color cUNKNOWN = Color.Olive;
        public static Color cUNSUPPORTED = Color.Red;
        public static Color cPASS = Color.ForestGreen;

        public static void ConvertVersionToLabel(Label label, Addon addon)
        {
            label.ForeColor = cUNSUPPORTED;

            if (IsUNKNOWN())
            {
                label.ForeColor = cUNKNOWN;
            }

            if (Utils.IsULTRAPATCH() && !Utils.IsANGEL())
            {
                if (addon.info.bULTRAPATCH && addon.info.bANGEL)
                {
                    label.ForeColor = cUNSUPPORTED;
                }
                else if (addon.info.bULTRAPATCH && !addon.info.bANGEL)
                {
                    label.ForeColor = cPASS;
                }

                if (addon.info.bZOOM)
                {
                    label.ForeColor = cPASS;
                }
            }

            if (Utils.IsANGEL())
            {
                if (addon.info.bULTRAPATCH || addon.info.bANGEL || addon.info.bZOOM)
                {
                    label.ForeColor = cPASS;
                }
            }

            if (Utils.IsZOOM())
            {
                if (addon.info.bULTRAPATCH || addon.info.bANGEL)
                {
                    label.ForeColor = cUNSUPPORTED;
                }
                else if (addon.info.bZOOM)
                {
                    label.ForeColor = cPASS;
                }
            }

            // It's all good, but you never know if latest version broke something
            if (label.ForeColor == cPASS)
            {
                if (addon.info.MinimumVer != "all")
                {
                    if (addon.info.MinimumVer != GetGameVersion().ToString().ToLower())
                    {
                        label.ForeColor = cUNSUPPORTED;
                    }
                }
            }
        }

        static bool CheckUNSUPPORTED()
        {
            // Game version doesn't have addon support
            if (CurrentGameVersion < P3Hash.P3Version.ADDON_SUPPORT)
                return true;

            if (IsSTEAM() || IsRETAIL() || IsNODRM())
                return true;

            return false;
        }

        static bool CheckSTEAM()
        {
            // pre-ZOOM Steam has this
            if (File.Exists(GetExePath() + "givacyzo.dll") && 
                File.Exists(GetExePath() + "givacyzo.x86"))
            {
                if (P3Hash.Current_Client == P3Hash.P3_Clients[(int)P3Hash.P3Version.Steam] &&
                    P3Hash.Current_Server == P3Hash.P3_Servers[(int)P3Hash.P3Version.Steam])
                {
                    if (P3Hash.Current_Exe == P3Hash.P3_Exes[(int)P3Hash.P3Version.Steam])
                    {
                        CurrentGameVersion = P3Hash.P3Version.Steam;
                        return true;
                    }
                }
            }

            return false;
        }

        static bool CheckRETAIL()
        {
            if (File.Exists(GetExePath() + "asylygac.dll") && 
                File.Exists(GetExePath() + "asylygac.x86") &&
                File.Exists(GetExePath() + "pcnsl.exe"))
            {
                for (int i = 0; i < P3Hash.P3_Clients.Length; i++)
                {
                    if (P3Hash.Current_Exe == P3Hash.P3_Exes[i])
                    {
                        if (P3Hash.Current_Client == P3Hash.P3_Clients[i] &&
                            P3Hash.Current_Server == P3Hash.P3_Servers[i])
                        {
                            if (P3Hash.Current_Exe == P3Hash.P3_Exes[i])
                            {
                                CurrentGameVersion = (P3Hash.P3Version)i;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        static bool CheckZOOM()
        {
            if (File.Exists(GetExePath() + "p3.dll"))
            {
                if (P3Hash.Current_Exe == P3Hash.P3_Exes[(int)P3Hash.P3Version.ZOOM])
                {
                    if (P3Hash.Current_Client == P3Hash.P3_Clients[(int)P3Hash.P3Version.ZOOM] &&
                        P3Hash.Current_Server == P3Hash.P3_Servers[(int)P3Hash.P3Version.ZOOM])
                    {
                        CurrentGameVersion = P3Hash.P3Version.ZOOM;
                        return true;
                    }
                }
            }

            return false;
        }

        static bool CheckNODRM()
        {
            if (P3Hash.Current_Exe == P3Hash.P3_Exes[(int)P3Hash.P3Version.NoDRM])
            {
                if (P3Hash.Current_Client == P3Hash.P3_Clients[(int)P3Hash.P3Version.NoDRM] ||
                    P3Hash.Current_Client == P3Hash.P3_Clients[(int)P3Hash.P3Version.VicNoDRM])
                {
                    if (P3Hash.Current_Server == P3Hash.P3_Servers[(int)P3Hash.P3Version.NoDRM])
                    {
                        CurrentGameVersion = P3Hash.P3Version.NoDRM;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsNODRM()
        {
            return NODRM;
        }

        public static bool IsRETAIL()
        {
            return RETAIL;
        }

        public static bool IsUNKNOWN()
        {
            return UNKNOWN;
        }

        public static bool IsANGEL()
        {
            return ANGEL;
        }

        public static bool IsULTRAPATCH()
        {
            return ULTRAPATCH;
        }

        public static bool IsSTEAM()
        {
            return STEAM;
        }

        public static bool IsZOOM()
        {
            return ZOOM;
        }
        public static bool IsUNSUPPORTED()
        {
            return UNSUPPORTED;
        }

        public static void OpenUrl(string url)
        {
            // For whatever reason this doesn't work, so had to do this
            try
            {
                Process.Start(url);
            }
            catch
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }

        public static void OpenFile(string path, string arguments = null)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = path,
                Arguments = arguments
            };

            process.Start();
            //process.WaitForExit();
        }

        public static void OpenFolder(string path)
        {
            Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
