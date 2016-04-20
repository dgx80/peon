using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PeonLib.script
{
    public class Weblink :pBase
    {
        public Weblink()
        :base()
        {
            mName = definitions.nameconst.pWeblink;
        }

        public void ExportFile(Object.User u, Object.Profile p, List<Object.ShortcutInfo> lst)
        {
            mPathKey = p.PathKey + mName;
            mPathData = p.PathData + mName;

            string s = ObtainTemplateKey();

            foreach (Object.ShortcutInfo i in lst)
            {
                s += ObtainScriptFromShortcutInfo(i);
            }
            base.ExportKeyFile(s);

            s = ObtainTemplateData();

            foreach (Object.ShortcutInfo i in lst)
            {
                s += i.Hotkey;
                s += ",";
                s += i.Name;
                s += "\r\n";
            }

            base.ExportDataFile(s);
        }
        private string ObtainScriptFromShortcutInfo(Object.ShortcutInfo inf)
        {
            string s = "\r\n";
            s += inf.Hotkey;
            s += "::\r\n\r\n\t";
            if (inf.IsCmd)
            {
                s += definitions.launcher.PeonLauncher;
                s += "(\"";
            }
            else
            {
                s += "web(\"";
            }
            s += inf.Url;
            s += "\")\r\n\r\n";
            s += "return\r\n";
            return s;
        }
    }
}
