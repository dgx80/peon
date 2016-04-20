using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeonLib.Data
{
    public class IDList : pDataTable
    {
        public IDList()
            : base()
        {
            StartNewList();
        }
        private void StartNewList()
        {
            Reset();
            AddColumn("ID");
            AddColumn("Value");
        }
        public void PushBack(string sValue)
        {
            System.Data.DataRow dr = NewRow();
            dr[0] = this.Rows.Count;
            dr[1] = sValue;
            Rows.Add(dr);
        }
    }
}
