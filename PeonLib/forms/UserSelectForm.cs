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
    public partial class UserSelectForm : Form
    {
        private int m_nElem = 0;
        bool combotoken = true;
        bool gridtoken = true;
        
        public UserSelectForm(string title)
        {
            InitializeComponent();
            this.Text = title;
            dataGridView1.ReadOnly = true;
        }
        public void LoadFile(PeonLib.File.textlist f)
        {
            comboBox1.Items.Clear();
            Data.IDList l = new PeonLib.Data.IDList();
            m_nElem = 0;
            foreach (string s in f.Items)
            {
                if (s != "")
                {
                    comboBox1.Items.Add(m_nElem);
                    l.PushBack(s);
                    m_nElem++;
                }
            }
            
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = l;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            
            comboBox1.SelectedIndex = 0;
        }
        public void LoadShortcutInfo(List<Object.ShortcutInfo> lst)
        {
            Data.IDList l = new PeonLib.Data.IDList();
            m_nElem = 0;
            comboBox1.Items.Clear();
            foreach (Object.ShortcutInfo i in lst)
            {
                if (i.IsCmd == false)
                {
                    comboBox1.Items.Add(m_nElem);
                    m_nElem++;
                    l.PushBack(i.Name);
                }
            }
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = l;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            comboBox1.SelectedIndex = 0;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int n = ID;
            if (n >= 0 && n < m_nElem)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public int ID
        {
            
            get 
            {
                return comboBox1.SelectedIndex; 
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;

            if (n < 0 || n >= m_nElem)
            {
                n = 0;
            }
            comboBox1.SelectedIndex = n;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combotoken)
            {
                combotoken = false;
                gridtoken = false;
                int n = comboBox1.SelectedIndex;
                if (n < dataGridView1.Rows.Count && n >= 0)
                {
                    if (dataGridView1.Rows[n].Selected == false)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            dataGridView1.Rows[i].Selected = false;
                        }

                        dataGridView1.Rows[n].Selected = true;
                    }
                }
                combotoken = true;
                gridtoken = true;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (gridtoken)
                {
                    gridtoken = false;
                    combotoken = false;
                    if (dataGridView1.CurrentRow != null)
                    {
                        int n = dataGridView1.CurrentRow.Index;

                        if (n < 0 || n >= m_nElem)
                        {
                            n = 0;
                        }
                        if (comboBox1.SelectedIndex != n)
                        {
                            comboBox1.SelectedIndex = n;
                        }
                    }
                    gridtoken = true;
                    combotoken = true;
                }
            }
        }



    }
}
