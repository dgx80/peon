using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PeonLib;

namespace PeonLib.forms
{
    public partial class ListForm : Form
    {
        private gSpreadsheet m_oSpreadSheet = null;
        private bool m_bWorking = true;

        public ListForm(string sSpreadsheetName, string sWorksheetName, string sColumnHeaderName)
        {
            InitializeComponent();

            try
            {
                this.Text = sSpreadsheetName + " List";
                m_oSpreadSheet = new gSpreadsheet( sSpreadsheetName);
                m_oSpreadSheet.SelectWorkSheet(sWorksheetName);
                m_oSpreadSheet.ReadColumn(sColumnHeaderName);

                dataGridView1.DataSource = m_oSpreadSheet.SelectedList;
                dataGridView1.Columns[1].ReadOnly = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                m_bWorking = false;        
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            m_oSpreadSheet.OnSubmit();
            Close();
        }
        

        private void OnCheckList()
        {
            if (dataGridView1.CurrentCell == null)
                return;
            int nIndex = dataGridView1.CurrentCell.RowIndex;
            
            if (m_oSpreadSheet != null)
            {
                bool bCheck = (bool)dataGridView1.Rows[nIndex].Cells[0].FormattedValue;
                string sItem = dataGridView1.Rows[nIndex].Cells[1].FormattedValue.ToString();

                m_oSpreadSheet.OnChecked(nIndex, bCheck,sItem);
            }
            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (m_bWorking == false)
            {
                Close();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            OnCheckList();
        }
       
    }
}
