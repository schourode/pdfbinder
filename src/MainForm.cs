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
        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (inputListBox.Items.Count < 2)
            {
                completeButton.Enabled = false;
                helpLabel.Text = "Drop PDF-documents in the box above, or choose \"add document\" from the toolbar";
            }
            else
            {
                completeButton.Enabled = true;
                helpLabel.Text = "Click the \"bind!\" button when you are done adding documents";
            }

            if (inputListBox.SelectedIndex < 0)
            {
                removeButton.Enabled = moveUpButton.Enabled = moveDownButton.Enabled = false;
            }
            else
            {
                removeButton.Enabled = true;
                moveUpButton.Enabled = (inputListBox.SelectedIndex > 0);
                moveDownButton.Enabled = (inputListBox.SelectedIndex < inputListBox.Items.Count - 1);
            }
        }

        private void AddInputFile(string file)
        {
            switch (Combiner.TestSourceFile(file))
            {
                case Combiner.SourceTestResult.Unreadable:
                    MessageBox.Show(string.Format("File could not be opened as a PDF document:\n\n{0}", file), "Illegal file type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case Combiner.SourceTestResult.Protected:
                    MessageBox.Show(string.Format("PDF document does not allow copying:\n\n{0}", file), "Permission denied", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    break;
                case Combiner.SourceTestResult.Ok:
                    inputListBox.Items.Add(file);
                    break;
            }
        }

        private void inputListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, false) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void inputListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                AddInputFile(file);
            }

            UpdateUI();
        }

        private void combineButton_Click(object sender, EventArgs e)
        {
            using (Combiner combiner = new Combiner())
            {
                progressBar.Visible = true;
                this.Enabled = false;

                for (int i = 0; i < inputListBox.Items.Count; i++)
                {
                    combiner.AddFile((string)inputListBox.Items[i]);
                    progressBar.Value = (int) (((i + 1) / (double)inputListBox.Items.Count) * 100);
                }

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    combiner.SaveCombinedDocument(saveFileDialog.FileName);
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }

                this.Enabled = true;
                progressBar.Visible = false;
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

        private void inputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            inputListBox.Items.Remove(inputListBox.SelectedItem);
        }

        private void moveItemButton_Click(object sender, EventArgs e)
        {
            object dataItem = inputListBox.SelectedItem;
            int index = inputListBox.SelectedIndex;

            if (sender == moveUpButton)
                index--;
            if (sender == moveDownButton)
                index++;

            inputListBox.Items.Remove(dataItem);
            inputListBox.Items.Insert(index, dataItem);

            inputListBox.SelectedIndex = index;
        }
    }
}