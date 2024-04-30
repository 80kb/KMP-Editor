namespace KMP_Editor
{
    public partial class ObjectBrowser : Form
    {
        public ushort SelectedID { get; set; }

        public ObjectBrowser()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            SelectedID = Convert.ToUInt16(idTextBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
