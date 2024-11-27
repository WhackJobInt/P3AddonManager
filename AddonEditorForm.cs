using Gameloop.Vdf.Linq;
using Gameloop.Vdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace P3AddonManager
{
    public partial class AddonEditorForm : Form
    {
        Form1 mainForm;

        public AddonEditorForm(Form1 main)
        {
            InitializeComponent();

            mainForm = main;
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            addonGroupBox = new GroupBox();
            selectVPKsButton = new Button();
            groupBox2 = new GroupBox();
            exitButton = new Button();
            saveButton = new Button();
            imageButton = new Button();
            addonPictureBox = new PictureBox();
            descriptionTextBox = new RichTextBox();
            minimumComboBox = new ComboBox();
            vpksTextBox = new TextBox();
            asTextBox = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label9 = new Label();
            label6 = new Label();
            p3sTextBox = new TextBox();
            label5 = new Label();
            linkTextBox = new TextBox();
            label4 = new Label();
            versionTextBox = new TextBox();
            label3 = new Label();
            titleTextBox = new TextBox();
            label2 = new Label();
            authorTextBox = new TextBox();
            addonGroupBox.SuspendLayout();
            groupBox2.SuspendLayout();
            ((ISupportInitialize)addonPictureBox).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Author:";
            // 
            // addonGroupBox
            // 
            addonGroupBox.Controls.Add(selectVPKsButton);
            addonGroupBox.Controls.Add(groupBox2);
            addonGroupBox.Controls.Add(descriptionTextBox);
            addonGroupBox.Controls.Add(minimumComboBox);
            addonGroupBox.Controls.Add(vpksTextBox);
            addonGroupBox.Controls.Add(asTextBox);
            addonGroupBox.Controls.Add(label8);
            addonGroupBox.Controls.Add(label7);
            addonGroupBox.Controls.Add(label9);
            addonGroupBox.Controls.Add(label6);
            addonGroupBox.Controls.Add(p3sTextBox);
            addonGroupBox.Controls.Add(label5);
            addonGroupBox.Controls.Add(linkTextBox);
            addonGroupBox.Controls.Add(label4);
            addonGroupBox.Controls.Add(versionTextBox);
            addonGroupBox.Controls.Add(label3);
            addonGroupBox.Controls.Add(titleTextBox);
            addonGroupBox.Controls.Add(label2);
            addonGroupBox.Controls.Add(authorTextBox);
            addonGroupBox.Controls.Add(label1);
            addonGroupBox.Location = new Point(12, 12);
            addonGroupBox.Name = "addonGroupBox";
            addonGroupBox.Size = new Size(871, 648);
            addonGroupBox.TabIndex = 1;
            addonGroupBox.TabStop = false;
            addonGroupBox.Text = "Selected Addon";
            // 
            // selectVPKsButton
            // 
            selectVPKsButton.Location = new Point(414, 212);
            selectVPKsButton.Name = "selectVPKsButton";
            selectVPKsButton.Size = new Size(92, 23);
            selectVPKsButton.TabIndex = 8;
            selectVPKsButton.Text = "Select VPKs ...";
            selectVPKsButton.UseVisualStyleBackColor = true;
            selectVPKsButton.Click += selectVPKs_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(exitButton);
            groupBox2.Controls.Add(saveButton);
            groupBox2.Controls.Add(imageButton);
            groupBox2.Controls.Add(addonPictureBox);
            groupBox2.Location = new Point(524, 21);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(341, 315);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Preview Image";
            // 
            // exitButton
            // 
            exitButton.Location = new Point(256, 268);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 37);
            exitButton.TabIndex = 8;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // saveButton
            // 
            saveButton.Enabled = false;
            saveButton.Location = new Point(11, 268);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 37);
            saveButton.TabIndex = 8;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // imageButton
            // 
            imageButton.Location = new Point(122, 282);
            imageButton.Name = "imageButton";
            imageButton.Size = new Size(103, 23);
            imageButton.TabIndex = 7;
            imageButton.Text = "Load Image ..";
            imageButton.UseVisualStyleBackColor = true;
            // 
            // addonPictureBox
            // 
            addonPictureBox.Image = Properties.Resources._default;
            addonPictureBox.Location = new Point(11, 22);
            addonPictureBox.Name = "addonPictureBox";
            addonPictureBox.Size = new Size(320, 240);
            addonPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            addonPictureBox.TabIndex = 6;
            addonPictureBox.TabStop = false;
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            descriptionTextBox.Location = new Point(6, 285);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            descriptionTextBox.Size = new Size(500, 357);
            descriptionTextBox.TabIndex = 2;
            descriptionTextBox.Text = "";
            descriptionTextBox.TextChanged += descriptionTextBox_TextChanged;
            // 
            // minimumComboBox
            // 
            minimumComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            minimumComboBox.Items.AddRange(new object[] { "invalid/none", "ZOOM/Steam (2023+)", "Ultrapatch Angel v1.1.0" });
            minimumComboBox.Location = new Point(149, 201);
            minimumComboBox.Name = "minimumComboBox";
            minimumComboBox.Size = new Size(179, 23);
            minimumComboBox.TabIndex = 2;
            minimumComboBox.SelectedIndexChanged += minimumComboBox_SelectedIndexChanged;
            minimumComboBox.SelectionChangeCommitted += minimumComboBox_SelectionChangeCommitted;
            // 
            // vpksTextBox
            // 
            vpksTextBox.Location = new Point(90, 236);
            vpksTextBox.Name = "vpksTextBox";
            vpksTextBox.ReadOnly = true;
            vpksTextBox.Size = new Size(416, 23);
            vpksTextBox.TabIndex = 1;
            vpksTextBox.TextChanged += asTextBox_TextChanged;
            // 
            // asTextBox
            // 
            asTextBox.Location = new Point(90, 166);
            asTextBox.Name = "asTextBox";
            asTextBox.Size = new Size(416, 23);
            asTextBox.TabIndex = 1;
            asTextBox.TextChanged += asTextBox_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(5, 267);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 0;
            label8.Text = "Description:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 204);
            label7.Name = "label7";
            label7.Size = new Size(138, 15);
            label7.TabIndex = 0;
            label7.Text = "Minimum Game Version:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 239);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 0;
            label9.Text = "VPKs:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 169);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 0;
            label6.Text = "AngelScript:";
            // 
            // p3sTextBox
            // 
            p3sTextBox.Location = new Point(90, 137);
            p3sTextBox.Name = "p3sTextBox";
            p3sTextBox.Size = new Size(416, 23);
            p3sTextBox.TabIndex = 1;
            p3sTextBox.TextChanged += p3sTextBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 140);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 0;
            label5.Text = "Postal3Script:";
            // 
            // linkTextBox
            // 
            linkTextBox.Location = new Point(90, 108);
            linkTextBox.Name = "linkTextBox";
            linkTextBox.Size = new Size(416, 23);
            linkTextBox.TabIndex = 1;
            linkTextBox.TextChanged += linkTextBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 111);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 0;
            label4.Text = "Link:";
            // 
            // versionTextBox
            // 
            versionTextBox.Location = new Point(90, 79);
            versionTextBox.Name = "versionTextBox";
            versionTextBox.Size = new Size(416, 23);
            versionTextBox.TabIndex = 1;
            versionTextBox.TextChanged += versionTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 82);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 0;
            label3.Text = "Version:";
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(90, 50);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(416, 23);
            titleTextBox.TabIndex = 1;
            titleTextBox.TextChanged += titleTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 53);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 0;
            label2.Text = "Title:";
            // 
            // authorTextBox
            // 
            authorTextBox.Location = new Point(90, 21);
            authorTextBox.Name = "authorTextBox";
            authorTextBox.Size = new Size(416, 23);
            authorTextBox.TabIndex = 1;
            authorTextBox.TextChanged += authorTextBox_TextChanged;
            // 
            // AddonEditorForm
            // 
            ClientSize = new Size(888, 668);
            ControlBox = false;
            Controls.Add(addonGroupBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "AddonEditorForm";
            ShowIcon = false;
            Text = "Addon Editor";
            addonGroupBox.ResumeLayout(false);
            addonGroupBox.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((ISupportInitialize)addonPictureBox).EndInit();
            ResumeLayout(false);
        }

        private Button exitButton;
        private Button saveButton;
        private Button selectVPKsButton;
        private TextBox vpksTextBox;
        private Label label9;
        Addon editedAddon;

        public void InsertAddonInfo(Addon addon)
        {
            this.Text = $"Addon Editor ({addon.name})";

            editedAddon = addon;

            authorTextBox.Text = addon.info.Author;
            titleTextBox.Text = addon.info.Title;
            versionTextBox.Text = addon.info.Version;
            linkTextBox.Text = addon.info.Link;
            p3sTextBox.Text = addon.info.Postal3Script;
            asTextBox.Text = addon.info.AngelScript;

            vpksTextBox.Text = "";

            for (int i = 0; i < addon.info.VPKs.Length; i++)
            {
                vpksTextBox.Text += $"{addon.info.VPKs[i]}";

                if ((i + 1) < addon.info.VPKs.Length)
                {
                    vpksTextBox.Text += ";";
                }
            }

            // TODO: version handling
            if (addon.info.bZOOM)
            {
                minimumComboBox.SelectedIndex = P3Hash.AddonIndex(P3Hash.P3Version.ZOOM);
            }
            else if (addon.info.bANGEL)
            {
                minimumComboBox.SelectedIndex = P3Hash.AddonIndex(P3Hash.P3Version.Angelv1_1_0);
            }
            else
            {
                minimumComboBox.SelectedIndex = P3Hash.AddonIndex(P3Hash.P3Version.ADDON_SUPPORT);
            }

            descriptionTextBox.Text = addon.info.Description;

            if (addon.Image != null)
            {
                addonPictureBox.Image = addon.Image;
            }

            saveButton.Enabled = false;
        }

        private void minimumComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ZOOM doesn't have support for injecting Postal3Script... nor do they have AngelScript (+base UP)
            // TODO: magic numbers begone
            if (minimumComboBox.SelectedIndex < P3Hash.AddonIndex(P3Hash.P3Version.Angelv1_1_0))
            {
                p3sTextBox.ForeColor = Color.Red;
                asTextBox.ForeColor = Color.Red;
            }
            else
            {
                p3sTextBox.ForeColor = Color.Black;
                asTextBox.ForeColor = Color.Black;
            }

            UpdateText();
        }

        private Label label1;
        private GroupBox addonGroupBox;
        private TextBox versionTextBox;
        private Label label3;
        private TextBox titleTextBox;
        private Label label2;
        private TextBox linkTextBox;
        private Label label4;
        private TextBox asTextBox;
        private Label label6;
        private TextBox p3sTextBox;
        private Label label5;
        private ComboBox minimumComboBox;
        private Label label7;
        public RichTextBox descriptionTextBox;
        private Label label8;
        private PictureBox addonPictureBox;
        private GroupBox groupBox2;
        private Button imageButton;
        private TextBox authorTextBox;

        private void minimumComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // ZOOM doesn't have support for injecting Postal3Script... nor do they have AngelScript
            if (minimumComboBox.SelectedIndex < P3Hash.AddonIndex(P3Hash.P3Version.Angelv1_1_0))
            {
                p3sTextBox.BackColor = Color.Red;
                asTextBox.BackColor = Color.Red;
                vpksTextBox.BackColor = Color.Red;
            }
            else
            {
                p3sTextBox.BackColor = Color.White;
                asTextBox.BackColor = Color.White;
                vpksTextBox.BackColor = Color.FromArgb(255, 240, 240, 240);
            }

            UpdateText();
        }

        void UpdateText()
        {
            addonGroupBox.Text = $"Selected Addon [{titleTextBox.Text} {versionTextBox.Text} by {authorTextBox.Text}] (-1)";
            saveButton.Enabled = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Overwrite the addoninfo...
            VObject addoninfo = new VObject();

            Utils.AddToVDF(addoninfo, "addontitle", titleTextBox.Text);
            Utils.AddToVDF(addoninfo, "addonauthor", authorTextBox.Text);
            Utils.AddToVDF(addoninfo, "addonversion", versionTextBox.Text);
            Utils.AddToVDF(addoninfo, "addondescription", descriptionTextBox.Text);
            Utils.AddToVDF(addoninfo, "addonlink", linkTextBox.Text);

            Utils.AddToVDF(addoninfo, "postal3script", p3sTextBox.Text);
            Utils.AddToVDF(addoninfo, "angelscript", asTextBox.Text);

            // TODO: SURELY there's a BETTER way to do this?? kill me
            string minimum = "";
            switch (minimumComboBox.SelectedIndex)
            {
                default:
                case 0: minimum = ""; break;
                case 1: minimum = "zoom"; break;
                case 2: minimum = "angelv1.1.0"; break;
            }

            Utils.AddToVDF(addoninfo, "addonminimum", minimum);

            Utils.AddToVDF(addoninfo, "addonvpks", vpksTextBox.Text);

            string data = VdfConvert.Serialize(addoninfo);
            File.WriteAllText($"{Utils.GetAddonFolder()}{editedAddon.name}\\addoninfo.txt", $"\"AddonInfo\"\n{data}");

            saveButton.Enabled = false;

            editedAddon.info.Title = titleTextBox.Text;
            editedAddon.info.Author = authorTextBox.Text;
            editedAddon.info.Version = versionTextBox.Text;
            editedAddon.info.Description = descriptionTextBox.Text;
            editedAddon.info.Link = linkTextBox.Text;

            editedAddon.info.Postal3Script = p3sTextBox.Text;
            editedAddon.info.AngelScript = asTextBox.Text;
            editedAddon.info.Minimum = minimum;
            editedAddon.info.CheckMinimum();

            mainForm.UpdateUI();

            mainForm.Reload(true);

            MessageBox.Show("Addon's info saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void authorTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void versionTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void linkTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void p3sTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void asTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void selectVPKs_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new()
            {
                InitialDirectory = $"{Utils.GetAddonFolder()}{editedAddon.name}",
                Title = "Choose one or multiple VPKs.",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true,
                DefaultExt = ".vpk",
                Filter = "Valve Pak (V1) files (*.vpk)|*.vpk",
            };

            DialogResult result = fileDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            if (fileDlg.FileNames.Length <= 0)
            {
                return;
            }

            // Check if someone is trying to make the game vomit
            for (int i = 0; i < fileDlg.FileNames.Length; i++)
            {
                string file = fileDlg.FileNames[i].Replace(fileDlg.InitialDirectory + "\\", "");
                if (file == fileDlg.FileNames[i])
                {
                    MessageBox.Show("Make sure to stay within the addon's root folder", "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                file = file.Replace(".vpk", "");

                // wtf?
                if (file.Length < 3)
                   continue;

                string L = String.Format($"{file[file.Length - 1]}");
                string L1 = String.Format($"{file[file.Length - 2]}");
                string L2 = String.Format($"{file[file.Length - 3]}");

                //Console.WriteLine($"{L2}{L1}{L}");

                int temp = -1;

                if (int.TryParse(L, out temp) &&
                    int.TryParse(L1, out temp) &&
                    int.TryParse(L2, out temp))
                {
                    MessageBox.Show("VPK Part detected!\nMake sure to choose _dir instead!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            vpksTextBox.Text = "";

            for (int i = 0; i < fileDlg.FileNames.Length; i++)
            {
                string file = fileDlg.FileNames[i].Replace(fileDlg.InitialDirectory + "\\", "").Replace(".vpk", "");

                vpksTextBox.Text += $"{file}";

                if ((i + 1) < fileDlg.FileNames.Length)
                {
                    vpksTextBox.Text += ";";
                }
            }
        }
    }
}
