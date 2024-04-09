namespace KMP_Editor
{
    partial class MainForm
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
            TreeNode treeNode1 = new TreeNode("Start Position(s)");
            TreeNode treeNode2 = new TreeNode("Enemy Routes");
            TreeNode treeNode3 = new TreeNode("Item Routes");
            TreeNode treeNode4 = new TreeNode("Checkpoints");
            TreeNode treeNode5 = new TreeNode("Objects");
            TreeNode treeNode6 = new TreeNode("Routes");
            TreeNode treeNode7 = new TreeNode("Areas");
            TreeNode treeNode8 = new TreeNode("Cameras");
            TreeNode treeNode9 = new TreeNode("Respawns");
            TreeNode treeNode10 = new TreeNode("Cannons");
            TreeNode treeNode11 = new TreeNode("Battle Endpoints");
            TreeNode treeNode12 = new TreeNode("Stage Info");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            newMenuItem = new ToolStripMenuItem();
            openMenuItem = new ToolStripMenuItem();
            saveMenuItem = new ToolStripMenuItem();
            saveAsMenuItem = new ToolStripMenuItem();
            sectionTree = new TreeView();
            entryListBox = new ListBox();
            entryPropertyGrid = new PropertyGrid();
            addButton = new Button();
            removeButton = new Button();
            entryGroupBox = new GroupBox();
            propertyGroupBox = new GroupBox();
            viewport = new Panel();
            sectionGroupBox = new GroupBox();
            menuStrip1.SuspendLayout();
            entryGroupBox.SuspendLayout();
            propertyGroupBox.SuspendLayout();
            sectionGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1113, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newMenuItem, openMenuItem, saveMenuItem, saveAsMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.Size = new Size(37, 20);
            fileMenuItem.Text = "File";
            // 
            // newMenuItem
            // 
            newMenuItem.Image = Properties.Resources.page_white_add;
            newMenuItem.Name = "newMenuItem";
            newMenuItem.Size = new Size(121, 22);
            newMenuItem.Text = "New";
            newMenuItem.Click += newMenuItem_Click;
            // 
            // openMenuItem
            // 
            openMenuItem.Image = Properties.Resources.folder;
            openMenuItem.Name = "openMenuItem";
            openMenuItem.Size = new Size(121, 22);
            openMenuItem.Text = "Open...";
            openMenuItem.Click += openMenuItem_Click;
            // 
            // saveMenuItem
            // 
            saveMenuItem.Image = Properties.Resources.disk;
            saveMenuItem.Name = "saveMenuItem";
            saveMenuItem.Size = new Size(121, 22);
            saveMenuItem.Text = "Save";
            saveMenuItem.Click += saveMenuItem_Click;
            // 
            // saveAsMenuItem
            // 
            saveAsMenuItem.Name = "saveAsMenuItem";
            saveAsMenuItem.Size = new Size(121, 22);
            saveAsMenuItem.Text = "Save as...";
            saveAsMenuItem.Click += saveAsMenuItem_Click;
            // 
            // sectionTree
            // 
            sectionTree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            sectionTree.Enabled = false;
            sectionTree.Location = new Point(6, 22);
            sectionTree.Name = "sectionTree";
            treeNode1.Name = "ktptNode";
            treeNode1.Text = "Start Position(s)";
            treeNode2.Name = "enptNode";
            treeNode2.Text = "Enemy Routes";
            treeNode3.Name = "itptNode";
            treeNode3.Text = "Item Routes";
            treeNode4.Name = "ckptNode";
            treeNode4.Text = "Checkpoints";
            treeNode5.Name = "gobjNode";
            treeNode5.Text = "Objects";
            treeNode6.Name = "potiNode";
            treeNode6.Text = "Routes";
            treeNode7.Name = "areaNode";
            treeNode7.Text = "Areas";
            treeNode8.Name = "cameNode";
            treeNode8.Text = "Cameras";
            treeNode9.Name = "jgpt";
            treeNode9.Text = "Respawns";
            treeNode10.Name = "cnptNode";
            treeNode10.Text = "Cannons";
            treeNode11.Name = "msptNode";
            treeNode11.Text = "Battle Endpoints";
            treeNode12.Name = "stgiNode";
            treeNode12.Text = "Stage Info";
            sectionTree.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7, treeNode8, treeNode9, treeNode10, treeNode11, treeNode12 });
            sectionTree.Size = new Size(209, 525);
            sectionTree.TabIndex = 1;
            sectionTree.AfterSelect += sectionTree_AfterSelect;
            // 
            // entryListBox
            // 
            entryListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            entryListBox.FormattingEnabled = true;
            entryListBox.ItemHeight = 15;
            entryListBox.Location = new Point(6, 22);
            entryListBox.Name = "entryListBox";
            entryListBox.Size = new Size(205, 199);
            entryListBox.TabIndex = 2;
            entryListBox.SelectedIndexChanged += entryListBox_SelectedIndexChanged;
            // 
            // entryPropertyGrid
            // 
            entryPropertyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            entryPropertyGrid.HelpVisible = false;
            entryPropertyGrid.Location = new Point(6, 22);
            entryPropertyGrid.Name = "entryPropertyGrid";
            entryPropertyGrid.PropertySort = PropertySort.Categorized;
            entryPropertyGrid.Size = new Size(205, 263);
            entryPropertyGrid.TabIndex = 3;
            entryPropertyGrid.ToolbarVisible = false;
            // 
            // addButton
            // 
            addButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            addButton.Location = new Point(6, 227);
            addButton.Name = "addButton";
            addButton.Size = new Size(108, 23);
            addButton.TabIndex = 4;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // removeButton
            // 
            removeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            removeButton.Location = new Point(120, 227);
            removeButton.Name = "removeButton";
            removeButton.Size = new Size(93, 23);
            removeButton.TabIndex = 5;
            removeButton.Text = "Remove";
            removeButton.UseVisualStyleBackColor = true;
            removeButton.Click += removeButton_Click;
            // 
            // entryGroupBox
            // 
            entryGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            entryGroupBox.Controls.Add(addButton);
            entryGroupBox.Controls.Add(removeButton);
            entryGroupBox.Controls.Add(entryListBox);
            entryGroupBox.Location = new Point(882, 27);
            entryGroupBox.Name = "entryGroupBox";
            entryGroupBox.Size = new Size(219, 256);
            entryGroupBox.TabIndex = 6;
            entryGroupBox.TabStop = false;
            entryGroupBox.Text = "Entries";
            // 
            // propertyGroupBox
            // 
            propertyGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            propertyGroupBox.Controls.Add(entryPropertyGrid);
            propertyGroupBox.Location = new Point(882, 289);
            propertyGroupBox.Name = "propertyGroupBox";
            propertyGroupBox.Size = new Size(219, 291);
            propertyGroupBox.TabIndex = 7;
            propertyGroupBox.TabStop = false;
            propertyGroupBox.Text = "Properties";
            // 
            // viewport
            // 
            viewport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            viewport.BackColor = SystemColors.ControlDark;
            viewport.Location = new Point(239, 35);
            viewport.Name = "viewport";
            viewport.Size = new Size(637, 545);
            viewport.TabIndex = 8;
            // 
            // sectionGroupBox
            // 
            sectionGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            sectionGroupBox.Controls.Add(sectionTree);
            sectionGroupBox.Location = new Point(12, 27);
            sectionGroupBox.Name = "sectionGroupBox";
            sectionGroupBox.Size = new Size(221, 553);
            sectionGroupBox.TabIndex = 9;
            sectionGroupBox.TabStop = false;
            sectionGroupBox.Text = "KMP Sections";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1113, 592);
            Controls.Add(sectionGroupBox);
            Controls.Add(viewport);
            Controls.Add(propertyGroupBox);
            Controls.Add(entryGroupBox);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "KMP Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            entryGroupBox.ResumeLayout(false);
            propertyGroupBox.ResumeLayout(false);
            sectionGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem newMenuItem;
        private ToolStripMenuItem openMenuItem;
        private ToolStripMenuItem saveMenuItem;
        private ToolStripMenuItem saveAsMenuItem;
        private TreeView sectionTree;
        private ListBox entryListBox;
        private PropertyGrid entryPropertyGrid;
        private Button addButton;
        private Button removeButton;
        private GroupBox entryGroupBox;
        private GroupBox propertyGroupBox;
        private Panel viewport;
        private GroupBox sectionGroupBox;
    }
}