using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PDFBinder
{
    public partial class PasswordEntry : Form
    {

        String description;
        String dialogInvoker;

        public PasswordEntry()
        {
            InitializeComponent();

        }

        public String Prompt
        {
            set { label1.Text = value; }
        }

        public string Password
        {
            get { return textBoxPassword.Text; }
            set { textBoxPassword.Text = value; }
        }

        private void PasswordEntry_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
