using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PeonLib.script;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PeonLib.Object
{
    public class Profile
    {
        private string mPath = "";
        private string mPathAhk = "";
        private string mPathUI = "";
        private string mPathKey = "";
        private string mPathData = "";
        private string msName = "";
        private List<Object.ShortcutInfo> m_lShortcut = new List<ShortcutInfo>();
        private List<Object.ShortcutInfo> m_lWeblink = new List<ShortcutInfo>();
        private List<Object.ShortcutInfo> m_lClipboard = new List<ShortcutInfo>();
        private User m_oParentUser;

        public Profile(User u, string name)
        {
            SetUserAndName(u, name);
        }
        public string Name
        {
            get { return msName; }
        }
        public void LoadData()
        {
            UnloadData();
            ReadScriptData(definitions.path.DataShortcuts, m_lShortcut);
            ReadScriptData(definitions.path.DataWeblink, m_lWeblink);
            ReadScriptData(definitions.nameconst.DataClipboard, m_lClipboard);
        }
        private void UnloadData()
        {
            m_lShortcut.Clear();
            m_lWeblink.Clear();
            m_lClipboard.Clear();
        }
        public void DeleteWeblink()
        {
            forms.UserSelectForm f = new PeonLib.forms.UserSelectForm("Delete weblink");
            f.LoadShortcutInfo(m_lWeblink);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                int n = f.ID;
                if (m_lWeblink[n].IsCmd)
                {
                    return;
                }
                m_lWeblink.RemoveAt(n);

                UpdateScriptWeblink();
                TransferProfileInCurrentScript();
            }
        }
        public void DeleteShortcutFolder()
        {
            forms.UserSelectForm f = new PeonLib.forms.UserSelectForm("Delete shortcut");
            f.LoadShortcutInfo(m_lShortcut);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                int n = f.ID;
                if (m_lShortcut[n].IsCmd)
                {
                    return;
                }
                m_lShortcut.RemoveAt(n);

                UpdateScriptShortcut();
                TransferProfileInCurrentScript();
            }
        }
        public void ObtainProfileData(ref ProfileData d)
        {
            d.Shortcuts = m_lShortcut;
            d.Weblinks = m_lWeblink;
        }
        public void SetNewProfileData(ProfileData d)
        {
            m_lShortcut = d.Shortcuts;
            m_lWeblink = d.Weblinks;
        }
        public void AddWeblink()
        {
            SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();

            string filename = "www.";

            foreach (SHDocVw.InternetExplorer ie in shellWindows)
            {
                filename = ie.LocationURL;
                
                break;
            }
            int n = filename.IndexOf("file:///");
            if (n == 0)
            {
                filename = "www.";
            }
            ShortcutInfo inf = new ShortcutInfo();
            if (AskShortCutInfo( ref inf, "Enter new weblink",m_lWeblink,true))
            {
                m_lWeblink.Add(inf);
                SortShortcutInfo(m_lWeblink);
                UpdateScriptWeblink();
                TransferProfileInCurrentScript();
            }
            
        }
        public void AddShortcutFolder()
        {
            SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();

            string filename = "";

            foreach (SHDocVw.InternetExplorer ie in shellWindows)
            {
                if(ie.LocationURL!= "")
                {
                    filename = ie.LocationURL;
                }
            }
            if (shellWindows.Count > 0)
            {
                int n = filename.Length;
                if (n > 0)
                {
                    filename = filename.Replace("file:///","");
                    filename = filename.Replace("%20", " ");
                    filename = filename.Replace("%26", "&");
                }
                else
                {
                    filename = "";
                }
            }
            ShortcutInfo inf = new ShortcutInfo();
            inf.Url = filename;
            while (!ShortcutAskingProcess(ref inf)) ;
        }

        private bool ShortcutAskingProcess(ref ShortcutInfo inf)
        {   
            bool r = true;
            if (AskShortCutInfo(ref inf, "Enter new shortcut", m_lShortcut,false))
            {

                if (System.IO.Directory.Exists(inf.Url) || System.IO.File.Exists(inf.Url))
                {
                    m_lShortcut.Add(inf);
                    SortShortcutInfo(m_lShortcut);
                    UpdateScriptShortcut();
                    TransferProfileInCurrentScript();
                    r = true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("this shortcut doesn't exist");
                    r = false;
                }
            }
            return r;
        }
        public void UpdateAllScript()
        {
            UpdateScriptShortcut();
            UpdateScriptWeblink();
        }
        public bool AskShortCutInfo(ref ShortcutInfo inf,string sQuestion,List<ShortcutInfo> lst,bool web)
        {   
            forms.hkForm f = new PeonLib.forms.hkForm(inf.Url, sQuestion,web);

            foreach (ShortcutInfo o in lst)
            {
                f.RemoveHotkeyChoice(o.Hotkey);
            }
            f.infHotkey = inf.Hotkey;
            f.infUrl = inf.Url;
            f.infName = inf.Name;

            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                inf.Hotkey = f.infHotkey;
                inf.Url = f.infUrl;
                inf.Name = f.infName;
                return true;
            }
            return false;
        }
        public void CreateStandardProfil()
        {
            UnloadData();
            System.IO.Directory.CreateDirectory(Path);
            System.IO.Directory.CreateDirectory(PathAhk);
            System.IO.Directory.CreateDirectory(PathUI);
            System.IO.Directory.CreateDirectory(PathKey);
            System.IO.Directory.CreateDirectory(PathData);

            script.PopupDlg d = new PopupDlg();
            d.ExportFile(m_oParentUser,this);

            List<compte> lCompte = new List<compte>();
            UpdateInterfaceTopList(ref lCompte);

            InitScriptData(); 
            //peon_admin
            ExportFileFromTemplate(definitions.nameconst.peonAdmin);

            //peon_user
            ExportFileFromTemplate(definitions.nameconst.peonUser);

            //peon_profile
            ExportFileFromTemplate(definitions.nameconst.peonProfile);

            
        }

        public void DestroyProfile()
        {
            System.IO.Directory.Delete(Path,true);
        }
        public void TransferProfileInCurrentScript()
        {
            string dKey = definitions.path.Ahk + definitions.nameconst.key;
            string dData = definitions.path.Ahk + definitions.nameconst.data;
            string dUi = definitions.path.Ahk + definitions.nameconst.ui;

            Folder.Delete(dKey, true);
            Folder.Delete(dData, true);
            Folder.Delete(dUi, true);

            Folder.Copy(PathKey, dKey);
            Folder.Copy(PathData, dData);
            Folder.Copy(PathUI, dUi);
        }
        public void UpdateInterfaceTopList(ref List<compte> lCompte)
        {
            script.pInterface i = new pInterface();
            i.ExportFile(m_oParentUser, this,ref lCompte);
        }
        
