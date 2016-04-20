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
    public partial class SubmitForm : Form
    {
        public SubmitForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void AddList(Data.pList oList)
        {
            //listView1.Clear();

            
            // Create columns for the items and subitems.
            listView1.Columns.Add("Items",268);

            foreach( System.Data.DataRow row in oList.Rows)
            {
                ListViewItem items = new ListViewItem(row[0].ToString(), 0);
                listView1.Items.Add(items);
            }

        }
    }
}
