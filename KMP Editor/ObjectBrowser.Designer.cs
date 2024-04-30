namespace KMP_Editor
{
    partial class ObjectBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectBrowser));
            idTextBox = new TextBox();
            selectButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(12, 12);
            idTextBox.Name = "idTextBox";
            idTextBox.Size = new Size(448, 23);
            idTextBox.TabIndex = 0;
            // 
            // selectButton
            // 
            selectButton.Location = new Point(385, 41);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(75, 23);
            selectButton.TabIndex = 1;
            selectButton.Text = "Select";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += selectButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(304, 41);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // ObjectBrowser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 73);
            Controls.Add(cancelButton);
            Controls.Add(selectButton);
            Controls.Add(idTextBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ObjectBrowser";
            Text = "Object Browser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox idTextBox;
        private Button selectButton;
        private Button cancelButton;
    }
}