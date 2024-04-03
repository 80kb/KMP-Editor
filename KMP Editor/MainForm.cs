using KMP_Editor.Serial;

namespace KMP_Editor
{
    public partial class MainForm : Form
    {
        private KMP FileInstance;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Populate()
        {
            if (FileInstance == null)
                return;

            sectionTree.Enabled = true;

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
    }
}