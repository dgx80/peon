using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeonLib.Object
{
    public class ShortcutInfo
    {
        private string msUrl = "";
        private string msHotkey = "";
        private string msName = "";
        private bool isCmd = false;

        public ShortcutInfo(){}

        public ShortcutInfo(string url, string hotkey, string name)
        {
            Url = url;
            msHotkey = hotkey;
            msName = name;
        }
        public void SetData( ShortcutInfo other)
        {
            Url = other.Url;
            Hotkey = other.Hotkey;
            Name = other.Name;
            isCmd = other.isCmd;
        }
        public string Url
        {
            get { return msUrl; }
            set
            {
                int n = value.IndexOf("cmd:"); 
                if (n != 0)
                {
                    msUrl = value;
                    isCmd = false;
                }
                else
                {
                    isCmd = true;
                    msUrl = value.Substring(4);
                }
            }
        }
        public bool IsCmd
        {
            get { return isCmd; }
        }
        public string Hotkey
        {
            get { return msHotkey; }
            set { msHotkey = value; }
        }
        public string Name
        {
            get { return msName; }
            set { msName = value; }
        }

    }
}
