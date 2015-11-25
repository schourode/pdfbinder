namespace PDFBinder
{
    partial class PdfItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.PageSelection_textBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoEllipsis = true;
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(19, 4);
            this.FileNameLabel.MaximumSize = new System.Drawing.Size(300, 0);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.FileNameLabel.Size = new System.Drawing.Size(35, 18);
            this.FileNameLabel.TabIndex = 0;
            this.FileNameLabel.Text = "label1";
            this.FileNameLabel.Click += new System.EventHandler(this.PdfItem_Click);
            // 
            // PageSelection_textBox
            // 
            this.PageSelection_textBox.Location = new System.Drawing.Point(320, 1);
            this.PageSelection_textBox.Name = "PageSelection_textBox";
            this.PageSelection_textBox.Size = new System.Drawing.Size(76, 20);
            this.PageSelection_textBox.TabIndex = 1;
            this.PageSelection_textBox.TextChanged += new System.EventHandler(this.PageSelection_textBox_TextChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
            // 
            // PdfItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PageSelection_textBox);
            this.Controls.Add(this.FileNameLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(400, 60);
            this.MinimumSize = new System.Drawing.Size(400, 20);
            this.Name = "PdfItem";
            this.Size = new System.Drawing.Size(398, 24);
            this.Click += new System.EventHandler(this.PdfItem_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.TextBox PageSelection_textBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
