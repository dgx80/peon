using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeonLib.forms
{
    public partial class inputForm : Form
    {
        public inputForm(string title, string msg)
        {
            InitializeComponent();
            label1.Text = msg;
            this.Text = title;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        public string Value
        {
            get { return textBox1.Text; }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void inputForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
