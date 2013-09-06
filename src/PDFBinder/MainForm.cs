using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PDFBinder
{
    public partial class MainForm : Form
    {

        public List<Control> selectedItems = new List<Control>();
        public Control selectionPendingItem = null;
        public Boolean selectionMode = true;


        public void ResetItemSelection()
        {
            selectionPendingItem = null;
            selectionMode = true;
        }
        public bool SelectRangeEnd(Control endOfSelectionItem) 
        {
            if (null != selectionPendingItem) {
                int start = Math.Min(FileListPanel.Controls.IndexOf(selectionPendingItem), FileListPanel.Controls.IndexOf(endOfSelectionItem));
                int end =  Math.Max(FileListPanel.Controls.IndexOf(selectionPendingItem), FileListPanel.Controls.IndexOf(endOfSelectionItem));

                for (int i = start; i <= end ; i++) {
                    ((PdfItem)FileListPanel.Controls[i]).Selected = selectionMode;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
        }
        
        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        private Dictionary<String,Byte[]> passwords = new Dictionary<string,byte[]>();

        private byte[] currentPassword = null;

        public void AddInputFile(string file)
        {
            Boolean tryAgain = true;
            Boolean newPassword = false;
            while (tryAgain || newPassword) {
                newPassword = false;
                switch (Combiner.TestSourceFile(file, currentPassword))
                {
                    case Combiner.SourceTestResult.Unreadable:
                        MessageBox.Show(string.Format("File could not be opened as a PDF document:\n\n{0}", file), "Illegal file type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case Combiner.SourceTestResult.Protected:
                        MessageBox.Show(string.Format("PDF document does not allow copying:\n\n{0}", file), "Permission denied", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        break;
                    case Combiner.SourceTestResult.Ok:
                        // inputListBox.Items.Add(file);

                        PdfItem pi = new PdfItem();
                        pi.BinderParentForm = this;
                        pi.FileName = file;
                        pi.PageDescriptor = "";
                        pi.Selected = false;
                        FileListPanel.Controls.Add(pi);

                        passwords[file] = currentPassword;
                        break;
                    case Combiner.SourceTestResult.PasswordRequired:
                        if (tryAgain)
                        {
                            PasswordEntry pe = new PasswordEntry();
                            pe.Prompt = "Password required for file: \n" + file;
                            DialogResult dr = pe.ShowDialog(this);
                    
                            if (dr == System.Windows.Forms.DialogResult.OK) {
                                currentPassword = StrToByteArray(pe.Password);
                                newPassword = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Password supplied didn't open PDF document:\n\n{0}", file), "Bad password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        break;
                }
                tryAgain = false;
            }
        }

        public void UpdateUI()
        {
            if (FileListPanel.Controls.Count < 1)
            {
                completeButton.Enabled = false;
                helpLabel.Text = "Drop PDF-documents in the box above, or choose \"add document\" from the toolbar";
                RemoveAllButton.Enabled = false;
            }
            else
            {
                completeButton.Enabled = true;
                helpLabel.Text = "Click the \"bind!\" button when you are done adding documents";
                RemoveAllButton.Enabled = true;
            }

            if (selectedItems.Count < 1)
            {
                removeButton.Enabled = moveUpButton.Enabled = moveDownButton.Enabled = false;
            }
            else
            {
                removeButton.Enabled = true;
                moveUpButton.Enabled = GetLowestSelectedIndex() > 0;
                moveDownButton.Enabled = GetHighestSelectedIndex() < FileListPanel.Controls.Count - 1;
            }
        }

        private int GetHighestSelectedIndex()
        {
            int max = -1;

            foreach (PdfItem pi in selectedItems)
            {
                int thisIndex = FileListPanel.Controls.IndexOf(pi);
                if (thisIndex > max)
                {
                    max = thisIndex;
                }
            }
            return max;
        }
        private int GetLowestSelectedIndex()
        {
            int min = FileListPanel.Controls.Count - 1;

            foreach (PdfItem pi in selectedItems)
            {
                int thisIndex = FileListPanel.Controls.IndexOf(pi);
                if (thisIndex < min)
                {
                    min = thisIndex;
                }
            }
            return min;
        }

        private void inputListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, false) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void inputListBox_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[]) e.Data.GetData(DataFormats.FileDrop);
            Array.Sort(fileNames);

            foreach (var file in fileNames)
            {
                AddInputFile(file);
            }

            UpdateUI();
        }

        private void combineButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var combiner = new Combiner(saveFileDialog.FileName))
                {
                    progressBar.Visible = true;
                    this.Enabled = false;

                    int i = 0;
                    foreach (PdfItem pi in FileListPanel.Controls)
                    {
                        byte[] pw ;
                        bool found_pw = passwords.TryGetValue(pi.FileName, out pw);
                        if (found_pw) {
                            combiner.AddFile(pi.FileName, pw,  pi.PageDescriptor );
                        } else {
                            combiner.AddFile(pi.FileName, new byte[0], pi.PageDescriptor);
                        }
                        i += 1;
                        progressBar.Value = (int)(((i) / (double)FileListPanel.Controls.Count) * 100);
                    }

                    this.Enabled = true;
                    progressBar.Visible = false;
                }

                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            if (addFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in addFileDialog.FileNames)
                {
                    AddInputFile(file);
                }
                UpdateUI();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            RemoveSelected();
        }

        private void moveItemButton_Click(object sender, EventArgs e)
        {
            foreach (PdfItem pi in selectedItems)
            {
                int index = FileListPanel.Controls.IndexOf(pi);
                if (sender == moveUpButton)
                    index--;
                if (sender == moveDownButton)
                    index++;
                FileListPanel.Controls.SetChildIndex(pi, index);
            }
            ResetItemSelection();
            UpdateUI();
        }

        public void RemoveSelected() {

            int indexOfTopSelected = GetLowestSelectedIndex();
            if (FileListPanel.HasChildren) 
            {
                RemoveItems(selectedItems.ToArray());
            }
            selectedItems.Clear();

            if (FileListPanel.Controls.Count > indexOfTopSelected)
            {
                ((PdfItem)FileListPanel.Controls[indexOfTopSelected]).Selected = true;
            }

            ResetItemSelection();
            UpdateUI();
        }

        void RemoveItems(Control[] removeList)
        {
            foreach (Control pi in removeList)
            {
                FileListPanel.Controls.Remove(pi);
            }
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            if (FileListPanel.HasChildren) 
            {
                Control[] removeList = new Control[FileListPanel.Controls.Count];
                FileListPanel.Controls.CopyTo(removeList,0);
                RemoveItems(removeList);
            }
            selectedItems.Clear();
            ResetItemSelection();
            UpdateUI();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(label2, "Pages can be left blank for all, or a list of pages. e.g. \"8,16-22,45\"");
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.KeyData == Keys.Delete))
            {
                return;
            }
            RemoveSelected();
        }
    }
}