using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3AddonManager
{
    public class Addon
    {
        public Addon(string sname, int iorder, bool benabled)
        {
            info = new AddonInfo();
            has = new AddonFeature();

            name = sname;
            order = iorder;
            enabled = benabled;

            Image = null;

            enabledOrder = -1;
        }

        public void CheckFeatures()
        {
            string AddonFolder = Utils.GetAddonFolder() + name + "\\";

            if (Directory.Exists(AddonFolder + "cfg"))
            {
                has.cfg = true;
            }
            if (Directory.Exists(AddonFolder + "maps"))
            {
                has.map = true;
            }
            if (Directory.Exists(AddonFolder + "materials"))
            {
                has.material = true;
            }
            if (Directory.Exists(AddonFolder + "media"))
            {
                has.media = true;
            }
            if (Directory.Exists(AddonFolder + "models"))
            {
                has.model = true;
            }
            if (Directory.Exists(AddonFolder + "particles"))
            {
                has.particle = true;
            }
            if (Directory.Exists(AddonFolder + "resource"))
            {
                has.resource = true;
            }
            if (Directory.Exists(AddonFolder + "scripts"))
            {
                has.script = true;
            }
            if (Directory.Exists(AddonFolder + "shaders"))
            {
                has.shader = true;
            }
            if (Directory.Exists(AddonFolder + "sound"))
            {
                has.sound = true;
            }

            if (File.Exists(AddonFolder + name + ".vpk"))
            {
                has.vpk = true;
            }

            if (File.Exists(AddonFolder + "pak01_dir.vpk"))
            {
                has.vpk = true;
            }

            if (File.Exists(AddonFolder + info.Postal3Script))
            {
                has.p3s = true;
            }

            if (File.Exists(AddonFolder + info.AngelScript))
            {
                has.ans = true;
            }

        }

        // Name of the addon, i.e. "exelocation/p3/addons/''''MyAddon''''"
        public string name;

        // First in order? Second in order?
        public int order;

        // Enabled?
        public bool enabled;

        // The order of which the addon is enabled (not array-like)
        public int enabledOrder;

        public AddonInfo info;
        public AddonFeature has;

        public Bitmap? Image;
    }
}
