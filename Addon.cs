using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharpVPK;
using SharpVPK.V1;
using SharpVPK.V2;

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

            string[] filesTemp = Directory.GetFiles(AddonFolder, "*.*", SearchOption.AllDirectories);

            bool IgnoreThis(string file)
            {
                if (file.EndsWith("vpk"))
                {
                    return true;
                }

                if (has.p3s)
                {
                    if (file == info.Postal3Script.Replace("/", "\\"))
                    {
                        return true;
                    }
                }

                if (has.ans)
                {
                    if (file == info.AngelScript.Replace("/", "\\"))
                    {
                        return true;
                    }
                }

                if (file == "addoninfo.txt")
                {
                    return true;
                }

                return false;
            }

            for (int i = 0; i < filesTemp.Length; i++)
            {
                filesTemp[i] = filesTemp[i].Replace(AddonFolder, "").ToLower();

                // Force ignore certain files
                if (IgnoreThis(filesTemp[i]))
                {
                    filesTemp[i] = "ignore";
                }

                //Console.WriteLine(filesTemp[i]);
            }

            if (has.vpk)
            {
                int index = filesTemp.Length + 1;
                void AddToFileArray(string entry)
                {
                    Array.Resize(ref filesTemp, index);
                    filesTemp[index - 1] = entry;
                }

                if (File.Exists(AddonFolder + "pak01_dir.vpk"))
                {
                    var archive = new VpkArchive();
                    archive.Load(@AddonFolder + "pak01_dir.vpk");

                    foreach (var directory in archive.Directories)
                        foreach (var entry in directory.Entries)
                            AddToFileArray($"(VPK) {entry.ToString().Replace('/', '\\')}");

                    if (archive.IsV1)
                        vpkV1 = true;
                    else if (archive.IsV2)
                        vpkV2 = true;
                }

                index = filesTemp.Length + 1;
                if (File.Exists(AddonFolder + name + ".vpk"))
                {
                    var archive = new VpkArchive();
                    archive.Load(@AddonFolder + name + ".vpk");

                    foreach (var directory in archive.Directories)
                        foreach (var entry in directory.Entries)
                            AddToFileArray($"(VPK) {entry.ToString().Replace('/', '\\')}");

                    if (archive.IsV1)
                        vpkV1 = true;
                    else if (archive.IsV2)
                        vpkV2 = true;
                }
            }

            files = filesTemp;
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

        public string[] files = new string[0];

        public string[] conflicts = new string[0];
        public string[] conflictFiles = new string[0];

        public bool vpkV1 = false;
        public bool vpkV2 = false;
    }
}
