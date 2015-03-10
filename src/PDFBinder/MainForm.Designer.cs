namespace PDFBinder
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addFileButton = new System.Windows.Forms.ToolStripButton();
            this.removeButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpButton = new System.Windows.Forms.ToolStripButton();
            this.moveDownButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.completeButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.addFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.helpLabel = new System.Windows.Forms.Label();
            this.FileListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.labelTitleFileName = new System.Windows.Forms.Label();
            this.labelTitlePages = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileButton,
            this.removeButton,
            this.RemoveAllButton,
            this.toolStripSeparator1,
            this.moveUpButton,
            this.moveDownButton,
            this.toolStripSeparator2,
            this.completeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(429, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addFileButton
            // 
            this.addFileButton.Image = ((System.Drawing.Image)(resources.GetObject("addFileButton.Image")));
            this.addFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(77, 22);
            this.addFileButton.Text = "Add file...";
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Image = ((System.Drawing.Image)(resources.GetObject("removeButton.Image")));
            this.removeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(116, 22);
            this.removeButton.Text = "Remove selected";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // RemoveAllButton
            // 
            this.RemoveAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RemoveAllButton.Enabled = false;
            this.RemoveAllButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveAllButton.Image")));
            this.RemoveAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveAllButton.Name = "RemoveAllButton";
            this.RemoveAllButton.Size = new System.Drawing.Size(55, 22);
            this.RemoveAllButton.Text = "Clear All";
            this.RemoveAllButton.Click += new System.EventHandler(this.ClearAllButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // moveUpButton
            // 
            this.moveUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUpButton.Enabled = false;
            this.moveUpButton.Image = ((System.Drawing.Image)(resources.GetObject("moveUpButton.Image")));
            this.moveUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(23, 22);
            this.moveUpButton.Text = "Move up";
            this.moveUpButton.Click += new System.EventHandler(this.moveItemButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDownButton.Enabled = false;
            this.moveDownButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownButton.Image")));
            this.moveDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(23, 22);
            this.moveDownButton.Text = "toolStripButton3";
            this.moveDownButton.Click += new System.EventHandler(this.moveItemButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // completeButton
            // 
            this.completeButton.Enabled = false;
            this.completeButton.Image = ((System.Drawing.Image)(resources.GetObject("completeButton.Image")));
            this.completeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.completeButton.Name = "completeButton";
            this.completeButton.Size = new System.Drawing.Size(54, 22);
            this.completeButton.Text = "Bind!";
            this.completeButton.Click += new System.EventHandler(this.combineButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "pdf";
            this.saveFileDialog.Filter = "PDF documents|*.pdf|All files|*.*";
            // 
            // addFileDialog
            // 
            this.addFileDialog.DefaultExt = "pdf";
            this.addFileDialog.Filter = "All supported types|*.pdf;*.txt|PDF documents|*.pdf|Plain text list of PDFs|*.txt" +
    "";
            this.addFileDialog.Multiselect = true;
            this.addFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.addFileDialog_FileOk);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 203);
            this.progressBar.MaximumSize = new System.Drawing.Size(0, 16);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(0, 16);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // helpLabel
            // 
            this.helpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpLabel.AutoSize = true;
            this.helpLabel.Location = new System.Drawing.Point(4, 203);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(0, 13);
            this.helpLabel.TabIndex = 3;
            // 
            // FileListPanel
            // 
            this.FileListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileListPanel.AutoScroll = true;
            this.FileListPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FileListPanel.Location = new System.Drawing.Point(0, 42);
            this.FileListPanel.MinimumSize = new System.Drawing.Size(423, 150);
            this.FileListPanel.Name = "FileListPanel";
            this.FileListPanel.Size = new System.Drawing.Size(429, 158);
            this.FileListPanel.TabIndex = 4;
            // 
            // labelTitleFileName
            // 
            this.labelTitleFileName.AutoSize = true;
            this.labelTitleFileName.Location = new System.Drawing.Point(15, 27);
            this.labelTitleFileName.Name = "labelTitleFileName";
            this.labelTitleFileName.Size = new System.Drawing.Size(52, 13);
            this.labelTitleFileName.TabIndex = 5;
            this.labelTitleFileName.Text = "File name";
            this.labelTitleFileName.Visible = false;
            // 
            // labelTitlePages
            // 
            this.labelTitlePages.AutoSize = true;
            this.labelTitlePages.Location = new System.Drawing.Point(345, 27);
            this.labelTitlePages.Name = "labelTitlePages";
            this.labelTitlePages.Size = new System.Drawing.Size(37, 13);
            this.labelTitlePages.TabIndex = 5;
            this.labelTitlePages.Text = "Pages";
            this.labelTitlePages.Visible = false;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 222);
            this.Controls.Add(this.labelTitlePages);
            this.Controls.Add(this.labelTitleFileName);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.FileListPanel);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(445, 260);
            this.Name = "MainForm";
            this.Text = "PDFBinder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.inputListBox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.inputListBox_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addFileButton;
        private System.Windows.Forms.ToolStripButton moveUpButton;
        private System.Windows.Forms.ToolStripButton moveDownButton;
        private System.Windows.Forms.ToolStripButton removeButton;
        private System.Windows.Forms.ToolStripButton completeButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog addFileDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.ToolStripButton RemoveAllButton;
        private System.Windows.Forms.FlowLayoutPanel FileListPanel;
        private System.Windows.Forms.Label labelTitleFileName;
        private System.Windows.Forms.Label labelTitlePages;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

