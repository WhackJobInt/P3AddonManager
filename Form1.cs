using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using static P3AddonManager.Form1;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace P3AddonManager
{
    public partial class Form1 : Form
    {
        enum AddonContains
        {
            cfg,
            maps,
            materials,
            media,
            models,
            particles,
            resource,
            scripts,
            shaders,
            sound,
            vpk,
            p3s,
            ans
        };

        bool PathLooping(IniFile ini)
        {
            FolderBrowserDialog folderDlg = new()
            {
                ShowNewFolderButton = false
            };

            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string exe = folderDlg.SelectedPath + "\\p3.exe";
                if (!File.Exists(exe))
                {
                    exe = folderDlg.SelectedPath + "\\cr_base.exe";
                    if (!File.Exists(exe))
                    {
                        MessageBox.Show("No valid Postal III exe was found.", "Exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                ini.Write("Path", folderDlg.SelectedPath + "\\", "Main");
                ini.Write("Game", folderDlg.SelectedPath + "\\p3", "Main");
                return true;
            }

            return false;
        }

        void Reload(bool newRun = true)
        {
            Utils.Reset();

            Array.Clear(AddonMgr.addons);
            AddonMgr.addons = new Addon[0];

            // Calculate MD5...
            Utils.DetermineInstallation(gameVersionLabel);

            AddonMgr.ReadAddonList();

            // Start filling entries...
            addonListBox.Items.Clear();

            for (int i = 0; i < AddonMgr.addons.Length; i++)
            {
                addonListBox.Items.Add($"{AddonMgr.addons[i].name}", AddonMgr.addons[i].enabled);
            }

            if (addonListBox.Items.Count > 0)
                addonListBox.SelectedItem = addonListBox.Items[0];

            // Awful solution to make the Window focused after entering path
            if (newRun)
            {
                Task.Delay(500).Wait();
                TopMost = true;
                Show();
                TopMost = false;
                Task.Delay(500).Wait();
                Activate();
                Focus();
            }

            addonListBox.CheckOnClick = true;

            saveButton.Enabled = false;

            // Will only force reload if it finds a completely new directory
            FindUnusedFolders();
        }

        public Form1()
        {
            bool exeInRoot = false;
            var ini = new IniFile("p3_addonmgr.ini");
            if (!ini.KeyExists("Path", "Main"))
            {
                string exe = $"{Utils.GetExeLocation()}\\p3.exe";
                if (File.Exists(exe))
                {
                    ini.Write("Path", Utils.GetExeLocation() + "\\", "Main");
                    ini.Write("Game", Utils.GetExeLocation() + "\\p3", "Main");
                    exeInRoot = true;
                }
                else
                {
                    // Check for CR
                    exe = $"{Utils.GetExeLocation()}\\cr_base.exe";
                    if (File.Exists(exe))
                    {
                        ini.Write("Path", Utils.GetExeLocation() + "\\", "Main");
                        ini.Write("Game", Utils.GetExeLocation() + "\\cr_base", "Main");
                        exeInRoot = true;
                    }
                }

                if (!exeInRoot)
                {
                    string msg = "No Postal III exe has been found.\nPlease select folder with \"p3.exe\" in it.";
                    string caption = "Information";

                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    bool bResult = false;
                    while (!bResult)
                    {
                        bResult = PathLooping(ini);
                    }
                }
            }

            // If this doesn't exist for some reason, then it'll always be p3
            if (ini.KeyExists("Game", "Main"))
                Utils.GameFolder = ini.Read("Game", "Main");

            Utils.SetExePath(ini.Read("Path", "Main"));

            InitializeComponent();
            addonPictureBox.InitialImage = addonPictureBox.Image;

            addonListBox.AllowDrop = false;

            containsListBox.CheckOnClick = false;
            containsListBox.SelectionMode = SelectionMode.None;

            Reload();
        }

        private void addonListBox_MouseDown(object sender, MouseEventArgs e)
        {
            //if (!addonListBox.AllowDrop) return;

            if (e.Button == MouseButtons.Left)
            {
                addonListBox.AllowDrop = false;
                return;
            }
            else
            {
                addonListBox.AllowDrop = true;
            }

            if (this.addonListBox.SelectedItem == null) return;
            this.addonListBox.DoDragDrop(this.addonListBox.SelectedItem, DragDropEffects.Move);
        }

        private void addonListBox_DragOver(object sender, DragEventArgs e)
        {
            if (!addonListBox.AllowDrop) return;

            e.Effect = DragDropEffects.Move;
        }

        private void addonListBox_DragDrop(object sender, DragEventArgs e)
        {
            if (!addonListBox.AllowDrop) return;

            bool st = (addonListBox.GetItemChecked(addonListBox.SelectedIndex));

            Point point = addonListBox.PointToClient(new Point(e.X, e.Y));
            int index = addonListBox.IndexFromPoint(point);
            if (index < 0) index = addonListBox.Items.Count - 1;
            object? data = addonListBox.SelectedItem;
            if (data != null)
            {
                addonListBox.Items.Remove(data);
                addonListBox.Items.Insert(index, data);
            }

            if (addonListBox.GetItemChecked(index) != st)
            {
                addonListBox.SetItemChecked(index, st);
            }

            for (int i = 0; i < AddonMgr.addons.Length; i++)
            {
                for (int j = 0; j < addonListBox.Items.Count; j++)
                {
                    if (addonListBox.Items[j].ToString() == AddonMgr.addons[i].name)
                    {
                        AddonMgr.addons[i].order = j;
                        continue;
                    }
                }
            }

            AddonMgr.RearrangeAddons(AddonMgr.addons);

            addonListBox.SelectedItem = data;

            // Start filling entries...
            UpdateUI();
        }

        private void UpdateUI(int index = -1)
        {
            // Update the enable state of all addons
            if (index == -1)
            {
                for (int i = 0; i < AddonMgr.addons.Length; i++)
                {
                    for (int j = 0; j < addonListBox.Items.Count - 1; j++)
                    {
                        if (addonListBox.Items[j].ToString() == AddonMgr.addons[i].name)
                        {
                            AddonMgr.addons[i].enabled = addonListBox.GetItemChecked(j);
                        }
                    }
                }
            }
            // addonListBox_ItemCheck shenanigans
            else
            {
                for (int i = 0; i < AddonMgr.addons.Length; i++)
                {
                    if (addonListBox.Items[index].ToString() == AddonMgr.addons[i].name)
                    {
                        AddonMgr.addons[i].enabled = !addonListBox.GetItemChecked(index);
                    }
                }
            }

            int enabledIndex = 0;
            for (int i = 0; i < AddonMgr.addons.Length; i++)
            {
                for (int j = 0; j < AddonMgr.addons.Length; j++)
                {
                    if (i == AddonMgr.addons[j].order)
                    {
                        if (AddonMgr.addons[j].enabled)
                        {
                            AddonMgr.addons[j].enabledOrder = enabledIndex + 1;
                            enabledIndex++;
                        }
                        else
                        {
                            AddonMgr.addons[j].enabledOrder = -1;
                        }
                    }
                }
            }

            Addon addon = AddonMgr.GetAddon(addonListBox.SelectedIndex);

            string state = addon.enabled ? "[ENABLED]" : "[DISABLED]";
            statusLabel.Text = state;

            if (addon.enabled)
            {
                statusLabel.ForeColor = Color.Green;
            }
            else
            {
                statusLabel.ForeColor = Color.Red;
            }

            string title = addon.info.Title ?? "Unnamed Addon";
            string ver = addon.info.Version ?? "n/a";
            string author = addon.info.Author ?? "anonymous";
            string displayText = $"Selected Addon [{title} {ver} by {author}] ({addon.enabledOrder})";

            selectedAddonGroupBox.Text = displayText;

            addonLinkLabel.Text = addon.info.Link ?? "No link";
            descriptionTextBox.Clear();
            descriptionTextBox.Text = (addon.info.Description ?? "No description");

            if (addon.Image != null)
            {
                addonPictureBox.Image = addon.Image;
            }
            else
            {
                addonPictureBox.Image = addonPictureBox.InitialImage;
            }

            minimumVersionDisplayLabel.Text = $"{addon.info.Minimum} {addon.info.MinimumVer}";

            Utils.ConvertVersionToLabel(minimumVersionDisplayLabel, addon);

            containsListBox.SetItemChecked((int)AddonContains.cfg, addon.has.cfg);
            containsListBox.SetItemChecked((int)AddonContains.maps, addon.has.map);
            containsListBox.SetItemChecked((int)AddonContains.materials, addon.has.material);
            containsListBox.SetItemChecked((int)AddonContains.media, addon.has.media);
            containsListBox.SetItemChecked((int)AddonContains.models, addon.has.model);
            containsListBox.SetItemChecked((int)AddonContains.particles, addon.has.particle);
            containsListBox.SetItemChecked((int)AddonContains.resource, addon.has.resource);
            containsListBox.SetItemChecked((int)AddonContains.scripts, addon.has.script);
            containsListBox.SetItemChecked((int)AddonContains.shaders, addon.has.shader);
            containsListBox.SetItemChecked((int)AddonContains.sound, addon.has.sound);

            if (addon.vpkV1)
            {
                containsListBox.Items.Remove(containsListBox.Items[(int)AddonContains.vpk]);
                containsListBox.Items.Insert((int)AddonContains.vpk, "VPK (V1)");
            }
            else if (addon.vpkV2)
            {
                containsListBox.Items.Remove(containsListBox.Items[(int)AddonContains.vpk]);
                containsListBox.Items.Insert((int)AddonContains.vpk, "VPK (V2)");

                string msg = "Postal III doesn't support VPK V2!";
                string caption = "VPK V2 Detected";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (addon.vpkV1 && addon.vpkV2)
            {
                containsListBox.Items.Remove(containsListBox.Items[(int)AddonContains.vpk]);
                containsListBox.Items.Insert((int)AddonContains.vpk, "VPK (V1+V2)");

                string msg = "Postal III doesn't support VPK V2!";
                string caption = "VPK V2 Detected";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            containsListBox.SetItemChecked((int)AddonContains.vpk, addon.has.vpk);

            containsListBox.SetItemChecked((int)AddonContains.p3s, addon.has.p3s);
            containsListBox.SetItemChecked((int)AddonContains.ans, addon.has.ans);

            string conflicts = "Conflicts: ";
            if (addon.conflicts.Length <= 0)
            {
                conflictTextBox.ForeColor = Color.ForestGreen;
                conflicts += "n/a";

                toolTipConflict.SetToolTip(conflictTextBox, "");
            }
            else
            {
                string conflictFiles = "";
                for (int i = 0; i < addon.conflictFiles.Length; i++)
                {
                    conflictFiles += $"{addon.conflictFiles[i]}\n";
                }

                toolTipConflict.SetToolTip(conflictTextBox, conflictFiles);

                conflictTextBox.ForeColor = Color.Olive;
            }

            for (int i = 0; i < addon.conflicts.Length; i++)
            {
                conflicts += $"\"{addon.conflicts[i]}\"";
                if (i + 1 < addon.conflicts.Length)
                {
                    conflicts += ", ";
                }
            }

            conflictTextBox.Text = conflicts;

            saveButton.Enabled = true;

            //Console.WriteLine($"{addonListBox.SelectedIndex}{displayText}");
        }

        private void addonListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void addonListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateUI(e.Index);
        }

        private void addonLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            static bool IsValidUrl(string url)
            {
                Uri uriResult;
                // Try to create a URI and check if the scheme is HTTP/HTTPS
                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                return result;
            }

            if (IsValidUrl(addonLinkLabel.Text))
                Utils.OpenUrl(addonLinkLabel.Text);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            VObject original = new VObject();

            int index = 0;
            for (int i = 0; i < AddonMgr.addons.Length; i++)
            {
                for (int j = 0; j < AddonMgr.addons.Length; j++)
                {
                    if (index == AddonMgr.addons[j].order)
                    {
                        VProperty temp = new VProperty(AddonMgr.addons[j].name, new VValue(AddonMgr.addons[j].enabled ? "1" : "0"));
                        original.Insert(index++, temp);
                    }
                }
            }

            string volvo = VdfConvert.Serialize(original);
            File.WriteAllText(Utils.GetAddonList(), $"\"AddonList\"\n{volvo}");

            saveButton.Enabled = false;
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            Reload(false);
        }

        private void addonInfoButton_Click(object sender, EventArgs e)
        {
            Addon addon = AddonMgr.GetAddon(addonListBox.SelectedIndex);

            Utils.OpenFile($"{Utils.GetAddonFolder()}{addon.name}\\addoninfo.txt");
        }

        private void addonFolderButton_Click(object sender, EventArgs e)
        {
            Utils.OpenFolder(Utils.GetAddonFolder());
        }

        private void addonListButton_Click(object sender, EventArgs e)
        {
            Utils.OpenFile(Utils.GetAddonList());
        }

        private void containsListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //containsListBox.SetItemChecked(e.Index, e.CurrentValue == CheckState.Checked ? false : true);
        }

        // Finds unused folders automatically.
        private void FindUnusedFolders()
        {
            string[] directories;

            if (Directory.Exists(Utils.GetAddonFolder()))
            {
                directories = Directory.GetDirectories(Utils.GetAddonFolder());
            }
            else
            {
                throw new DirectoryNotFoundException($"The path '{Utils.GetAddonFolder()}' does not exist.");
            }

            for (int i = 0; i < directories.Length; i++)
            {
                directories[i] = directories[i].Replace(Utils.GetAddonFolder(), "");
            }

            VObject original = new VObject();

            int index = 0;
            for (int i = 0; i < AddonMgr.addons.Length; i++)
            {
                for (int j = 0; j < AddonMgr.addons.Length; j++)
                {
                    if (index == AddonMgr.addons[j].order)
                    {
                        VProperty temp = new VProperty(AddonMgr.addons[j].name, new VValue(AddonMgr.addons[j].enabled ? "1" : "0"));
                        original.Insert(index++, temp);
                    }
                }
            }

            bool bNewFolder = false;
            foreach (string directory in directories)
            {
                bool bFound = false;
                for (int i = 0; i < AddonMgr.addons.Length; i++)
                {
                    if (directory == AddonMgr.addons[i].name)
                    {
                        bFound = true;
                        break;
                    }
                }

                if (bFound)
                {
                    continue;
                }

                if (File.Exists($"{Utils.GetAddonFolder()}\\{directory}\\addoninfo.txt"))
                {
                    string fileContent = File.ReadAllText($"{Utils.GetAddonFolder()}\\{directory}\\addoninfo.txt");

                    // invalid
                    if (fileContent.Length <= 0)
                    {
                        continue;
                    }

                    VProperty temp = new VProperty(directory, new VValue("0"));
                    original.Insert(index++, temp);
                    bNewFolder = true;
                }
            }

            if (bNewFolder)
            {
                string volvo = VdfConvert.Serialize(original);
                File.WriteAllText(Utils.GetAddonList(), $"\"AddonList\"\n{volvo}");

                Reload(false);
            }
        }

        bool IsValidVPK(string path)
        {
            string pattern = @"^[a-zA-Z0-9._\\\-:/\s]+$";

            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            string[] invalid = new string[] { };

            //string invalid = "";
            //int vtx = 0;
            for (int i = 0; i < files.Length; i++)
            {
                string str = files[i].Replace(path, "");
                if (!Regex.IsMatch(str, pattern))
                {
                    invalid = invalid.Append<string>(str).ToArray();
                    //invalid = invalid + $"{str}\n";
                    //Console.WriteLine(files[i].Replace(path, ""));
                }
                else
                {
                    // L4D1 vpk is being an asshole
                    //if (str.EndsWith("dx80.vtx") || str.EndsWith("dx90.vtx") || str.EndsWith("sw.vtx"))
                    {
                        //invalid = invalid.Append<string>(str).ToArray();
                        //vtx++;
                    }
                }
            }

            if (invalid.Length > 0)
            {
                // anymore than this and I'll have a heartattack
                if (invalid.Length < 75)
                {
                    string messageFiles = "";
                    for (int i = 0; i < invalid.Length; i++)
                    {
                        messageFiles = messageFiles + invalid[i];
                    }

                    MessageBox.Show($"These files will crash vpk.exe:\n\n{messageFiles}");

                    return false;
                }
                else
                {
                    StreamWriter file = File.CreateText("p3_addonmgr_log.txt");
                    file.WriteLine($"{path}\n");
                    for (int i = 0; i < invalid.Length; i++)
                    {
                        file.WriteLine(invalid[i]);
                    }
                    file.Close();
                    MessageBox.Show($"These files will crash vpk.exe:\n\nFound \"{invalid.Length}\" files");
                    Utils.OpenFile("p3_addonmgr_log.txt");

                    return false;
                }
            }
            else
                MessageBox.Show($"The folder seems VPK-friendly.");

            return true;
        }

        private void vpkValidButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new()
            {
                ShowNewFolderButton = false
            };

            DialogResult result = folderDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            IsValidVPK(folderDlg.SelectedPath);
        }

        static string vpk_pakName = "pak01";

        private void buildVPKFolderMultiButton_Click()
        {
            FolderBrowserDialog folderDlg = new()
            {
                ShowNewFolderButton = false
            };

            DialogResult result = folderDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            if (!IsValidVPK(folderDlg.SelectedPath))
            {
                MessageBox.Show("Building VPK (multi) aborted.");
                return;
            }

            // Folders to look for and pack into the pak01 vpk set.
            string[] targetFolders = { "materials", "models", "resource", "media", "particles", "scripts", "maps", "expressions", "scenes", "sound" };

            // File types to look for in the aforementioned folders.
            string[] fileTypes = { "vmt", "vtf", "mdl", "phy", "vtx", "vvd", "ani", "pcf", "vcd", "txt", "res", "vfont", "cur", "dat", "bik", "mov", "bsp", "nav", "lst", "lmp", "vfe", "wav", "mp3" };

            // Script begins
            string currentDirectory = Directory.GetCurrentDirectory();

            // Path to vpk.exe
            string vpkPath = currentDirectory + "\\" + "p3_addonmgr\\vpk.exe";

            if (!File.Exists(vpkPath))
            {
                MessageBox.Show("p3_addonmgr/vpk.exe not found.\n\nBuilding VPK (multi) aborted.");
                return;
            }

            string responsePath = folderDlg.SelectedPath + "\\" + "vpk_list.txt";

            string msg = "Building the VPK begins now.\nThe program will appear frozen, do not close it until it shows \"Done.\".\n\nPress OK to continue.";
            string caption = "Building VPK V1 (multi)";

            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            using (StreamWriter outFile = new StreamWriter(responsePath))
            {
                foreach (string folder in targetFolders)
                {
                    string targetFolderPath = Path.Combine(folderDlg.SelectedPath, folder);

                    Console.WriteLine(targetFolderPath);

                    if (Directory.Exists(targetFolderPath))
                    {
                        // Recursively get all files in the folder and subfolders
                        foreach (string file in Directory.EnumerateFiles(targetFolderPath, "*.*", SearchOption.AllDirectories))
                        {
                            // Get file extension
                            string extension = Path.GetExtension(file).TrimStart('.').ToLower();

                            // Check if the file extension matches one of the desired types
                            if (fileTypes.Contains(extension))
                            {
                                string relativePath = file.Substring(folderDlg.SelectedPath.Length + 1).Replace("/", "\\");
                                outFile.WriteLine(relativePath);
                            }
                        }
                    }
                }
            }

            // The "pak01" here specifies the multi-chunk vpk names.
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = vpkPath,
                Arguments = $"-M a {vpk_pakName} @" + $"\"{responsePath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = folderDlg.SelectedPath
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit();

            MessageBox.Show("Done.\nYou can find the VPKs inside the chosen directory.");
        }

        private void buildVPKFolderMultiButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                buildVPKFolderMultiButton_Click();
            }
            else if (e.Button == MouseButtons.Right)
            {
                using (Prompt prompt = new Prompt($"Current: \"{vpk_pakName}\"", "Enter new name"))
                {
                    if (prompt.Result.Length > 0)
                    {
                        vpk_pakName = prompt.Result;

                        toolTip.SetToolTip(buildVPKFolderMultiButton, $"Builds a multi-pak VPK ({vpk_pakName}_dir)\n\nRight Click to change the name of the VPK");
                    }
                }
            }

        }

        private void buildVPKFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new()
            {
                ShowNewFolderButton = false
            };

            DialogResult result = folderDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            if (!IsValidVPK(folderDlg.SelectedPath))
            {
                MessageBox.Show("Building VPK (single) aborted.");
                return;
            }

            // Script begins
            string currentDirectory = Directory.GetCurrentDirectory();

            // Path to vpk.exe
            string vpkPath = currentDirectory + "\\" + "p3_addonmgr\\vpk.exe";

            if (!File.Exists(vpkPath))
            {
                MessageBox.Show("p3_addonmgr/vpk.exe not found.\n\nBuilding VPK (single) aborted.");
                return;
            }

            string msg = "Building the VPK begins now.\nThe program will appear frozen, do not close it until it shows \"Done.\".\n\nPress OK to continue.";
            string caption = "Building VPK V1 (single)";

            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            // The "pak01" here specifies the multi-chunk vpk names.
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = vpkPath,
                Arguments = $"\"{folderDlg.SelectedPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = folderDlg.SelectedPath
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit();

            MessageBox.Show("Done.\nYou can find the VPK outside of the chosen directory.");
        }
    }
}