/*        public void TransferCurrentScriptInProfile()
        {
            string sKey = definitions.path.Ahk + definitions.nameconst.key;
            string sData = definitions.path.Ahk + definitions.nameconst.data;
            string sUi = definitions.path.Ahk + definitions.nameconst.ui;


            Folder.Delete(PathKey, true);
            Folder.Delete(PathData, true);
            Folder.Delete(PathUI, true);

            Folder.Copy(sKey, PathKey);
            Folder.Copy(sData, PathData);
            Folder.Copy(sUi, PathUI);
        }
*/
        #region properties
        public string Path
        {
            get { return mPath; }
        }
        public string PathAhk
        {
            get { return mPathAhk; }
        }
        public string PathUI
        {
            get { return mPathUI; }
        }
        public string PathKey
        {
            get { return mPathKey; }
        }
        public string PathData
        {
            get { return mPathData; }
        }
        #endregion

        #region private

        private void UpdateScriptShortcut()
        {
            script.Shortcuts s = new Shortcuts();
            s.ExportFile(m_oParentUser, this,m_lShortcut);
            WriteScriptData(definitions.path.DataShortcuts, m_lShortcut);
        }
        private void UpdateScriptWeblink()
        {
            script.Weblink w = new Weblink();
            w.ExportFile( m_oParentUser,this,m_lWeblink);
            WriteScriptData(definitions.path.DataWeblink, m_lWeblink);
        }
        private void UpdateScriptClipboard()
        {
/*            script.Clipboard w = new script.Clipboard();
            w.ExportFile(m_oParentUser, this, m_lClipboard);
            WriteScriptData(definitions.nameconst.DataClipboard, m_lClipboard);
 */       }

        private void InitScriptData()
        {
            //weblink
            string filename = definitions.path.DataWeblink;
            System.IO.StreamWriter sw = new StreamWriter(this.PathData+filename);
            
            script.pBase b = new pBase();
            b.mName = filename;

            string s = b.ObtainTemplateScript();
            sw.Write(s);
            sw.Close();

            //shortcuts
            filename = definitions.path.DataShortcuts;
            b.mName = filename;
            s = b.ObtainTemplateScript();
            sw = new StreamWriter(this.PathData + filename);
            sw.Write(s);
            sw.Close();

            //clipboard
            filename = definitions.nameconst.DataClipboard;
            sw = new StreamWriter(this.PathData + filename);
            sw.Close();

            LoadData();
            UpdateScriptShortcut();
            UpdateScriptWeblink();
            UpdateScriptClipboard();
        }
        private void ExportFileFromTemplate(string filename)
        {
            script.pBase b = new pBase();
            b.mName = filename;
            b.mPathData = this.PathData + filename;
            b.mPathKey = this.PathKey + filename;
            string buf = b.ObtainTemplateData();
            b.ExportDataFile(buf);
            buf = b.ObtainTemplateKey();
            b.ExportKeyFile(buf);
        }

        private void WriteScriptData(string filename, List<ShortcutInfo> lst)
        {
            System.IO.StreamWriter sw = new StreamWriter(this.PathData+filename);

            string s = "";

            foreach (ShortcutInfo i in lst)
            {
                s = i.Hotkey;
                s += ";";
                s += i.Name;
                s += ";";
                if (i.IsCmd)
                {
                    s += "cmd:";
                }
                s += i.Url;
                sw.WriteLine(s);
            }
            sw.Close();
        }
        private void SortShortcutInfo(List<ShortcutInfo> lst)
        {
            lst.Sort(delegate(ShortcutInfo o1, ShortcutInfo o2) 
            {
                if(o1.IsCmd == false && o2.IsCmd)
                {
                    return -1;
                }
                else if (o1.IsCmd && o2.IsCmd == false)
                {
                    return 1;
                }

                int n = o1.Hotkey.CompareTo(o2.Hotkey);
                return n; 
            }
            );
            
         //   lst.FindIndex(
        }
        private void SetUserAndName(User u, string name)
        {
            m_oParentUser = u;
            mPath = u.GetPathUser() + name + "\\";
            mPathAhk = mPath + definitions.nameconst.ahk + "\\";
            mPathUI = mPathAhk + definitions.nameconst.ui + "\\";
            mPathKey = mPathAhk + definitions.nameconst.key + "\\";
            mPathData = mPathAhk + definitions.nameconst.data + "\\";
            msName = name;
        }
        private void ReadScriptData(string filename, List<ShortcutInfo> l)
        {
            string line = null;

            string name = this.PathData + filename;

            if (System.IO.File.Exists(name))
            {
                System.IO.TextReader readFile = new StreamReader(name);
                while (true)
                {
                    ShortcutInfo inf = new ShortcutInfo();
                    line = readFile.ReadLine();

                    if (line != null && line != "")
                    {
                        int nBeg = 0, nEnd = 0, len = 0;
                        nEnd = line.IndexOf(";", nBeg);
                        len = nEnd - nBeg;
                        inf.Hotkey = line.Substring(nBeg, len);

                        nBeg = nEnd;
                        ++nBeg;
                        nEnd = line.IndexOf(";", nBeg);
                        len = nEnd - nBeg;
                        inf.Name = line.Substring(nBeg, len);

                        nBeg = nEnd;
                        ++nBeg;
                        inf.Url = line.Substring(nBeg);
                        l.Add(inf);
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
    
        #endregion
    }
}
