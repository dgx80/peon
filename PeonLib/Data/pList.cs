using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeonLib.Data
{
    public class pList : pDataTable
    {
        #region Constructors

        public pList(string sName)
            : base(sName)
        {
        }
        public pList()
            : base()
        {
        }
        #endregion

        public void StartNewList(string sName)
        {
            Reset();
            AddColumn(sName);
        }
        public void PushBack(string sValue)
        {
            System.Data.DataRow dr = NewRow();
            dr[Columns[0].ColumnName] = sValue;
            Rows.Add(dr);
        }
    }
}
