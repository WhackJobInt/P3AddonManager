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

            GameVersion = P3Hash.P3Version.Unknown;
        }
        public void CheckMinimum()
        {
            bZOOM = false;
            bULTRAPATCH = false;
            bANGEL = false;
            bool bUnknown = true;

            // ZOOM addon (example)
            if (Minimum == null)
            {
                Minimum = "ZOOM";
                bZOOM = true;
                GameVersion = P3Hash.P3Version.ZOOM;
                return;
            }

            // oh god, oh fuck
            string ver = Minimum.ToLower();

            if (ver.Contains("zoom"))
            {
                Minimum = "ZOOM";
                bZOOM = true;
                bUnknown = false;
                GameVersion = P3Hash.P3Version.ZOOM;
            }
            else
            {
                // TODO: Simplify this for future releases
                // ex.: Angelv1.2.0 mods will/might not work with Angelv1.1.0

                if (ver.Contains("ultrapatch") || ver.Contains("up"))
                {
                    Minimum = "Ultrapatch";
                    bULTRAPATCH = true;
                    bUnknown = false;

                    // TODO: Initial version of Ultrapatch with Addon support
                    GameVersion = P3Hash.P3Version.Ultrapatchv1_0_0;
                }

                // Angel is much more strict about versions
                if (ver.Contains("angel"))
                {
                    Minimum = "Ultrapatch Angel";
                    bANGEL = true;
                    bULTRAPATCH = true;

                    bUnknown = false;

                    // Initial release of Angel with Addon support
                    GameVersion = P3Hash.P3Version.Angelv1_1_0;

                    // TODO: there's gotta be a better way to handle this...
                    if (ver.Contains("angelv"))
                    {
                        if (ver.EndsWith("1.1.0") || ver.EndsWith("1_1_0"))
                        {
                            GameVersion = P3Hash.P3Version.Angelv1_1_0;
                        }
                        else if (ver.EndsWith("1.2.0") || ver.EndsWith("1_2_0"))
                        {
                            //
                        }
                    }
                }
            }

            if (bUnknown)
            {
                bUNKNOWN = true;
            }
        }

        public bool bZOOM = false;
        public bool bULTRAPATCH = false;
        public bool bANGEL = false;
        public bool bUNKNOWN = true;

        public string Title = new string("null");
        public string Author = new string("null");
        public string Version = new string("null");
        public string Description = new string("null");
        public string Link = new string("null");
        public string Postal3Script = new string("null");
        public string AngelScript = new string("null");
        public string Minimum = new string("null");

        public P3Hash.P3Version GameVersion = P3Hash.P3Version.Unknown;
    }
}
