using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeonLib.script
{
    public class pAdmin : pBase
    {
        public pAdmin()
            : base()
        {
            mName = definitions.nameconst.pShortcuts;
        }

        public void ExportFile(Object.User u, Object.Profile p)
        {
            mPathKey = p.PathKey + mName;

            string s = ObtainTemplateKey();
            base.ExportKeyFile(s);
        }
    }
}
