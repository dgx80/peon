using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PeonLib.script
{
    public class pBase
    {
        public string mName = "";
        public string mPathKey = "";
        public string mPathData = "";

        public pBase()
        { }
        public void ExportKeyFile(string buf)
        {
            string filename = mPathKey + "." + definitions.extension.ahk;
            WriteFile(filename,buf);
        }
        public void ExportDataFile(string buf)
        {
            string filename = mPathData + "." + definitions.extension.txt;
            WriteFile(filename,buf);
        }
        public string ObtainTemplateKey()
        {
            string filename = definitions.path.TemplatePref + mName + "." + definitions.extension.ahk;
            return ReadFile(filename);
        }
        public string ObtainTemplateData()
        {
            string filename = definitions.path.TemplatePref + mName + "." + definitions.extension.txt;
            return ReadFile(filename); ;
        }
        public string ObtainTemplateScript()
        {
            string filename = definitions.path.TemplatePref + mName;
            return ReadFile(filename); ;
        }

        #region Private
        private void WriteFile(string filename, string buf)
        {

            System.IO.TextWriter fw = new StreamWriter(filename);
            fw.Write(buf);
            fw.Close();
        }
        private string ReadFile(string filename)
        {
            string buf="";

            if (System.IO.File.Exists(filename))
            {
                System.IO.TextReader fr = new StreamReader(filename);
                buf = fr.ReadToEnd();
                fr.Close();
            }
            return buf;
        }
        #endregion
    }
}
