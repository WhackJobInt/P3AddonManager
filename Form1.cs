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
using static System.Windows.Forms.Design.AxImporter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace P3AddonManager
{
    public partial class Form1 : Form
    {
        AddonEditorForm addonEditor = null;

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
            //shaders,
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
                ini.Write("Game", "p3", "Main");
                return true;
            }

            return false;
        }

        void Reload(bool newRun = true)
        {
            addonPictureBox.Image = addonPictureBox.InitialImage;
            conflictTextBox.Text = "Conflicts with: n/a";
            conflictTextBox.ForeColor = Color.Olive;
            addonLinkLabel.Text = "n/a";
            minimumVersionDisplayLabel.Text = "n/a";
            selectedAddonGroupBox.Text = "Selected Addon [UNKNOWN] (-1)";
            descriptionTextBox.Text = "n/a";
            statusLabel.Text = "[UNKNOWN]";
            statusLabel.ForeColor = Color.Olive;
            containsListBox.SetItemChecked((int)AddonContains.cfg, false);
            containsListBox.SetItemChecked((int)AddonContains.maps, false);
            containsListBox.SetItemChecked((int)AddonContains.materials, false);
            containsListBox.SetItemChecked((int)AddonContains.media, false);
            containsListBox.SetItemChecked((int)AddonContains.models, false);
            containsListBox.SetItemChecked((int)AddonContains.particles, false);
            containsListBox.SetItemChecked((int)AddonContains.resource, false);
            containsListBox.SetItemChecked((int)AddonContains.scripts, false);
            containsListBox.SetItemChecked((int)AddonContains.sound, false);
            containsListBox.SetItemChecked((int)AddonContains.vpk, false);
            containsListBox.SetItemChecked((int)AddonContains.p3s, false);
            containsListBox.SetItemChecked((int)AddonContains.ans, false);

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
            saveCurrentSettingsToolStripMenuItem.Enabled = false;

            // Will only force reload if it finds a completely new directory
            FindUnusedFolders();

            string GameVer = $"The currently detected version of your Postal III installation.\r\n\r\nRed = Addons will not work for this Postal III\r\nOlive = Addons might or not work, make sure you have ZOOM or Ultrapatch\r\nGreen = Addons will work\n\nC:{P3Hash.Current_Client}\nS:{P3Hash.Current_Server}\nE:{P3Hash.Current_Exe}";

            toolTip.SetToolTip(gameVersionLabel, GameVer);

            changeGamePathp3ToolStripMenuItem.Text = $"Change game path ({Utils.GetGameFolder()})";
            addonBox.Text = $"Addons ({Utils.GetGameFolder()})";
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
                    ini.Write("Game", "p3", "Main");
                    ini.Write("Cmd", "-novid +sv_cheats 0", "Main");
                    exeInRoot = true;
                }
                else
                {
                    // Check for CR
                    exe = $"{Utils.GetExeLocation()}\\cr_base.exe";
                    if (File.Exists(exe))
                    {
                        ini.Write("Path", Utils.GetExeLocation() + "\\", "Main");
                        ini.Write("Game", "cr_base", "Main");
                        ini.Write("Cmd", "-novid +sv_cheats 0", "Main");
                        exeInRoot = true;
                    }
                }

                if (!exeInRoot)
                {
                    string msg = "No Postal III exe has been found.\nPlease select folder with \"p3.exe\" in it.";
                    string caption = "Information";

                    DialogResult res = MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    if (res == DialogResult.OK)
                    {
                        bool bResult = false;
                        while (!bResult)
                        {
                            bResult = PathLooping(ini);
                        }
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        Close();
                    }

                }
            }

            // If this doesn't exist for some reason, then it'll always be p3
            if (ini.KeyExists("Game", "Main"))
            {
                Utils.GameFolder = ini.Read("Game", "Main");
                //Utils.RootFolder = ini.Read("Path", "Main");
            }

            Utils.SetExePath(ini.Read("Path", "Main"));

            InitializeComponent();
            addonPictureBox.InitialImage = addonPictureBox.Image;

            addonListBox.AllowDrop = false;

            containsListBox.CheckOnClick = false;
            containsListBox.SelectionMode = SelectionMode.None;

            if (ini.KeyExists("Cmd", "Main"))
            {
                cmdTextBox.Text = ini.Read("Cmd", "Main");
            }

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

        void RearrangeAddonList(int index)
        {
            bool st = (addonListBox.GetItemChecked(addonListBox.SelectedIndex));

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

        private void addonListBox_DragDrop(object sender, DragEventArgs e)
        {
            if (!addonListBox.AllowDrop) return;

            Point point = addonListBox.PointToClient(new Point(e.X, e.Y));
            int index = addonListBox.IndexFromPoint(point);

            RearrangeAddonList(index);
        }

        public void UpdateUI(int index = -1)
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

            minimumVersionDisplayLabel.Text = $"{addon.info.Minimum}";

            Utils.ConvertVersionToLabel(minimumVersionDisplayLabel, addon);

            containsListBox.SetItemChecked((int)AddonContains.cfg, addon.has.cfg);
            containsListBox.SetItemChecked((int)AddonContains.maps, addon.has.map);
            containsListBox.SetItemChecked((int)AddonContains.materials, addon.has.material);
            containsListBox.SetItemChecked((int)AddonContains.media, addon.has.media);
            containsListBox.SetItemChecked((int)AddonContains.models, addon.has.model);
            containsListBox.SetItemChecked((int)AddonContains.particles, addon.has.particle);
            containsListBox.SetItemChecked((int)AddonContains.resource, addon.has.resource);
            containsListBox.SetItemChecked((int)AddonContains.scripts, addon.has.script);
            //containsListBox.SetItemChecked((int)AddonContains.shaders, addon.has.shader);
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
            saveCurrentSettingsToolStripMenuItem.Enabled = true;

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
                Directory.CreateDirectory(Utils.GetAddonFolder());
                directories = Directory.GetDirectories(Utils.GetAddonFolder());

                //throw new DirectoryNotFoundException($"The path '{Utils.GetAddonFolder()}' does not exist.");
            }

            if (directories.Length <= 0)
            {
                MessageBox.Show("No addons found.", "No addons", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        static string vpk_pakName = "pak01";

        private void upArrowButton_Click(object sender, EventArgs e)
        {
            int index = addonListBox.SelectedIndex;

            if (index != 0)
            {
                index--;
            }

            RearrangeAddonList(index);
        }

        private void downArrowButton_Click(object sender, EventArgs e)
        {
            int index = addonListBox.SelectedIndex;

            if (index != addonListBox.Items.Count - 1)
            {
                index++;
            }

            RearrangeAddonList(index);
        }

        private void multiToolStripMenuItem_Click(object sender, EventArgs e)
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

                    //Console.WriteLine(targetFolderPath);

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

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void vPKValidCheckingFolderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void addonsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.OpenFolder(Utils.GetAddonFolder());
        }

        private void addoninfotxtactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Addon addon = AddonMgr.GetAddon(addonListBox.SelectedIndex);

            Utils.OpenFile($"{Utils.GetAddonFolder()}{addon.name}\\addoninfo.txt");
        }

        private void addonlisttxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.OpenFile(Utils.GetAddonList());
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Help";

            string msg = "";

            msg += "Welcome to Postal III Addon Manager.\n\n";

            msg += "You can manage currently installed addons (usually inside p3/addons/) ";
            msg += "with this program.\n";
            msg += "It can also help with making your own addon. (built-in VPK tools, description editor, etc..)\n\n";

            msg += "You can hover over certain items in the program to view their tips.\n\n";

            msg += "It's worth noting this program will not work with retail copies of Postal III, and only ZOOM, Steam (since 2023), and Ultrapatch(+Angel) will work.\n\n";

            msg += "The \"Detected Version\" which the program displays should be very accurate, however the minimum version the addon recommends might not be correct at all times, so it is highly recommended to read its description.\n";
            msg += "If you are using Linux, this feature of the program will not work properly.\n\n";

            msg += "The order of addons is very important, you can change the order by right clicking on an addon on the right, and dragging it to a spot to your liking.\nYou can also just use the Up and Down buttons.\n";
            msg += "Conflicts box will tell you if addons are modifying same files, so you will be able to deduce which addon is more important in the order list.";

            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveCurrentSettingsToolStripMenuItem_Click(object sender, EventArgs e)
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

            saveCurrentSettingsToolStripMenuItem.Enabled = false;
            saveButton.Enabled = false;

            var ini = new IniFile("p3_addonmgr.ini");
            ini.Write("Cmd", cmdTextBox.Text, "Main");
        }

        private void reloadResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload(false);
        }

        private void changeExePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ini = new IniFile("p3_addonmgr.ini");
            if (!PathLooping(ini))
            {
                string msg = "No Postal III exe has been found.\nPlease select folder with \"p3.exe\" in it.";
                string caption = "Information";

                DialogResult res = MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void changeGamePathp3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new()
            {
                ShowNewFolderButton = false
            };

            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newGameFolder = folderDlg.SelectedPath.Replace(Utils.GetExePath(), "");

                if (newGameFolder == Utils.GetGameFolder())
                {
                    MessageBox.Show($"Game folder is already \"{newGameFolder}\".", "Change Game Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult res = MessageBox.Show($"The selected game folder is \"{newGameFolder}\", are you sure?", "Change Game Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    Utils.GameFolder = newGameFolder;
                    var ini = new IniFile("p3_addonmgr.ini");
                    ini.Write("Game", newGameFolder, "Main");

                    MessageBox.Show($"Game folder is now \"{newGameFolder}\", program restarts..");
                    Reload(true);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buildVPKFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePakNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Prompt prompt = new Prompt($"Current: \"{vpk_pakName}\"", "Enter new name"))
            {
                if (prompt.Result.Length > 0)
                {
                    vpk_pakName = prompt.Result;

                    //toolTip.SetToolTip(buildVPKFolderMultiButton, $"Builds a multi-pak VPK ({vpk_pakName}_dir)\n\nRight Click to change the name of the VPK");
                }
            }
        }
        private void descriptionEditorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (addonEditor == null || addonEditor.IsDisposed)
            {
                addonEditor = new AddonEditorForm(this);
            }

            addonEditor.Show();
            addonEditor.InsertAddonInfo(AddonMgr.GetAddon(addonListBox.SelectedIndex));
        }

        private void newAddonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Prompt prompt = new Prompt($"Enter a (folder) name for the addon.", "New Addon Creation"))
            {
                if (prompt.Result.Length > 0)
                {
                    bool bCreate = false;

                    if (Directory.Exists($"{Utils.GetAddonFolder()}\\{prompt.Result}"))
                    {
                        if (!File.Exists($"{Utils.GetAddonFolder()}\\{prompt.Result}\\addoninfo.txt"))
                        {
                            // Maybe.... maybe not
                            bCreate = true;
                        }
                    }
                    else
                    {
                        bCreate = true;
                    }

                    if (bCreate)
                    {
                        Directory.CreateDirectory($"{Utils.GetAddonFolder()}\\{prompt.Result}");

                        // Overwrite the addoninfo...
                        VObject addoninfo = new VObject();

                        Utils.AddToVDF(addoninfo, "addontitle", "Unnamed Addon");
                        Utils.AddToVDF(addoninfo, "addonauthor", "Anonymous");
                        Utils.AddToVDF(addoninfo, "addonversion", "v1.0");
                        Utils.AddToVDF(addoninfo, "addondescription", "No description.");
                        Utils.AddToVDF(addoninfo, "addonlink", "https://www.youtube.com/watch?v=dQw4w9WgXcQ");

                        Utils.AddToVDF(addoninfo, "postal3script", "scripts/Postal3Script/ai_scripts.txt");
                        Utils.AddToVDF(addoninfo, "angelscript", "scripts/AngelScript/!as_scripts.as");

                        Utils.AddToVDF(addoninfo, "addonminimum", "zoom");

                        string data = VdfConvert.Serialize(addoninfo);
                        File.WriteAllText($"{Utils.GetAddonFolder()}\\{prompt.Result}\\addoninfo.txt", $"\"AddonInfo\"\n{data}");

                        Reload(true);

                        Addon newAddon = AddonMgr.GetAddonByName(prompt.Result);
                        if (addonEditor == null || addonEditor.IsDisposed)
                        {
                            addonEditor = new AddonEditorForm(this);
                        }

                        addonEditor.Show();
                        addonEditor.InsertAddonInfo(newAddon);
                    }
                    else
                    {
                        MessageBox.Show("An addon with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //toolTip.SetToolTip(buildVPKFolderMultiButton, $"Builds a multi-pak VPK ({vpk_pakName}_dir)\n\nRight Click to change the name of the VPK");
                }
            }
        }

        private static void CopyAllFromAtoB(string source, string target)
        {
            foreach (string dir in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dir.Replace(source, target));
            }

            foreach (string file in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(file, file.Replace(source, target), true);
            }
        }

        private void cloneSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Addon addon = AddonMgr.GetAddon(addonListBox.SelectedIndex);

            using (Prompt prompt = new Prompt($"Original's name: \"{addon.name}\"", "Clone Creation"))
            {
                if (prompt.Result.Length > 0)
                {
                    if (!Directory.Exists($"{Utils.GetAddonFolder()}\\{prompt.Result}"))
                    {
                        Directory.CreateDirectory($"{Utils.GetAddonFolder()}\\{prompt.Result}");

                        CopyAllFromAtoB($"{Utils.GetAddonFolder()}\\{addon.name}", $"{Utils.GetAddonFolder()}\\{prompt.Result}");

                        Reload(true);

                        Addon cloneAddon = AddonMgr.GetAddonByName(prompt.Result);
                        if (addonEditor == null || addonEditor.IsDisposed)
                        {
                            addonEditor = new AddonEditorForm(this);
                        }

                        addonEditor.Show();
                        addonEditor.InsertAddonInfo(cloneAddon);
                    }
                    else
                    {
                        MessageBox.Show("Folder already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveCurrentSettingsToolStripMenuItem_Click(sender, e);
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            reloadResetToolStripMenuItem_Click(sender, e);
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            // Possible exes to run the game with - Kizoky
            string exeCR = Utils.GetExePath() + "\\cr_base.exe";
            string exeP3 = Utils.GetExePath() + "\\p3.exe";

            if (!File.Exists(exeCR))
            {
                if (!File.Exists(exeP3))
                {
                    MessageBox.Show("Postal III executable was not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    exeCR = exeP3;
                }
            }

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = exeCR;
                p.StartInfo.Arguments = $"-game {Utils.GetGameFolder()} {cmdTextBox.Text}";
                p.Start();
            }
            catch
            {
                MessageBox.Show("Couldn't start Postal III!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdTextBox_TextChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = true;
            saveCurrentSettingsToolStripMenuItem.Enabled = true;
        }
    }
}
