using KMP_Editor.Control;
using KMP_Editor.Serial;
using System.Diagnostics;

namespace KMP_Editor
{
    public partial class MainForm : Form
    {
        private KMP FileInstance;
        private INode SelectedNode;

        public MainForm()
        {
            InitializeComponent();
            LoadTreeIcons();
        }

        private void LoadTreeIcons()
        {
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

        private void Populate()
        {
            if (FileInstance == null)
                return;

            sectionTree.Enabled = true;

            // KTPT
            KTPTNode ktptNode = new KTPTNode(FileInstance);
            sectionTree.Nodes[0].Tag = ktptNode;

            // ENPH/ENPT
            ENPHNode enphNode = new ENPHNode(FileInstance);
            List<KMP._ISectionEntry> enphData = enphNode.GetData();

            sectionTree.Nodes[1].Nodes.Clear();
            sectionTree.Nodes[1].Tag = enphNode;
            for (int i = 0; i < enphData.Count; i++)
            {
                KMP._ENPH enph = (KMP._ENPH)enphData[i];
                ENPHGroupNode enphGroupNode = new ENPHGroupNode(FileInstance, i);

                TreeNode treeNode = new TreeNode("Group " + i);
                treeNode.Tag = enphGroupNode;
                treeNode.ImageIndex = 1;
                treeNode.SelectedImageIndex = 1;

                sectionTree.Nodes[1].Nodes.Add(treeNode);
            }
        }

        private void UpdateUI()
        {
            Populate();

            entryListBox.Items.Clear();
            entryPropertyGrid.SelectedObject = null;
            for (int i = 0; i < SelectedNode.GetData().Count; i++)
            {
                entryListBox.Items.Add("Node " + i);
            }
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "KMP Files (*.kmp)|*.kmp|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] buffer = File.ReadAllBytes(ofd.FileName);
                FileInstance = new KMP(buffer, ofd.FileName);
                Text = "KMP Editor - " + Path.GetFileName(FileInstance.Filename);
                Populate();
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (FileInstance != null && File.Exists(FileInstance.Filename))
            {
                File.WriteAllBytes(FileInstance.Filename, FileInstance.Write());
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
            }
        }

        private void newMenuItem_Click(object sender, EventArgs e)
        {
            FileInstance = new KMP();
            Text = "KMP Editor - " + Path.GetFileName(FileInstance.Filename);
            Populate();
        }

        private void sectionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            INode node = (INode)sectionTree.SelectedNode.Tag;
            if (node == null)
                return;

            entryListBox.Items.Clear();
            entryPropertyGrid.SelectedObject = null;

            SelectedNode = node;
            for (int i = 0; i < node.GetData().Count; i++)
            {
                entryListBox.Items.Add("Node " + i);
            }
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
            SelectedNode.AddEntry();
            UpdateUI();
            entryListBox.SelectedIndex = SelectedNode.GetData().Count - 1;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (entryListBox.SelectedIndex < 0) 
                return;

            SelectedNode.RemoveEntry(entryListBox.SelectedIndex);
            UpdateUI();
        }
    }
}