namespace P3AddonManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            addonBox = new GroupBox();
            addonListBox = new CheckedListBox();
            selectedAddonGroupBox = new GroupBox();
            conflictTextBox = new RichTextBox();
            statusLabel = new Label();
            descriptionGroupBox = new GroupBox();
            descriptionTextBox = new RichTextBox();
            versionGroupBox = new GroupBox();
            minimumVersionDisplayLabel = new Label();
            addonPictureBox = new PictureBox();
            groupBox2 = new GroupBox();
            containsListBox = new CheckedListBox();
            addonLinkLabel = new LinkLabel();
            saveButton = new Button();
            actionGroupBox = new GroupBox();
            addonInfoButton = new Button();
            addonFolderButton = new Button();
            addonListButton = new Button();
            reloadButton = new Button();
            buildVPKFolderMultiButton = new Button();
            buildVPKFolderButton = new Button();
            vpkValidButton = new Button();
            gameVersionLabel = new Label();
            toolTip = new ToolTip(components);
            toolTipConflict = new ToolTip(components);
            expertGroupBox = new GroupBox();
            addonBox.SuspendLayout();
            selectedAddonGroupBox.SuspendLayout();
            descriptionGroupBox.SuspendLayout();
            versionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)addonPictureBox).BeginInit();
            groupBox2.SuspendLayout();
            actionGroupBox.SuspendLayout();
            expertGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // addonBox
            // 
            addonBox.Controls.Add(addonListBox);
            addonBox.Location = new Point(490, 30);
            addonBox.Name = "addonBox";
            addonBox.Size = new Size(200, 377);
            addonBox.TabIndex = 0;
            addonBox.TabStop = false;
            addonBox.Text = "Addons (p3)";
            toolTip.SetToolTip(addonBox, "The addons loaded from addonlist.txt\r\n\r\nPress and hold down the Right Mouse button to drag addons to change their load order.");
            // 
            // addonListBox
            // 
            addonListBox.FormattingEnabled = true;
            addonListBox.Items.AddRange(new object[] { "(1)Test", "(2)Test2", "(3)Test3", "(4)Skibidi Rizz", "(5)Main Menu Example", "(6)Very Long Mod Name To Fuck With Me", "(7)Carrot Dude", "(8)Malfunction", "(9)Better AI", "(10)Better Maps" });
            addonListBox.Location = new Point(6, 18);
            addonListBox.Name = "addonListBox";
            addonListBox.ScrollAlwaysVisible = true;
            addonListBox.Size = new Size(188, 346);
            addonListBox.TabIndex = 1;
            addonListBox.ItemCheck += addonListBox_ItemCheck;
            addonListBox.SelectedIndexChanged += addonListBox_SelectedIndexChanged;
            addonListBox.DragDrop += addonListBox_DragDrop;
            addonListBox.DragOver += addonListBox_DragOver;
            addonListBox.MouseDown += addonListBox_MouseDown;
            // 
            // selectedAddonGroupBox
            // 
            selectedAddonGroupBox.Controls.Add(conflictTextBox);
            selectedAddonGroupBox.Controls.Add(statusLabel);
            selectedAddonGroupBox.Controls.Add(descriptionGroupBox);
            selectedAddonGroupBox.Controls.Add(versionGroupBox);
            selectedAddonGroupBox.Controls.Add(addonPictureBox);
            selectedAddonGroupBox.Controls.Add(groupBox2);
            selectedAddonGroupBox.Controls.Add(addonLinkLabel);
            selectedAddonGroupBox.Location = new Point(12, 12);
            selectedAddonGroupBox.Name = "selectedAddonGroupBox";
            selectedAddonGroupBox.Size = new Size(472, 677);
            selectedAddonGroupBox.TabIndex = 1;
            selectedAddonGroupBox.TabStop = false;
            selectedAddonGroupBox.Text = "Selected Addon [Test 1.0 by Kizoky] (-1)";
            toolTip.SetToolTip(selectedAddonGroupBox, "The currently selected addon.\r\n\r\nThe number encapsulated in () symbols is the load order.");
            // 
            // conflictTextBox
            // 
            conflictTextBox.BackColor = Color.White;
            conflictTextBox.BorderStyle = BorderStyle.FixedSingle;
            conflictTextBox.ForeColor = Color.ForestGreen;
            conflictTextBox.Location = new Point(6, 279);
            conflictTextBox.Name = "conflictTextBox";
            conflictTextBox.ReadOnly = true;
            conflictTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            conflictTextBox.Size = new Size(449, 47);
            conflictTextBox.TabIndex = 12;
            conflictTextBox.Text = "Conflicts with: n/a";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.BackColor = Color.Transparent;
            statusLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            statusLabel.ForeColor = Color.Red;
            statusLabel.Location = new Point(359, 221);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(71, 15);
            statusLabel.TabIndex = 10;
            statusLabel.Text = "[DISABLED]";
            toolTip.SetToolTip(statusLabel, "Whether the addon is disabled or enabled. If it's disabled the addon will not appear in-game.");
            // 
            // descriptionGroupBox
            // 
            descriptionGroupBox.Controls.Add(descriptionTextBox);
            descriptionGroupBox.Location = new Point(6, 350);
            descriptionGroupBox.Name = "descriptionGroupBox";
            descriptionGroupBox.Size = new Size(460, 321);
            descriptionGroupBox.TabIndex = 9;
            descriptionGroupBox.TabStop = false;
            descriptionGroupBox.Text = "Description";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Dock = DockStyle.Fill;
            descriptionTextBox.Location = new Point(3, 19);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ReadOnly = true;
            descriptionTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            descriptionTextBox.Size = new Size(454, 299);
            descriptionTextBox.TabIndex = 0;
            descriptionTextBox.Text = "";
            toolTip.SetToolTip(descriptionTextBox, "The description of the addon.");
            // 
            // versionGroupBox
            // 
            versionGroupBox.Controls.Add(minimumVersionDisplayLabel);
            versionGroupBox.Location = new Point(332, 238);
            versionGroupBox.Name = "versionGroupBox";
            versionGroupBox.Size = new Size(132, 39);
            versionGroupBox.TabIndex = 8;
            versionGroupBox.TabStop = false;
            versionGroupBox.Text = "Minimum Version";
            // 
            // minimumVersionDisplayLabel
            // 
            minimumVersionDisplayLabel.AutoSize = true;
            minimumVersionDisplayLabel.BorderStyle = BorderStyle.Fixed3D;
            minimumVersionDisplayLabel.ForeColor = Color.Red;
            minimumVersionDisplayLabel.Location = new Point(8, 17);
            minimumVersionDisplayLabel.Name = "minimumVersionDisplayLabel";
            minimumVersionDisplayLabel.Size = new Size(27, 17);
            minimumVersionDisplayLabel.TabIndex = 7;
            minimumVersionDisplayLabel.Text = "n/a";
            toolTip.SetToolTip(minimumVersionDisplayLabel, "The minimum version required to run this addon properly.\r\n\r\nRed = Addon will not work\r\nOlive = Addon might or not work\r\nGreen = Addon will work");
            // 
            // addonPictureBox
            // 
            addonPictureBox.Image = Properties.Resources._default;
            addonPictureBox.Location = new Point(6, 35);
            addonPictureBox.Name = "addonPictureBox";
            addonPictureBox.Size = new Size(320, 240);
            addonPictureBox.TabIndex = 5;
            addonPictureBox.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(containsListBox);
            groupBox2.Location = new Point(332, 27);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(132, 193);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Contains";
            toolTip.SetToolTip(groupBox2, "The features the currently selected addon contains.");
            // 
            // containsListBox
            // 
            containsListBox.FormattingEnabled = true;
            containsListBox.Items.AddRange(new object[] { "Cfg", "Map", "Material", "Media", "Model", "Particle", "Resource", "Script", "Shader", "Sound", "VPK", "Postal3Script", "AngelScript" });
            containsListBox.Location = new Point(6, 22);
            containsListBox.Name = "containsListBox";
            containsListBox.Size = new Size(120, 166);
            containsListBox.TabIndex = 0;
            containsListBox.ItemCheck += containsListBox_ItemCheck;
            // 
            // addonLinkLabel
            // 
            addonLinkLabel.AutoSize = true;
            addonLinkLabel.BorderStyle = BorderStyle.Fixed3D;
            addonLinkLabel.Location = new Point(6, 331);
            addonLinkLabel.Name = "addonLinkLabel";
            addonLinkLabel.Size = new Size(513, 17);
            addonLinkLabel.TabIndex = 2;
            addonLinkLabel.TabStop = true;
            addonLinkLabel.Text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            toolTip.SetToolTip(addonLinkLabel, "The link of the addon.");
            addonLinkLabel.LinkClicked += addonLinkLabel_LinkClicked;
            // 
            // saveButton
            // 
            saveButton.Enabled = false;
            saveButton.Location = new Point(117, 114);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(77, 32);
            saveButton.TabIndex = 3;
            saveButton.Text = "Save";
            toolTip.SetToolTip(saveButton, "Saves current settings to addonlist.txt");
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // actionGroupBox
            // 
            actionGroupBox.Controls.Add(addonInfoButton);
            actionGroupBox.Controls.Add(addonFolderButton);
            actionGroupBox.Controls.Add(addonListButton);
            actionGroupBox.Controls.Add(reloadButton);
            actionGroupBox.Controls.Add(saveButton);
            actionGroupBox.Location = new Point(490, 413);
            actionGroupBox.Name = "actionGroupBox";
            actionGroupBox.Size = new Size(200, 157);
            actionGroupBox.TabIndex = 4;
            actionGroupBox.TabStop = false;
            actionGroupBox.Text = "Main";
            // 
            // addonInfoButton
            // 
            addonInfoButton.Location = new Point(6, 60);
            addonInfoButton.Name = "addonInfoButton";
            addonInfoButton.Size = new Size(188, 23);
            addonInfoButton.TabIndex = 9;
            addonInfoButton.Text = "Open addoninfo.txt";
            toolTip.SetToolTip(addonInfoButton, "Opens current selected addon's addoninfo.txt in a text editor.");
            addonInfoButton.UseVisualStyleBackColor = true;
            addonInfoButton.Click += addonInfoButton_Click;
            // 
            // addonFolderButton
            // 
            addonFolderButton.Location = new Point(6, 19);
            addonFolderButton.Name = "addonFolderButton";
            addonFolderButton.Size = new Size(188, 37);
            addonFolderButton.TabIndex = 8;
            addonFolderButton.Text = "Open addons Folder";
            toolTip.SetToolTip(addonFolderButton, "Opens the addons folder in explorer.");
            addonFolderButton.UseVisualStyleBackColor = true;
            addonFolderButton.Click += addonFolderButton_Click;
            // 
            // addonListButton
            // 
            addonListButton.Location = new Point(6, 87);
            addonListButton.Name = "addonListButton";
            addonListButton.Size = new Size(188, 23);
            addonListButton.TabIndex = 6;
            addonListButton.Text = "Open addonlist.txt";
            toolTip.SetToolTip(addonListButton, "Opens the addonlist.txt in a text editor.");
            addonListButton.UseVisualStyleBackColor = true;
            addonListButton.Click += addonListButton_Click;
            // 
            // reloadButton
            // 
            reloadButton.Location = new Point(6, 114);
            reloadButton.Name = "reloadButton";
            reloadButton.Size = new Size(75, 32);
            reloadButton.TabIndex = 5;
            reloadButton.Text = "Reload";
            toolTip.SetToolTip(reloadButton, "Reloads the whole program as if you just relaunched it.");
            reloadButton.UseVisualStyleBackColor = true;
            reloadButton.Click += reloadButton_Click;
            // 
            // buildVPKFolderMultiButton
            // 
            buildVPKFolderMultiButton.Location = new Point(6, 78);
            buildVPKFolderMultiButton.Name = "buildVPKFolderMultiButton";
            buildVPKFolderMultiButton.Size = new Size(186, 23);
            buildVPKFolderMultiButton.TabIndex = 11;
            buildVPKFolderMultiButton.Text = "Build VPK from Folder (Multi)";
            toolTip.SetToolTip(buildVPKFolderMultiButton, "Builds a multi-pak VPK (pak01_dir)\r\n\r\nRight Click to change the name of the pak");
            buildVPKFolderMultiButton.UseVisualStyleBackColor = true;
            buildVPKFolderMultiButton.MouseUp += buildVPKFolderMultiButton_MouseUp;
            // 
            // buildVPKFolderButton
            // 
            buildVPKFolderButton.Location = new Point(6, 49);
            buildVPKFolderButton.Name = "buildVPKFolderButton";
            buildVPKFolderButton.Size = new Size(186, 23);
            buildVPKFolderButton.TabIndex = 11;
            buildVPKFolderButton.Text = "Build VPK from Folder (Single)";
            toolTip.SetToolTip(buildVPKFolderButton, "Builds a single VPK file (ex.: myaddonname.vpk)\r\n\r\nMake sure the directory does not exceed 200mb.\r\n\r\nMainly used for .nav files which cannot be\r\noutside of VPK.");
            buildVPKFolderButton.UseVisualStyleBackColor = true;
            buildVPKFolderButton.Click += buildVPKFolderButton_Click;
            // 
            // vpkValidButton
            // 
            vpkValidButton.Location = new Point(6, 21);
            vpkValidButton.Name = "vpkValidButton";
            vpkValidButton.Size = new Size(186, 23);
            vpkValidButton.TabIndex = 10;
            vpkValidButton.Text = "VPK Valid Checking (Folder)";
            toolTip.SetToolTip(vpkValidButton, "Checks if a specified folder contains any file \r\nthat has illegal (unicode) character, which would crash vpk.exe");
            vpkValidButton.UseVisualStyleBackColor = true;
            vpkValidButton.Click += vpkValidButton_Click;
            // 
            // gameVersionLabel
            // 
            gameVersionLabel.AutoSize = true;
            gameVersionLabel.BorderStyle = BorderStyle.Fixed3D;
            gameVersionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            gameVersionLabel.ForeColor = Color.Green;
            gameVersionLabel.Location = new Point(492, 5);
            gameVersionLabel.Name = "gameVersionLabel";
            gameVersionLabel.Size = new Size(141, 17);
            gameVersionLabel.TabIndex = 5;
            gameVersionLabel.Text = "Detected Version: ZOOM";
            toolTip.SetToolTip(gameVersionLabel, resources.GetString("gameVersionLabel.ToolTip"));
            // 
            // toolTip
            // 
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ToolTipTitle = "Help";
            // 
            // toolTipConflict
            // 
            toolTipConflict.AutomaticDelay = 50;
            toolTipConflict.AutoPopDelay = 10000;
            toolTipConflict.BackColor = Color.Khaki;
            toolTipConflict.InitialDelay = 50;
            toolTipConflict.ReshowDelay = 10;
            toolTipConflict.ToolTipIcon = ToolTipIcon.Warning;
            toolTipConflict.ToolTipTitle = "Conflicts";
            // 
            // expertGroupBox
            // 
            expertGroupBox.Controls.Add(buildVPKFolderMultiButton);
            expertGroupBox.Controls.Add(vpkValidButton);
            expertGroupBox.Controls.Add(buildVPKFolderButton);
            expertGroupBox.Location = new Point(490, 576);
            expertGroupBox.Name = "expertGroupBox";
            expertGroupBox.Size = new Size(198, 113);
            expertGroupBox.TabIndex = 6;
            expertGroupBox.TabStop = false;
            expertGroupBox.Text = "Tools";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 701);
            Controls.Add(expertGroupBox);
            Controls.Add(gameVersionLabel);
            Controls.Add(actionGroupBox);
            Controls.Add(selectedAddonGroupBox);
            Controls.Add(addonBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Postal III Addon Manager v1.00";
            addonBox.ResumeLayout(false);
            selectedAddonGroupBox.ResumeLayout(false);
            selectedAddonGroupBox.PerformLayout();
            descriptionGroupBox.ResumeLayout(false);
            versionGroupBox.ResumeLayout(false);
            versionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)addonPictureBox).EndInit();
            groupBox2.ResumeLayout(false);
            actionGroupBox.ResumeLayout(false);
            expertGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox addonBox;
        private CheckedListBox addonListBox;
        private GroupBox selectedAddonGroupBox;
        private LinkLabel addonLinkLabel;
        private GroupBox groupBox2;
        private CheckedListBox containsListBox;
        private Button saveButton;
        private PictureBox addonPictureBox;
        private GroupBox actionGroupBox;
        private Button reloadButton;
        private Label gameVersionLabel;
        public Label minimumVersionDisplayLabel;
        private GroupBox descriptionGroupBox;
        private GroupBox versionGroupBox;
        private Button addonListButton;
        private Button addonFolderButton;
        private Button addonInfoButton;
        public RichTextBox descriptionTextBox;
        private Label statusLabel;
        public RichTextBox conflictTextBox;
        public ToolTip toolTip;
        private ToolTip toolTipConflict;
        private Button vpkValidButton;
        private Button buildVPKFolderMultiButton;
        private Button buildVPKFolderButton;
        private GroupBox expertGroupBox;
    }
}
