using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PeonLib.Data
{    
    public class pDataTable : DataTable
    {
        #region Constructors

        public pDataTable(string sName)
            : base(sName)
        {

        }
        public pDataTable()
            : base()
        {

        }
        #endregion

        #region Reading

        #endregion

        #region Writting

        public void AddColumn(string sName)
        {
            Columns.Add(new DataColumn(sName, typeof(string)));
        }

        #endregion

        #region Validation

        public bool ColumnExists(string ColumnName)
        {
            bool bRet = false;
            foreach (DataColumn col in Columns)
            {
                if (col.ColumnName == ColumnName)
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
        }
        #endregion

    }

}
