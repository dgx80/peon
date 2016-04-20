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
    public partial class SettinsAdvancedForm : Form
    {
        public SettinsAdvancedForm(string path)
        {
            InitializeComponent();
            textBox1.Text = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                string s = folderBrowserDialog1.SelectedPath;
                string peon = "Peon";
                string end = s.Substring(s.Length - 1, 1);
                if ( end != "\\")
                {
                    s += "\\";
                }
                if (!s.Contains(peon))
                {
                    s += peon;
                    s += "\\";
                }
                else
                {
                    int n = s.LastIndexOf(peon)+peon.Length+1;
                    if (n < s.Length)
                    {
                        s = s.Remove(n);
                    }
                }

                textBox1.Text = s;
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        public string UserPath{ get{return textBox1.Text;}}
    }
}
