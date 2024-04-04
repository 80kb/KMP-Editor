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
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
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
            sectionTree.Location = new Point(12, 27);
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
            sectionTree.Size = new Size(174, 411);
            sectionTree.TabIndex = 1;
            sectionTree.AfterSelect += sectionTree_AfterSelect;
            // 
            // entryListBox
            // 
            entryListBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            entryListBox.FormattingEnabled = true;
            entryListBox.ItemHeight = 15;
            entryListBox.Location = new Point(594, 57);
            entryListBox.Name = "entryListBox";
            entryListBox.Size = new Size(194, 154);
            entryListBox.TabIndex = 2;
            // 
            // entryPropertyGrid
            // 
            entryPropertyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            entryPropertyGrid.Location = new Point(594, 217);
            entryPropertyGrid.Name = "entryPropertyGrid";
            entryPropertyGrid.Size = new Size(194, 221);
            entryPropertyGrid.TabIndex = 3;
            // 
            // addButton
            // 
            addButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addButton.Location = new Point(594, 28);
            addButton.Name = "addButton";
            addButton.Size = new Size(98, 23);
            addButton.TabIndex = 4;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            removeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            removeButton.Location = new Point(698, 28);
            removeButton.Name = "removeButton";
            removeButton.Size = new Size(90, 23);
            removeButton.TabIndex = 5;
            removeButton.Text = "Remove";
            removeButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(removeButton);
            Controls.Add(addButton);
            Controls.Add(entryPropertyGrid);
            Controls.Add(entryListBox);
            Controls.Add(sectionTree);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "KMP Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
    }
}