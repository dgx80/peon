using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Google.GData.Spreadsheets;


namespace PeonLib
{
    public class gAccount
    {
        private SpreadsheetsService m_oService;

        public gAccount(string sUser, string sPass)
        {
            m_oService = new SpreadsheetsService("Generic Spreadsheet-List-Capture");

            m_oService.setUserCredentials(sUser + "@gmail.com", sPass);
        }

        #region Properties

        public SpreadsheetsService Service
        {
            get
            {
                return m_oService;
            }
        }

        #endregion
    }
}
