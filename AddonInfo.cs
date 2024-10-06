using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3AddonManager
{
    public class AddonInfo
    {
        public AddonInfo()
        {
            Title = "null";
            Author = "null";
            Version = "null";
            Description = "null";
            Link = "null";

            // Introduced by Ultrapatch+Angel
            Postal3Script = "null";
            AngelScript = "null";
            Minimum = "null";
            MinimumVer = "null";
        }
        public void CheckMinimum()
        {
            // ZOOM addon (example)
            if (Minimum == null && MinimumVer == null)
            {
                Minimum = "ZOOM";
                bZOOM = true;

                bIGNOREVER = true;
                MinimumVer = "(ALL)";
                return;
            }

            // oh god, oh fuck
            string ver = Minimum.ToLower();

            string vermin = MinimumVer.ToLower();

            if (ver.Contains("zoom"))
            {
                Minimum = "ZOOM";
                bZOOM = true;

                if (vermin.Contains("all") || vermin.Length < 1)
                {
                    bIGNOREVER = true;
                    MinimumVer = "(ALL)";
                }
            }
            else
            {
                // Ultrapatch and Angel can't have ALL since they never had modloader from the beginning

                if (ver.Contains("ultrapatch") || ver.Contains("up"))
                {
                    Minimum = "Ultrapatch";
                    bULTRAPATCH = true;
                    bANGEL = true;
                }

                if (ver.Contains("angel"))
                {
                    Minimum = "UP+Angel";
                    bANGEL = true;
                }
            }
        }

        public bool bZOOM = false;
        public bool bULTRAPATCH = false;
        public bool bANGEL = false;
        public bool bIGNOREVER = false;

        public string Title = new string("null");
        public string Author = new string("null");
        public string Version = new string("null");
        public string Description = new string("null");
        public string Link = new string("null");
        public string Postal3Script = new string("null");
        public string AngelScript = new string("null");
        public string Minimum = new string("null");
        public string MinimumVer = new string("null");
    }
}
