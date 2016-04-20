using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeonLib.script
{
    public class pInterface : pBase
    {
        
        public pInterface()
        :base()
        {
            mName = definitions.nameconst.pInterface;
        }

        public void ExportFile(Object.User u, Object.Profile p,ref List<compte> lTop5)
        {
            mPathKey = p.PathKey + mName;
            mPathData = p.PathData + mName;

            string s = ObtainTemplateKey();
            
            base.ExportKeyFile(s);

            s = ObtainTemplateData();
            for (int i = 1; i <= 5; i++)
            {
                string sTarget = "<user" + i.ToString() + ">";
                if (i <= lTop5.Count)
                {
                    string sLine = i.ToString() + "," + lTop5[i - 1].sUser + ":" + lTop5[i - 1].sProfile;
                    s = s.Replace(sTarget, sLine);
                }
                else
                {
                    sTarget += "\r\n";
                    s = s.Replace(sTarget, "");
                }
            } 
            base.ExportDataFile(s);
        }
    }
}
