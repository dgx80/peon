using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;
using PeonLib.Data;
using System.Windows.Forms;

namespace PeonLib
{
    public class gSpreadsheet
    {
        #region Definitions

        private class pColumnEntry
        {
            public pColumnEntry()
            {
                nIndex = 0;
                sName = "";
            }
            public int nIndex;
            public string sName;
        }
        
        #endregion

        #region Members

        private SpreadsheetQuery query = new SpreadsheetQuery();
        private AtomLink m_oLink = null;
        private gAccount m_oAccount = null;
        private WorksheetEntry m_oWorksheet = null;
        private ListFeed m_oListFeed = null;
        private WorksheetFeed m_oWorksheetFeed = null;
        private pColumnEntry m_oCurrentColumn = new pColumnEntry();

        private pList m_oSelectedList = new pList("SelectedCol");
        private pDataTable m_oValidationDataTable = new pDataTable("Validation");

        #endregion

        #region Constructors 
        public gSpreadsheet( string sName)
        {
            m_oAccount = new gAccount("jeanpierre.bouchard80", "ragoutdeferblanc");
            
            query.Title = sName;
            SpreadsheetFeed feed = m_oAccount.Service.Query(query);

            if (feed.Entries.Count < 1)
                return;
            m_oLink = feed.Entries[0].Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

        }
        #endregion

        #region Spreadsheet Reader
        
        public void SelectWorkSheet(string sSheetName)
        {
            WorksheetQuery worksheetQuery = new WorksheetQuery(m_oLink.HRef.ToString());
            worksheetQuery.Title = sSheetName;
            m_oWorksheetFeed = m_oAccount.Service.Query(worksheetQuery);

            m_oWorksheet = (WorksheetEntry)m_oWorksheetFeed.Entries[0];
            AtomLink listFeedLink = m_oWorksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            m_oListFeed = m_oAccount.Service.Query(listQuery);
            
        }
        
        public void ReadColumn(string sColName)
        {
            if (m_oWorksheetFeed == null)
                throw new System.Exception("The sheet is unselected");
            if (m_oWorksheetFeed.Entries.Count != 1)
                return;

            string sChecked = "checked";
            m_oCurrentColumn.sName = sColName;
            m_oCurrentColumn.nIndex = ObtainIndexByName(sColName);

            m_oSelectedList.StartNewList(sColName);
            m_oValidationDataTable.Reset();
            m_oValidationDataTable.Columns.Add(new System.Data.DataColumn(sChecked, typeof(bool)));
            m_oValidationDataTable.AddColumn(sColName);

            foreach (ListEntry worksheetRow in m_oListFeed.Entries)
            {
                string scanData = worksheetRow.Elements[m_oCurrentColumn.nIndex].Value.ToString();
                m_oSelectedList.PushBack(scanData);

                System.Data.DataRow dr = m_oValidationDataTable.NewRow();
                dr[sColName] = scanData;
                dr[sChecked] = false;
                m_oValidationDataTable.Rows.Add(dr);
            }
        }
        
        private string ReadCell(string sColName, ListEntry worksheetRow)
        {
            return worksheetRow.Elements[1].Value.ToString();
        }
        
        #endregion

        #region Spreadsheet Writter

        private void RemoveRow(int nIndex)
        {
            m_oListFeed.Entries[nIndex].Delete();
        }
        #endregion

        #region CallBack
        
        public void OnChecked(int nIndex, bool bChecked, string sCellItem)
        {
            if (nIndex >= m_oListFeed.Entries.Count)
                return;

            if ((bool)m_oValidationDataTable.Rows[nIndex][0] != bChecked)
            {
                if((string)m_oValidationDataTable.Rows[nIndex][1] == sCellItem)
                    m_oValidationDataTable.Rows[nIndex][0] = bChecked;
            }

        }
        
        public void OnSubmit()
        {
            pList oList = new pList();
            oList.StartNewList("items");

            foreach (System.Data.DataRow row in m_oValidationDataTable.Rows)
            {
                if ((bool)row[0] == true)
                {
                    oList.PushBack(row[1].ToString());
                }
            }

            if (oList.Rows.Count > 0)
            {
                forms.SubmitForm oSub = new forms.SubmitForm();
                oSub.AddList(oList);

                if (oSub.ShowDialog() == DialogResult.OK)
                {
                    int i = 0;

                    foreach (System.Data.DataRow row in m_oValidationDataTable.Rows)
                    {
                        if ((bool)row[0] == true)
                        {
                            RemoveRow(i);
                        }
                        i++;
                    }
                    System.Windows.Forms.MessageBox.Show("items cleared:)");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("action canceled");
                }
            }
            ReadColumn(m_oCurrentColumn.sName);
        }
        
        #endregion

        #region Data Reader

        #endregion

        #region Data Writter

        #endregion

        #region Utility
        
        private int ObtainIndexByName(string sName)
        {

            CellQuery query = new CellQuery(m_oWorksheet.CellFeedLink.ToString());

            CellFeed feed = m_oAccount.Service.Query(query);
            AtomEntryCollection entries = feed.Entries;

            for (int i = 0; i < (int)feed.Entries.Count; i++)
            {
                CellEntry cell = entries[i] as CellEntry;

                if (cell.InputValue == sName)
                {
                    return i;
                }
            }
//            throw new Exception("Invalid collumn name");
            return -1;
        }
        private void LogList()
        {
            m_oSelectedList.WriteXml("H:\\Dgpx\\DgxCode\\Peon\\SelectedList.xml");
            m_oValidationDataTable.WriteXml("H:\\Dgpx\\DgxCode\\Peon\\validationLog.xml");
        }
        #endregion

        #region Properties

        public pList SelectedList
        {
            get
            {
                return m_oSelectedList;
            }
        }
        #endregion
        //now we must to create a backup image from the DB6

    }
}
