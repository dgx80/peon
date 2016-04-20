using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeonLib.COM
{
    partial class listbox : System.Windows.Forms.ListBox
    {
        private File.textlist mFile = null;

        public listbox()
        {
            InitializeComponent();
        }

        public listbox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        
        public void ReadFile(string filename)
        {
            mFile = new PeonLib.File.textlist(filename);
            RefreshListBox();
        }
        private void RefreshListBox()
        {
            this.Items.Clear();

            foreach (string s in mFile.Items)
            {
                this.Items.Add(s);
            }
        }
        public void addValue(string val)
        {
            foreach (string s in mFile.Items)
            {
                if (s == val)
                {
                    return;
                }
            }
            mFile.Items.Add(val);
            mFile.Sort();
            RefreshListBox();
        }
        public void RemoveSelectedValue()
        {
            int n = this.SelectedIndex;
            if (n >= 0 && n < mFile.Items.Count)
            {
                removeValue(n);
            }
        }
        public void SaveFile()
        {
            mFile.WriteList();
            MessageBox.Show("The file is saved");
        }
        private void removeValue(int nCase)
        {
            mFile.Items.RemoveAt(nCase);
            this.Items.RemoveAt(nCase);
        }
    }
}
