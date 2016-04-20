using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace PeonLib.File
{
    public class textlist      
    {
        private System.IO.FileInfo inf;
        private List<string> mList = new List<string>();
   
        public textlist(string name)
        {
            inf = new System.IO.FileInfo(name);
            LoadList(name);
        }

        private void LoadList(string name)
        {
            string line = null;
            if (System.IO.File.Exists(name))
            {
                System.IO.TextReader readFile = new StreamReader(name);
                while (true)
                {
                    line = readFile.ReadLine();

                    if (line != null)
                    {
                        mList.Add(line);
                    }
                    else
                    {
                        break;
                    }
                }
                readFile.Close();
                readFile = null;
            }
        }
        public void WriteList()
        {
            System.IO.TextWriter writeFile = new StreamWriter(inf.FullName);

            foreach (string s in mList)
            {
                writeFile.WriteLine(s);
            }
            writeFile.Close();
        }
        public void Sort()
        {
            mList.Sort();
        }
        public List<string> Items
        {
            get
            {
                return mList;
            }
        }
        public bool IsExist(string name)
        {
            foreach (string s in mList)
            {
                if (s == name)
                    return true;
            }
            return false;
        }
    }
}
