using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PDFBinder
{
    public partial class PdfItem : UserControl
    {
        public PdfItem()
        {
            InitializeComponent();

            BinderParentForm = (MainForm)ParentForm;
        }

        private MainForm binderParentForm;
        public MainForm BinderParentForm
        {
            get { return binderParentForm; }
            set { binderParentForm = value; }
        }
        
        private String fileName;
        public String FileName { 
            get { return fileName;}
            set { fileName = value; this.FileNameLabel.Text = fileName; this.toolTip1.SetToolTip(this.FileNameLabel, fileName); } 
        }

        private String pageDescriptor;
        public String PageDescriptor { 
            get { return pageDescriptor;}
            set { pageDescriptor = value; this.PageSelection_textBox.Text = pageDescriptor;  }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                this.selected = value;
                if (this.selected)
                {
                    this.BackColor = SystemColors.Highlight;
                    this.ForeColor = SystemColors.HighlightText;
                    BinderParentForm.selectedItems.Add(this);
                }
                else
                {
                    this.BackColor = SystemColors.Control;
                    this.ForeColor = SystemColors.ControlText;
                    if (BinderParentForm.selectedItems.Contains(this))
                    {
                        BinderParentForm.selectedItems.Remove(this);
                    }
                }
            }
        }

        private void PdfItem_Click(object sender, EventArgs e)
        {
            bool handled = false;
            if ((ModifierKeys & Keys.Control) == Keys.Control) // keydown 
            {
                this.Selected = !this.Selected;
                handled = true;
                // not handling control+shift, getting carried away.
            }
            else if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                // the other end of a selection, perhaps.
                handled = BinderParentForm.SelectRangeEnd(this);
            }

            if (!handled)
            {
                // no keyboard modifier, just select
                List<Control> tmpSelected = new List<Control>();
                tmpSelected.AddRange(BinderParentForm.selectedItems);
                foreach (PdfItem pi in tmpSelected)
                {
                    pi.Selected = false;
                }
                this.Selected = true;

                BinderParentForm.selectionMode = true;
                binderParentForm.selectionPendingItem = this;
            }
            BinderParentForm.UpdateUI();
        }

        private void PageSelection_textBox_TextChanged(object sender, EventArgs e)
        {
            PageDescriptor = PageSelection_textBox.Text;
        }
    }
}
