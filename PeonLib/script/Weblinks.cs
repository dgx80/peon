using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PeonLib.script
{
    public class Weblinks
    {
        private FileInfo m_oFileInfo = null;

        public Weblinks(string sName)
        {
            m_oFileInfo = new FileInfo(sName + ".ahk");
        }
    }
}
