using KMP_Editor.Control;
using KMP_Editor.Serial;
using System.Diagnostics;

namespace KMP_Editor
{
    public partial class MainForm : Form
    {
        private KMP FileInstance;
        private List<KMP._ISectionEntry> SelectedData;

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

            // ENPH/ENPT
            ENPHNode enphNode = new ENPHNode(FileInstance);
            sectionTree.Nodes[1].Nodes.Clear();
            sectionTree.Nodes[1].Tag = enphNode.GetData();
            for(int i = 0; i < FileInstance.ENPH.Length(); i++)
            {
                TreeNode enptNode = new TreeNode("Group " + i);
                enptNode.Tag = enphNode.GetGroup(i);
                enptNode.ImageIndex = 1;
                enptNode.SelectedImageIndex = 1;
                sectionTree.Nodes[1].Nodes.Add(enptNode);
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
            List<KMP._ISectionEntry> data = (List<KMP._ISectionEntry>)sectionTree.SelectedNode.Tag;
            if (data == null)
                return;

            entryListBox.Items.Clear();
            entryPropertyGrid.SelectedObject = null;

            SelectedData = data;
            for (int i = 0; i < data.Count; i++)
            {
                entryListBox.Items.Add("Node " + i);
            }
        }

        private void entryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entryListBox.SelectedIndex < 0)
                return;

            entryPropertyGrid.SelectedObject = SelectedData[entryListBox.SelectedIndex];
        }
    }
}