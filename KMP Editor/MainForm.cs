using KMP_Editor.Control.Nodes;
using KMP_Editor.Control;
using KartLib.Serial;
using System.Text;

namespace KMP_Editor
{
    public partial class MainForm : Form
    {
        private KMP? FileInstance;
        private Node? SelectedNode;
        private bool UnsavedChanges;

        public MainForm()
        {
            UnsavedChanges = false;

            InitializeComponent();
            InitializeUI();
        }

        // Helper functions

        private void InitNodes()
        {
            if (FileInstance == null)
                return;

            sectionTree.Nodes[0].Tag = new KTPTNode(FileInstance, viewport);
            sectionTree.Nodes[1].Tag = new ENPHNode(FileInstance);
            sectionTree.Nodes[2].Tag = new ITPHNode(FileInstance);
            sectionTree.Nodes[3].Tag = new CKPHNode(FileInstance);
            sectionTree.Nodes[4].Tag = new GOBJNode(FileInstance);
            sectionTree.Nodes[5].Tag = new POTINode(FileInstance);
            sectionTree.Nodes[6].Tag = new AREANode(FileInstance);
            sectionTree.Nodes[7].Tag = new CAMENode(FileInstance);
            sectionTree.Nodes[8].Tag = new JGPTNode(FileInstance);
            sectionTree.Nodes[9].Tag = new CNPTNode(FileInstance);
            sectionTree.Nodes[10].Tag = new MSPTNode(FileInstance);
            sectionTree.Nodes[11].Tag = new STGINode(FileInstance);
        }

        private void InitializeUI()
        {
            // Load tree icons
            ImageList icons = new ImageList();
            icons.Images.Add(Properties.Resources.star);
            icons.Images.Add(Properties.Resources.balloons);
            icons.Images.Add(Properties.Resources.redshell);
            icons.Images.Add(Properties.Resources.flag);
            icons.Images.Add(Properties.Resources.crate);
            icons.Images.Add(Properties.Resources.goomba);
            icons.Images.Add(Properties.Resources.blueshell);
            icons.Images.Add(Properties.Resources.camera);
            icons.Images.Add(Properties.Resources.lakitu);
            icons.Images.Add(Properties.Resources.cannon);
            icons.Images.Add(Properties.Resources.coin);
            icons.Images.Add(Properties.Resources.greenshell);
            icons.ImageSize = new Size(24, 24);
            sectionTree.ImageList = icons;

            for (int i = 0; i < sectionTree.Nodes.Count; i++)
            {
                sectionTree.Nodes[i].ImageIndex = i;
                sectionTree.Nodes[i].SelectedImageIndex = i;
            }
        }

        private void PopulateUI()
        {
            sectionTree.Enabled = true;
            propertyGroupBox.Enabled = true;
            entryGroupBox.Enabled = true;
            viewport.ClearShapes();

            foreach (TreeNode node in sectionTree.Nodes)
                if (node.Tag != null) ((Node)node.Tag).Populate(node);
        }

        private void UpdateUI()
        {
            entryListBox.Items.Clear();
            entryPropertyGrid.SelectedObject = null;
            for (int i = 0; i < SelectedNode?.GetData().Count; i++)
            {
                entryListBox.Items.Add(SelectedNode.GetTitle(i));
            }

            if (UnsavedChanges)
            {
                foreach (char c in Text) if (c == '*') return;
                Text += "*";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in Text) { if (c != '*') sb.Append(c); }
                Text = sb.ToString();
            }
        }

        private void UpdateShapes()
        {
            if (SelectedNode == null)
                return;

            viewport.ClearShapes();
            SelectedNode.AddShapes();
            viewport.Invalidate();
        }

        // Event handlers

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "KMP Files (*.kmp)|*.kmp|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] buffer = File.ReadAllBytes(ofd.FileName);
                FileInstance = new KMP(buffer, ofd.FileName);
                Text = "KMP Editor - " + Path.GetFileName(FileInstance.Filename);
                InitNodes();
                PopulateUI();
                sectionTree.SelectedNode = sectionTree.Nodes[0];
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (FileInstance != null && File.Exists(FileInstance.Filename))
            {
                File.WriteAllBytes(FileInstance.Filename, FileInstance.Write());
                UnsavedChanges = false;
                UpdateUI();
            }
            else if (FileInstance != null)
            {
                saveAsMenuItem_Click(sender, e);
            }
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (FileInstance == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "KMP Files (*.kmp)|*.kmp|All Files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, FileInstance.Write());
                UnsavedChanges = false;
                UpdateUI();
            }
        }

        private void newMenuItem_Click(object sender, EventArgs e)
        {
            FileInstance = new KMP();
            Text = "KMP Editor - " + Path.GetFileName(FileInstance.Filename);
            InitNodes();
            PopulateUI();
        }

        private void sectionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Node node = (Node)sectionTree.SelectedNode.Tag;
            if (node == null)
                return;

            entryListBox.Items.Clear();
            entryPropertyGrid.SelectedObject = null;
            SelectedNode = node;
            UpdateShapes();
            for (int i = 0; i < node.GetData().Count; i++)
            {
                entryListBox.Items.Add(node.GetTitle(i));
            }
            if (entryListBox.Items.Count > 0) entryListBox.SelectedIndex = entryListBox.Items.Count - 1;
        }

        private void entryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedNode == null || entryListBox.SelectedIndex < 0)
                return;

            List<KMP._ISectionEntry> data = SelectedNode.GetData();
            entryPropertyGrid.SelectedObject = data[entryListBox.SelectedIndex];
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null)
                return;

            SelectedNode.AddEntry();
            UnsavedChanges = true;
            UpdateUI();
            PopulateUI();
            UpdateShapes();
            entryListBox.SelectedIndex = entryListBox.Items.Count - 1;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null)
                return;

            if (entryListBox.SelectedIndex < 0)
                return;

            int tmpIndex = entryListBox.SelectedIndex;
            SelectedNode.RemoveEntry(entryListBox.SelectedIndex);
            UnsavedChanges = true;
            UpdateUI();
            PopulateUI();
            UpdateShapes();
            entryListBox.SelectedIndex = tmpIndex - 1;
        }
    }
}