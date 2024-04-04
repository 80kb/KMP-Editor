using KMP_Editor.Serial;
using System.Diagnostics;

namespace KMP_Editor
{
    public partial class MainForm : Form
    {
        private KMP FileInstance;

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

            for (int i = 0; i < icons.Images.Count; i++)
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

            sectionTree.Nodes[0].Tag = FileInstance.KTPT;
            sectionTree.Nodes[1].Tag = FileInstance.ENPH;
            sectionTree.Nodes[2].Tag = FileInstance.ITPH;
            sectionTree.Nodes[3].Tag = FileInstance.CKPH;
            sectionTree.Nodes[4].Tag = FileInstance.GOBJ;
            sectionTree.Nodes[5].Tag = FileInstance.POTI;
            sectionTree.Nodes[6].Tag = FileInstance.AREA;
            sectionTree.Nodes[7].Tag = FileInstance.CAME;
            sectionTree.Nodes[8].Tag = FileInstance.JGPT;
            sectionTree.Nodes[9].Tag = FileInstance.CNPT;
            sectionTree.Nodes[10].Tag = FileInstance.MSPT;
            sectionTree.Nodes[11].Tag = FileInstance.STGI;
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
            KMP._ISection section = (KMP._ISection)sectionTree.SelectedNode.Tag;
            if (section == null)
                return;

            entryListBox.Items.Clear();
            for (int i = 0; i < section.Length(); i++)
            {
                entryListBox.Items.Add("Node " + i);
            }
        }
    }
}