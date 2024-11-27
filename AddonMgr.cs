using Gameloop.Vdf;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P3AddonManager
{
    public class AddonMgr
    {
        public static Addon[] addons = new Addon[0];
        public static Addon GetAddon(int index)
        {
            for (int i = 0; i < addons.Length; i++)
            {
                // BUGBUGBUG
                // link/merge addonlistbox array?
                if (index == addons[i].order)
                {
                    return addons[i];
                }

                //if (i == index)
                //{
                //    return addons[i];
                //}
            }

            return addons[0];
        }

        public static Addon GetAddonByName(string name)
        {
            for (int i = 0; i < addons.Length; i++)
            {
                if (name == addons[i].name)
                {
                    return addons[i];
                }
            }

            return null;
        }

        public static void AddAddon(Addon addon)
        {
            int index = addons.Length + 1;
            Array.Resize(ref addons, index);
            addons[index - 1] = addon;
        }

        public static void RemoveAddon(Addon addon)
        {
            int index = Array.IndexOf(addons, addon);

            int num = 0;
            Addon[] newArr = new Addon[addons.Length - 1];
            for (int i = 0; i < addons.Length; i++)
            {
                if (i == index)
                {
                    continue;
                }

                newArr[num] = addons[i];
                num++;
            }

            addons = newArr;
        }

        // Rearranges array according to the order
        public static void RearrangeAddons(Addon[] addons)
        {
            IEnumerable<Addon> query = addons.OrderBy(addon => addon.order);

            Addon[] newArr = new Addon[addons.Length];
            int index = 0;
            foreach (Addon addon in query)
            {
                newArr[index++] = addon;
            }

            addons = newArr;
            Array.Clear(newArr);
        }

        public static void ReadAddonList()
        {
            if (!File.Exists(Utils.GetAddonList()))
            {
                string addonlist = "\"AddonList\"\n";
                addonlist += "{\n";
                addonlist += "}\n";

                File.WriteAllText(Utils.GetAddonList(), addonlist);
            }

            Addon[] newArr = new Addon[0];

            Dictionary<string, string> addonList = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines(Utils.GetAddonList());
            // Flag to check if we're inside the AddonList section
            bool insideAddonList = false;

            foreach (var line in lines)
            {
                // Trim leading/trailing whitespace
                string trimmedLine = line.Trim();

                // Start reading after "AddonList {"
                if (trimmedLine == "\"AddonList\"" || trimmedLine == "{")
                {
                    insideAddonList = true;
                    continue;
                }

                // Stop reading when we reach the closing brace
                if (trimmedLine == "}")
                {
                    insideAddonList = false;
                    continue;
                }

                if (insideAddonList)
                {
                    // Example format: "key" "value"
                    var parts = trimmedLine.Split(new[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                    //Console.WriteLine($"{parts.Length}\n");
                    if (parts.Length == 3)
                    {
                        string key = parts[0].Trim();
                        string value = parts[2].Trim();
                        //Console.WriteLine($"{key} {value}\n");

                        addonList.Add(key, value);
                    }
                }
            }

            // Print the values to verify
            int index = 0;
            foreach (var pair in addonList)
            {
                //Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");

                bool enabled = false;
                if (pair.Value == "1")
                {
                    enabled = true;
                }

                Addon addon = new($"{pair.Key}", index, enabled);

                if (ReadAddonInfo(addon))
                {
                    AddAddon(addon);
                    addon.CheckFeatures();
                    index++;
                }
            }

            static bool FindConflict(string[] conf, string what)
            {
                for (int i = 0; i < conf.Length; i++)
                {
                    if (conf[i] == what)
                    {
                        return true;
                    }
                }

                return false;
            }

            // Now then check for conflicts...
            // kill me
            for (int i = 0; i < addons.Length; i++)
            {
                for (int j = 0; j < addons.Length; j++)
                {
                    if (addons[i].name != addons[j].name)
                    {
                        for (int k = 0; k < addons[i].files.Length; k++)
                        {
                            for (int l = 0; l < addons[j].files.Length; l++)
                            {
                                if (addons[i].files[k] == addons[j].files[l])
                                {
                                    if (addons[i].files[k] == "ignore")
                                    {
                                        continue;
                                    }

                                    if (!FindConflict(addons[i].conflictFiles, addons[j].files[l]))
                                    {
                                        int confindex = addons[i].conflictFiles.Length + 1;
                                        Array.Resize(ref addons[i].conflictFiles, confindex);
                                        addons[i].conflictFiles[confindex - 1] = addons[j].files[l];
                                    }

                                    if (!FindConflict(addons[j].conflictFiles, addons[i].files[k]))
                                    {
                                        int confindex = addons[j].conflictFiles.Length + 1;
                                        Array.Resize(ref addons[j].conflictFiles, confindex);
                                        addons[j].conflictFiles[confindex - 1] = addons[i].files[k];
                                    }

                                    if (!FindConflict(addons[i].conflicts, addons[j].name))
                                    {
                                        int confindex = addons[i].conflicts.Length + 1;
                                        Array.Resize(ref addons[i].conflicts, confindex);
                                        addons[i].conflicts[confindex - 1] = addons[j].name;
                                    }

                                    if (!FindConflict(addons[j].conflicts, addons[i].name))
                                    {
                                        int confindex = addons[j].conflicts.Length + 1;
                                        Array.Resize(ref addons[j].conflicts, confindex);
                                        addons[j].conflicts[confindex - 1] = addons[i].name;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // GetAddonFolder() + $"{addon.name}\\addoninfo.txt"
        public static bool ReadAddonInfo(Addon addon)
        {
            if (!File.Exists($"{Utils.GetAddonFolder()}{addon.name}\\addoninfo.txt"))
            {
                string msg = $"\"{addon.name}\" has no addoninfo.txt, it will not load in-game.";
                string caption = "Warning";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string fileContent = File.ReadAllText($"{Utils.GetAddonFolder()}{addon.name}\\addoninfo.txt");

            // Really?
            if (fileContent.Length <= 0)
            {
                string msg = $"\"{addon.name}\" has empty addoninfo.txt, it will not load in-game.";
                string caption = "Warning";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Sorry, looks like there's no other way of doing this...
            fileContent = Regex.Replace(fileContent, "addontitle", "addontitle", RegexOptions.IgnoreCase);
            fileContent = Regex.Replace(fileContent, "addonauthor", "addonauthor", RegexOptions.IgnoreCase);
            fileContent = Regex.Replace(fileContent, "addondescription", "addondescription", RegexOptions.IgnoreCase);
            fileContent = Regex.Replace(fileContent, "addonversion", "addonversion", RegexOptions.IgnoreCase);
            fileContent = Regex.Replace(fileContent, "addonlink", "addonlink", RegexOptions.IgnoreCase);

            // Introduced by Ultrapatch+Angel
            fileContent = Regex.Replace(fileContent, "postal3script", "postal3script", RegexOptions.IgnoreCase);
            fileContent = Regex.Replace(fileContent, "angelscript", "angelscript", RegexOptions.IgnoreCase);
            fileContent = Regex.Replace(fileContent, "addonminimum", "addonminimum", RegexOptions.IgnoreCase);

            dynamic volvo = VdfConvert.Deserialize(fileContent);

            AddonInfo info = new AddonInfo();
            addon.info = info;

            addon.info.Title = Convert.ToString(volvo.Value.addontitle);
            addon.info.Description = Convert.ToString(volvo.Value.addondescription);
            addon.info.Author = Convert.ToString(volvo.Value.addonauthor);
            addon.info.Version = Convert.ToString(volvo.Value.addonversion);
            addon.info.Link = Convert.ToString(volvo.Value.addonlink);

            if (addon.info.Title == null || addon.info.Title.Length <= 0)
                addon.info.Title = "Unnamed Addon";
            if (addon.info.Description == null || addon.info.Description.Length <= 0)
                addon.info.Description = "No description.";
            if (addon.info.Author == null || addon.info.Author.Length <= 0)
                addon.info.Author = "Anonymous";
            if (addon.info.Link == null || addon.info.Link.Length <= 0)
                addon.info.Link = "n/a";

            // Introduced by Ultrapatch+Angel
            addon.info.Postal3Script = Convert.ToString(volvo.Value.postal3script);
            addon.info.AngelScript = Convert.ToString(volvo.Value.angelscript);
            addon.info.Minimum = Convert.ToString(volvo.Value.addonminimum);

            // Fallback
            if (addon.info.Minimum == null || addon.info.Minimum.Length <= 0)
                addon.info.Minimum = "zoom";

            // Check if it's ZOOM, Ultrapatch or Angel..
            addon.info.CheckMinimum();

            if (File.Exists($"{Utils.GetAddonFolder()}{addon.name}\\{addon.name}.png"))
                addon.Image = new Bitmap($"{Utils.GetAddonFolder()}{addon.name}\\{addon.name}.png");

            return true;
        }
    }

}