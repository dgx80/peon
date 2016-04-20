using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeonLib.Object
{
    public class ProfileData
    {
        private List<Object.ShortcutInfo> m_lShortcut = new List<ShortcutInfo>();
        private List<Object.ShortcutInfo> m_lWeblink = new List<ShortcutInfo>();

        public ProfileData()
        { }
        public List<ShortcutInfo> Shortcuts
        {
            set
            {
                m_lShortcut.Clear();

                foreach (ShortcutInfo s in value)
                {
                    ShortcutInfo o = new ShortcutInfo();
                    o.SetData(s);
                    m_lShortcut.Add(o);
                }
            }
            get
            {
                List<ShortcutInfo> l = new List<ShortcutInfo>();
                foreach (ShortcutInfo s in m_lShortcut)
                {
                    ShortcutInfo o = new ShortcutInfo();
                    o.SetData(s);
                    l.Add(o);
                }
                return l;
            }
        }
        public List<ShortcutInfo> Weblinks
        {
            set
            {
                m_lWeblink.Clear();

                foreach (ShortcutInfo s in value)
                {
                    ShortcutInfo o = new ShortcutInfo();
                    o.SetData(s);
                    m_lWeblink.Add(o);
                }
            }
            get
            {
                List<ShortcutInfo> l = new List<ShortcutInfo>();
                foreach (ShortcutInfo s in m_lWeblink)
                {
                    ShortcutInfo o = new ShortcutInfo();
                    o.SetData(s);
                    l.Add(o);
                }
                return l;
            }
        }
       
    }
}
