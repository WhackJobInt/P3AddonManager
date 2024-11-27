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
            upArrowButton = new Button();
            downArrowButton = new Button();
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
            gameVersionLabel = new Label();
            toolTip = new ToolTip(components);
            toolTipConflict = new ToolTip(components);
            menuStrip1 = new MenuStrip();
            p3AMToolStripMenuItem = new ToolStripMenuItem();
            saveCurrentSettingsToolStripMenuItem = new ToolStripMenuItem();
            reloadResetToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            addonifyFoldersToolStripMenuItem = new ToolStripMenuItem();
            generateAddoninfotxtForEmptyFoldersToolStripMenuItem = new ToolStripMenuItem();
            changeExePathToolStripMenuItem = new ToolStripMenuItem();
            changeGamePathp3ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolStripComboBox1 = new ToolStripMenuItem();
            openAddonsFolderToolStripMenuItem = new ToolStripMenuItem();
            addonsFolderToolStripMenuItem = new ToolStripMenuItem();
            addoninfotxtactiveToolStripMenuItem = new ToolStripMenuItem();
            addonlisttxtToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            createAddonToolStripMenuItem = new ToolStripMenuItem();
            cloneSelectedToolStripMenuItem = new ToolStripMenuItem();
            newAddonToolStripMenuItem = new ToolStripMenuItem();
            descriptionEditorToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            vPKValidCheckingFolderToolStripMenuItem = new ToolStripMenuItem();
            buildVPKFromFolderToolStripMenuItem = new ToolStripMenuItem();
            changePakNameToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            singleToolStripMenuItem = new ToolStripMenuItem();
            multiToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            saveButton = new Button();
            reloadButton = new Button();
            groupBox1 = new GroupBox();
            cmdTextBox = new TextBox();
            launchButton = new Button();
            addonBox.SuspendLayout();
            selectedAddonGroupBox.SuspendLayout();
            descriptionGroupBox.SuspendLayout();
            versionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)addonPictureBox).BeginInit();
            groupBox2.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // addonBox
            // 
            addonBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            addonBox.Controls.Add(upArrowButton);
            addonBox.Controls.Add(downArrowButton);
            addonBox.Controls.Add(addonListBox);
            addonBox.Location = new Point(492, 50);
            addonBox.Name = "addonBox";
            addonBox.Size = new Size(224, 510);
            addonBox.TabIndex = 0;
            addonBox.TabStop = false;
            addonBox.Text = "Addons (p3)";
            toolTip.SetToolTip(addonBox, "The addons loaded from addonlist.txt\r\n\r\nPress and hold down the Right Mouse button to drag addons to change their load order.\r\nUse the Up and Down buttons to the right to change their load order.");
            // 
            // upArrowButton
            // 
            upArrowButton.FlatAppearance.BorderSize = 10;
            upArrowButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            upArrowButton.Location = new Point(196, 16);
            upArrowButton.Name = "upArrowButton";
            upArrowButton.Size = new Size(25, 23);
            upArrowButton.TabIndex = 7;
            upArrowButton.Text = "🠅";
            upArrowButton.UseVisualStyleBackColor = true;
            upArrowButton.Click += upArrowButton_Click;
            // 
            // downArrowButton
            // 
            downArrowButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            downArrowButton.Location = new Point(196, 39);
            downArrowButton.Name = "downArrowButton";
            downArrowButton.Size = new Size(25, 23);
            downArrowButton.TabIndex = 7;
            downArrowButton.Text = "🠇";
            downArrowButton.UseVisualStyleBackColor = true;
            downArrowButton.Click += downArrowButton_Click;
            // 
            // addonListBox
            // 
            addonListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            addonListBox.FormattingEnabled = true;
            addonListBox.Items.AddRange(new object[] { "(1)Test", "(2)Test2", "(3)Test3", "(4)Skibidi Rizz", "(5)Main Menu Example", "(6)Very Long Mod Name To Fuck With Me", "(7)Carrot Dude", "(8)Malfunction", "(9)Better AI", "(10)Better Maps", "I can't keep a secret", "But neither you can now!" });
            addonListBox.Location = new Point(6, 18);
            addonListBox.Name = "addonListBox";
            addonListBox.ScrollAlwaysVisible = true;
            addonListBox.Size = new Size(188, 454);
            addonListBox.TabIndex = 1;
            addonListBox.ItemCheck += addonListBox_ItemCheck;
            addonListBox.SelectedIndexChanged += addonListBox_SelectedIndexChanged;
            addonListBox.DragDrop += addonListBox_DragDrop;
            addonListBox.DragOver += addonListBox_DragOver;
            addonListBox.MouseDown += addonListBox_MouseDown;
            // 
            // selectedAddonGroupBox
            // 
            selectedAddonGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            selectedAddonGroupBox.Controls.Add(conflictTextBox);
            selectedAddonGroupBox.Controls.Add(statusLabel);
            selectedAddonGroupBox.Controls.Add(descriptionGroupBox);
            selectedAddonGroupBox.Controls.Add(versionGroupBox);
            selectedAddonGroupBox.Controls.Add(addonPictureBox);
            selectedAddonGroupBox.Controls.Add(groupBox2);
            selectedAddonGroupBox.Controls.Add(addonLinkLabel);
            selectedAddonGroupBox.Location = new Point(8, 28);
            selectedAddonGroupBox.Name = "selectedAddonGroupBox";
            selectedAddonGroupBox.Size = new Size(474, 700);
            selectedAddonGroupBox.TabIndex = 1;
            selectedAddonGroupBox.TabStop = false;
            selectedAddonGroupBox.Text = "Selected Addon [Test 1.0 by Kizoky] (-1)";
            toolTip.SetToolTip(selectedAddonGroupBox, "The currently selected addon.\r\n\r\nThe number encapsulated in () symbols is the load order.");
            // 
            // conflictTextBox
            // 
            conflictTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            conflictTextBox.BackColor = Color.WhiteSmoke;
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
            descriptionGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            descriptionGroupBox.Controls.Add(descriptionTextBox);
            descriptionGroupBox.Location = new Point(6, 350);
            descriptionGroupBox.Name = "descriptionGroupBox";
            descriptionGroupBox.Size = new Size(462, 335);
            descriptionGroupBox.TabIndex = 9;
            descriptionGroupBox.TabStop = false;
            descriptionGroupBox.Text = "Description";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            descriptionTextBox.Location = new Point(3, 19);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ReadOnly = true;
            descriptionTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            descriptionTextBox.Size = new Size(456, 313);
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
            toolTip.SetToolTip(minimumVersionDisplayLabel, "The minimum version required to run this addon properly.\r\n\r\nRed = Addon will not work\r\nOlive = Addon might or not work\r\nGreen = Addon will work\r\n\r\nCheck console for errors if using Ultrapatch Angel");
            // 
            // addonPictureBox
            // 
            addonPictureBox.Image = Properties.Resources._default;
            addonPictureBox.Location = new Point(6, 35);
            addonPictureBox.Name = "addonPictureBox";
            addonPictureBox.Size = new Size(320, 240);
            addonPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
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
            containsListBox.Items.AddRange(new object[] { "Cfg", "Map", "Material", "Media", "Model", "Particle", "Resource", "Script", "Sound", "VPK", "Postal3Script", "AngelScript" });
            containsListBox.Location = new Point(6, 22);
            containsListBox.Name = "containsListBox";
            containsListBox.Size = new Size(120, 166);
            containsListBox.TabIndex = 0;
            containsListBox.ItemCheck += containsListBox_ItemCheck;
            // 
            // addonLinkLabel
            // 
            addonLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            addonLinkLabel.AutoSize = true;
            addonLinkLabel.BorderStyle = BorderStyle.Fixed3D;
            addonLinkLabel.Location = new Point(6, 331);
            addonLinkLabel.Name = "addonLinkLabel";
            addonLinkLabel.Size = new Size(27, 17);
            addonLinkLabel.TabIndex = 2;
            addonLinkLabel.TabStop = true;
            addonLinkLabel.Text = "n/a";
            toolTip.SetToolTip(addonLinkLabel, "The link of the addon.");
            addonLinkLabel.LinkClicked += addonLinkLabel_LinkClicked;
            // 
            // gameVersionLabel
            // 
            gameVersionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            gameVersionLabel.AutoSize = true;
            gameVersionLabel.BorderStyle = BorderStyle.Fixed3D;
            gameVersionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            gameVersionLabel.ForeColor = Color.Green;
            gameVersionLabel.Location = new Point(496, 29);
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
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.GripStyle = ToolStripGripStyle.Visible;
            menuStrip1.Items.AddRange(new ToolStripItem[] { p3AMToolStripMenuItem, toolStripComboBox1, helpToolStripMenuItem });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.ShowItemToolTips = true;
            menuStrip1.Size = new Size(720, 23);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // p3AMToolStripMenuItem
            // 
            p3AMToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveCurrentSettingsToolStripMenuItem, reloadResetToolStripMenuItem, toolStripSeparator1, addonifyFoldersToolStripMenuItem, changeExePathToolStripMenuItem, changeGamePathp3ToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            p3AMToolStripMenuItem.Name = "p3AMToolStripMenuItem";
            p3AMToolStripMenuItem.Size = new Size(46, 19);
            p3AMToolStripMenuItem.Text = "Main";
            // 
            // saveCurrentSettingsToolStripMenuItem
            // 
            saveCurrentSettingsToolStripMenuItem.Checked = true;
            saveCurrentSettingsToolStripMenuItem.CheckState = CheckState.Checked;
            saveCurrentSettingsToolStripMenuItem.Name = "saveCurrentSettingsToolStripMenuItem";
            saveCurrentSettingsToolStripMenuItem.Size = new Size(206, 22);
            saveCurrentSettingsToolStripMenuItem.Text = "Save current settings";
            saveCurrentSettingsToolStripMenuItem.ToolTipText = "Saves current settings to the addon list.";
            saveCurrentSettingsToolStripMenuItem.Click += saveCurrentSettingsToolStripMenuItem_Click;
            // 
            // reloadResetToolStripMenuItem
            // 
            reloadResetToolStripMenuItem.Name = "reloadResetToolStripMenuItem";
            reloadResetToolStripMenuItem.Size = new Size(206, 22);
            reloadResetToolStripMenuItem.Text = "Reload/Reset";
            reloadResetToolStripMenuItem.ToolTipText = "Reloads the whole program as if you just relaunched it.";
            reloadResetToolStripMenuItem.Click += reloadResetToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(203, 6);
            // 
            // addonifyFoldersToolStripMenuItem
            // 
            addonifyFoldersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { generateAddoninfotxtForEmptyFoldersToolStripMenuItem });
            addonifyFoldersToolStripMenuItem.Name = "addonifyFoldersToolStripMenuItem";
            addonifyFoldersToolStripMenuItem.Size = new Size(206, 22);
            addonifyFoldersToolStripMenuItem.Text = "Addonify Folders ...";
            // 
            // generateAddoninfotxtForEmptyFoldersToolStripMenuItem
            // 
            generateAddoninfotxtForEmptyFoldersToolStripMenuItem.Name = "generateAddoninfotxtForEmptyFoldersToolStripMenuItem";
            generateAddoninfotxtForEmptyFoldersToolStripMenuItem.Size = new Size(253, 22);
            generateAddoninfotxtForEmptyFoldersToolStripMenuItem.Text = "Generate addoninfo.txt for folders";
            generateAddoninfotxtForEmptyFoldersToolStripMenuItem.ToolTipText = "Make ZOOM/Ultrapatch and Postal III Addon Manager recognize folders that do not have any valid addoninfo.txt";
            generateAddoninfotxtForEmptyFoldersToolStripMenuItem.Click += generateAddoninfotxtForEmptyFoldersToolStripMenuItem_Click;
            // 
            // changeExePathToolStripMenuItem
            // 
            changeExePathToolStripMenuItem.Name = "changeExePathToolStripMenuItem";
            changeExePathToolStripMenuItem.Size = new Size(206, 22);
            changeExePathToolStripMenuItem.Text = "Change exe path";
            changeExePathToolStripMenuItem.ToolTipText = "Changes the exe/binary path.";
            changeExePathToolStripMenuItem.Click += changeExePathToolStripMenuItem_Click;
            // 
            // changeGamePathp3ToolStripMenuItem
            // 
            changeGamePathp3ToolStripMenuItem.Name = "changeGamePathp3ToolStripMenuItem";
            changeGamePathp3ToolStripMenuItem.Size = new Size(206, 22);
            changeGamePathp3ToolStripMenuItem.Text = "Change game folder (p3)";
            changeGamePathp3ToolStripMenuItem.ToolTipText = "Changes the game folder. (ex.: p3, hl2, ep1, ep2, cr_base, sourcemod, etc..)";
            changeGamePathp3ToolStripMenuItem.Click += changeGamePathp3ToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(203, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(206, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.ToolTipText = "Causes a black hole to appear and suck this program in.";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // toolStripComboBox1
            // 
            toolStripComboBox1.DropDownItems.AddRange(new ToolStripItem[] { openAddonsFolderToolStripMenuItem, toolStripSeparator5, createAddonToolStripMenuItem, descriptionEditorToolStripMenuItem, toolStripSeparator4, vPKValidCheckingFolderToolStripMenuItem, buildVPKFromFolderToolStripMenuItem });
            toolStripComboBox1.Name = "toolStripComboBox1";
            toolStripComboBox1.Size = new Size(46, 19);
            toolStripComboBox1.Text = "Tools";
            // 
            // openAddonsFolderToolStripMenuItem
            // 
            openAddonsFolderToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addonsFolderToolStripMenuItem, addoninfotxtactiveToolStripMenuItem, addonlisttxtToolStripMenuItem });
            openAddonsFolderToolStripMenuItem.Name = "openAddonsFolderToolStripMenuItem";
            openAddonsFolderToolStripMenuItem.Size = new Size(220, 22);
            openAddonsFolderToolStripMenuItem.Text = "Open ...";
            // 
            // addonsFolderToolStripMenuItem
            // 
            addonsFolderToolStripMenuItem.Name = "addonsFolderToolStripMenuItem";
            addonsFolderToolStripMenuItem.Size = new Size(200, 22);
            addonsFolderToolStripMenuItem.Text = "addons Folder";
            addonsFolderToolStripMenuItem.ToolTipText = "Opens <gamefolder>/addons folder";
            addonsFolderToolStripMenuItem.Click += addonsFolderToolStripMenuItem_Click;
            // 
            // addoninfotxtactiveToolStripMenuItem
            // 
            addoninfotxtactiveToolStripMenuItem.Name = "addoninfotxtactiveToolStripMenuItem";
            addoninfotxtactiveToolStripMenuItem.Size = new Size(200, 22);
            addoninfotxtactiveToolStripMenuItem.Text = "addoninfo.txt (selected)";
            addoninfotxtactiveToolStripMenuItem.ToolTipText = "Opens the currently selected addon's addoninfo.txt in your text editor.";
            addoninfotxtactiveToolStripMenuItem.Click += addoninfotxtactiveToolStripMenuItem_Click;
            // 
            // addonlisttxtToolStripMenuItem
            // 
            addonlisttxtToolStripMenuItem.Name = "addonlisttxtToolStripMenuItem";
            addonlisttxtToolStripMenuItem.Size = new Size(200, 22);
            addonlisttxtToolStripMenuItem.Text = "addonlist.txt";
            addonlisttxtToolStripMenuItem.ToolTipText = "Opens addonlist.txt in your text editor.";
            addonlisttxtToolStripMenuItem.Click += addonlisttxtToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(217, 6);
            // 
            // createAddonToolStripMenuItem
            // 
            createAddonToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cloneSelectedToolStripMenuItem, newAddonToolStripMenuItem });
            createAddonToolStripMenuItem.Name = "createAddonToolStripMenuItem";
            createAddonToolStripMenuItem.Size = new Size(220, 22);
            createAddonToolStripMenuItem.Text = "Create New Addon ..";
            // 
            // cloneSelectedToolStripMenuItem
            // 
            cloneSelectedToolStripMenuItem.Name = "cloneSelectedToolStripMenuItem";
            cloneSelectedToolStripMenuItem.Size = new Size(152, 22);
            cloneSelectedToolStripMenuItem.Text = "Clone Selected";
            cloneSelectedToolStripMenuItem.ToolTipText = "Creates a copy of the currently selected addon.";
            cloneSelectedToolStripMenuItem.Click += cloneSelectedToolStripMenuItem_Click;
            // 
            // newAddonToolStripMenuItem
            // 
            newAddonToolStripMenuItem.Name = "newAddonToolStripMenuItem";
            newAddonToolStripMenuItem.Size = new Size(152, 22);
            newAddonToolStripMenuItem.Text = "New Addon";
            newAddonToolStripMenuItem.ToolTipText = "Creates a completely new addon.";
            newAddonToolStripMenuItem.Click += newAddonToolStripMenuItem_Click;
            // 
            // descriptionEditorToolStripMenuItem
            // 
            descriptionEditorToolStripMenuItem.Name = "descriptionEditorToolStripMenuItem";
            descriptionEditorToolStripMenuItem.Size = new Size(220, 22);
            descriptionEditorToolStripMenuItem.Text = "Addon Editor (selected)";
            descriptionEditorToolStripMenuItem.ToolTipText = "Launches an editor which modifies the currently selected addon's info.";
            descriptionEditorToolStripMenuItem.Click += descriptionEditorToolStripMenuItem_Click_1;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(217, 6);
            // 
            // vPKValidCheckingFolderToolStripMenuItem
            // 
            vPKValidCheckingFolderToolStripMenuItem.Name = "vPKValidCheckingFolderToolStripMenuItem";
            vPKValidCheckingFolderToolStripMenuItem.Size = new Size(220, 22);
            vPKValidCheckingFolderToolStripMenuItem.Text = "VPK Valid Checking (Folder)";
            vPKValidCheckingFolderToolStripMenuItem.ToolTipText = "Checks if a specified folder has no illegal characters for building a VPK.";
            vPKValidCheckingFolderToolStripMenuItem.Click += vPKValidCheckingFolderToolStripMenuItem_Click;
            // 
            // buildVPKFromFolderToolStripMenuItem
            // 
            buildVPKFromFolderToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changePakNameToolStripMenuItem, toolStripSeparator3, singleToolStripMenuItem, multiToolStripMenuItem });
            buildVPKFromFolderToolStripMenuItem.Name = "buildVPKFromFolderToolStripMenuItem";
            buildVPKFromFolderToolStripMenuItem.Size = new Size(220, 22);
            buildVPKFromFolderToolStripMenuItem.Text = "Build VPK from Folder";
            buildVPKFromFolderToolStripMenuItem.Click += buildVPKFromFolderToolStripMenuItem_Click;
            // 
            // changePakNameToolStripMenuItem
            // 
            changePakNameToolStripMenuItem.Name = "changePakNameToolStripMenuItem";
            changePakNameToolStripMenuItem.Size = new Size(236, 22);
            changePakNameToolStripMenuItem.Text = "Change pak name (pak01)";
            changePakNameToolStripMenuItem.ToolTipText = "Changes the name of the VPK file. Useful for single VPKs.";
            changePakNameToolStripMenuItem.Click += changePakNameToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(233, 6);
            // 
            // singleToolStripMenuItem
            // 
            singleToolStripMenuItem.Name = "singleToolStripMenuItem";
            singleToolStripMenuItem.Size = new Size(236, 22);
            singleToolStripMenuItem.Text = "Single (max. 200mb)";
            singleToolStripMenuItem.ToolTipText = "Creates a single VPK file. (ex.: myaddon.vpk)";
            singleToolStripMenuItem.Click += singleToolStripMenuItem_Click;
            // 
            // multiToolStripMenuItem
            // 
            multiToolStripMenuItem.Name = "multiToolStripMenuItem";
            multiToolStripMenuItem.Size = new Size(236, 22);
            multiToolStripMenuItem.Text = "Multi (pak01_000, pak01_001 ..)";
            multiToolStripMenuItem.ToolTipText = "Creates a multi pak file. Useful for big mods.";
            multiToolStripMenuItem.Click += multiToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 19);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Location = new Point(6, 22);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(87, 41);
            saveButton.TabIndex = 9;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // reloadButton
            // 
            reloadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            reloadButton.Location = new Point(131, 22);
            reloadButton.Name = "reloadButton";
            reloadButton.Size = new Size(87, 41);
            reloadButton.TabIndex = 9;
            reloadButton.Text = "Reload";
            reloadButton.UseVisualStyleBackColor = true;
            reloadButton.Click += reloadButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(cmdTextBox);
            groupBox1.Controls.Add(launchButton);
            groupBox1.Controls.Add(saveButton);
            groupBox1.Controls.Add(reloadButton);
            groupBox1.Location = new Point(492, 566);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(224, 162);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Main";
            // 
            // cmdTextBox
            // 
            cmdTextBox.Location = new Point(6, 69);
            cmdTextBox.Multiline = true;
            cmdTextBox.Name = "cmdTextBox";
            cmdTextBox.Size = new Size(212, 51);
            cmdTextBox.TabIndex = 11;
            cmdTextBox.Text = "-novid +sv_cheats 0";
            cmdTextBox.TextChanged += cmdTextBox_TextChanged;
            // 
            // launchButton
            // 
            launchButton.Location = new Point(6, 126);
            launchButton.Name = "launchButton";
            launchButton.Size = new Size(212, 30);
            launchButton.TabIndex = 10;
            launchButton.Text = "Launch Postal III";
            launchButton.UseVisualStyleBackColor = true;
            launchButton.Click += launchButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 736);
            Controls.Add(groupBox1);
            Controls.Add(gameVersionLabel);
            Controls.Add(selectedAddonGroupBox);
            Controls.Add(addonBox);
            Controls.Add(menuStrip1);
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
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        public PictureBox addonPictureBox;
        private Label gameVersionLabel;
        public Label minimumVersionDisplayLabel;
        private GroupBox descriptionGroupBox;
        private GroupBox versionGroupBox;
        public RichTextBox descriptionTextBox;
        private Label statusLabel;
        public RichTextBox conflictTextBox;
        public ToolTip toolTip;
        private ToolTip toolTipConflict;
        private Button upArrowButton;
        private Button downArrowButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripComboBox1;
        private ToolStripMenuItem descriptionEditorToolStripMenuItem;
        private ToolStripMenuItem openAddonsFolderToolStripMenuItem;
        private ToolStripMenuItem vPKValidCheckingFolderToolStripMenuItem;
        private ToolStripMenuItem buildVPKFromFolderToolStripMenuItem;
        private ToolStripMenuItem singleToolStripMenuItem;
        private ToolStripMenuItem multiToolStripMenuItem;
        private ToolStripMenuItem addonsFolderToolStripMenuItem;
        private ToolStripMenuItem addoninfotxtactiveToolStripMenuItem;
        private ToolStripMenuItem addonlisttxtToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem p3AMToolStripMenuItem;
        private ToolStripMenuItem saveCurrentSettingsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem reloadResetToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem changeExePathToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem changeGamePathp3ToolStripMenuItem;
        private ToolStripMenuItem changePakNameToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem createAddonToolStripMenuItem;
        private ToolStripMenuItem cloneSelectedToolStripMenuItem;
        private ToolStripMenuItem newAddonToolStripMenuItem;
        private Button saveButton;
        private Button reloadButton;
        private GroupBox groupBox1;
        private Button launchButton;
        private TextBox cmdTextBox;
        private ToolStripMenuItem addonifyFoldersToolStripMenuItem;
        private ToolStripMenuItem generateAddoninfotxtForEmptyFoldersToolStripMenuItem;
    }
}
