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
    public partial class hkForm : Form
    {
        public hkForm(string path,string sQuestion,bool web)
        {
            InitializeComponent();
            label_question.Text = sQuestion;
            textBox_path.Text = path;
            comboBox1.SelectedIndex = 0;

            if (web)
            {
                btn_folder.Visible = false;
                btn_file.Visible = false;
            }
        }
        public string infHotkey
        {
            get { return comboBox1.SelectedItem.ToString(); }
            set 
            {
                for (int i = 0; i < comboBox1.Items.Count;i++ )
                {
                    if (value == comboBox1.Items[i].ToString())
                    {
                        comboBox1.SelectedItem = comboBox1.Items[i];
                        break;
                    }
                }
            }
        }
        public string infUrl
        {
            get { return textBox_path.Text; }
            set { textBox_path.Text = value; }
        }
        public string infName
        {
            get { return textBox_name.Text; }
            set { textBox_name.Text = value; }
        }
        public void RemoveHotkeyChoice(string hk)
        {
            comboBox1.Items.Remove(hk);
            comboBox1.SelectedIndex = 0;
        }
        private void brn_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btn_folder_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                textBox_path.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btn_file_Click(object sender, EventArgs e)
        {
            string s = "";
            s = textBox_path.Text;
            if (s != "")
            {
                string sOld = "/";
                string sNew = "\\";

                s = s.Replace(sOld,sNew);
                openFileDialog1.InitialDirectory =  s;
            }
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                textBox_path.Text = openFileDialog1.FileName;
            }
        }
    }
}
