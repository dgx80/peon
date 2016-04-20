using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PeonLib.script
{
    public class PopupDlg : pBase
    {
        public PopupDlg() 
        :base()
        {
            mName = definitions.nameconst.popupDlg;
        }

        public void ExportFile(Object.User u, Object.Profile p)
        {
            mPathKey = p.PathUI +  mName;

            string s = ObtainTemplateKey();
            s = s.Replace("<name>", u.NAME);
            s = s.Replace("<profile>", p.Name);
            base.ExportKeyFile(s);
        }
    }
}
