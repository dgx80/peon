using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeonLib.Object
{
    public class PeonRoot
    {
        private Object.User mUser = null;
        File.textlist m_listUser = null;

        public PeonRoot()
        {
        }
        #region Public
        public void CreateUser()
        {
            string rep = AskQuestion(definitions.FormTitle.AddUser, definitions.Question.AddUser);

            if (rep != "")
            {
                bool bFirst = false;
                File.textlist f = ObtaintUserTextList();
                if(f.IsExist(rep))
                {
                    MessageBox.Show(definitions.Message.UserExist);
                    return;
                }
                if (!LoadCurrentUser())
                {
                    bFirst = true;
                } 
                f.Items.Add(rep);
                f.Sort();
                f.WriteList();
                Object.User u = new User(rep);
                u.CreateUser();

                if (bFirst)
                {
                    SetCurrentUser(rep,true);
                }
                UpdateAllUserScript();
            }
        }
        public void CreateProfile()
        {
            if (LoadCurrentUser())
            {
                string rep = AskQuestion(definitions.FormTitle.AddProfile,definitions.Question.AddProfile);
                if (rep != "")
                {
                    if (mUser.CreateProfile(rep) == null)
                    {
                        MessageBox.Show(definitions.Message.ProfileExist);
                    }
                    UpdateAllUserScript();
                }
            }
            else
            {
                MessageBox.Show(definitions.Message.NoUser);
            }
        }
        
        public void DeleteProfile()
        {
            if (LoadCurrentUser())
            {
                mUser.DeleteProfile();
                UpdateAllUserScript();
            }
            else
            {
                MessageBox.Show(definitions.Message.NoUser);
            }
        }

        public void DeleteUser()
        {
            File.textlist file = new PeonLib.File.textlist(definitions.path.UserList);
            if (file.Items.Count > 1)
            {
                forms.UserSelectForm f = new PeonLib.forms.UserSelectForm("Delete User");

                f.LoadFile(file);
                f.ShowDialog();

                if (f.DialogResult == DialogResult.OK)
                {
                    int n = f.ID;
                    User u = new User(file.Items[n]);
                    User.RemoveFromLastUserProfile(u.NAME, "");
                    if (LoadCurrentUser())
                    {
                        if (mUser.NAME == u.NAME)
                        {
                            NextCurrentUser();
                        }
                    }
                    u.DestroyUser();
                    file.Items.RemoveAt(n);
                    file.WriteList();
                    UpdateAllUserScript();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("You cannot delete the last user");
            }
        }
        public void ChangeProfile()
        {
            if (LoadCurrentUser())
            {
                mUser.AskToChangeCurrentProfile();
            }
        }
        public void ChangeUser()
        {
            LoadCurrentUser();
            
            File.textlist file = new PeonLib.File.textlist(definitions.path.UserList);
            forms.UserSelectForm f = new PeonLib.forms.UserSelectForm("Change User");
            int i = 0;

            foreach (string s in file.Items)
            {
                if (s == mUser.NAME)
                {
                    file.Items.RemoveAt(i);
                    break;
                }
                ++i;
            }
            if (file.Items.Count == 0)
            {
                MessageBox.Show("There is no other user");
                return;
            }
            f.LoadFile(file);

            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                int n = f.ID;
                mUser.PushFrontLastUserProfile();
                mUser.WriteLastUserProfile();
                SetCurrentUser(file.Items[n],true);
                mUser.WriteLastUserProfile();
            }
        }
        
        public void SelectTop5UserProfile(int nIndex)
        {
            if (nIndex > 0 && nIndex <= 5)
            {
                LoadCurrentUser();
                compte c = new compte();

                if (mUser.GetLastUserIndex(ref c, nIndex - 1))
                {
                    mUser.PushFrontLastUserProfile();
                    mUser.WriteLastUserProfile();
                    List<compte> l = mUser.LIST_PROFILE_TOP_5;
                    SetCurrentUser(c.sUser,false);
                    mUser.LIST_PROFILE_TOP_5 = l;
                    mUser.ChangeCurrentProfile(c.sProfile,false);
                    
                }
            }
        }
        public void AddWeblink()
        {
            LoadCurrentUser();
            mUser.CurrentProfile.AddWeblink();
        }
        public void AddShortcutFolder()
        {
            LoadCurrentUser();
            mUser.CurrentProfile.AddShortcutFolder();
        }
        public void DeleteShortcutFolder()
        {
            LoadCurrentUser();
            mUser.CurrentProfile.DeleteShortcutFolder();
        }
        public void DeleteWeblink()
        {
            LoadCurrentUser();
            mUser.CurrentProfile.DeleteWeblink();
        }

        public void UpdateAllUserScript()
        {
            m_listUser = ObtaintUserTextList();

            for (int i = 0; i < m_listUser.Items.Count; i++)
            {
                    UpdateAllScriptForOneUser(i);
            }
            LoadCurrentUser();
            mUser.TransferCurrentProfileInCurrentScript();
            MessageBox.Show("All scripts are uppdated :)");
        }
        public void PeonAbout()
        {
            forms.AboutForm f = new PeonLib.forms.AboutForm();
            f.ShowDialog();
        }
        public void SettingAdvanced()
        {
            System.IO.DirectoryInfo inf = new System.IO.DirectoryInfo(definitions.path.UserConfig);
            
            string userconfig = inf.FullName;

            if (userconfig == "")
            {
                userconfig = definitions.path.Root;
            }

            forms.SettinsAdvancedForm f = new PeonLib.forms.SettinsAdvancedForm(userconfig);
            
            if(f.ShowDialog() == DialogResult.OK)
            {
                LinkUsersPath(userconfig,f.UserPath);
            }
        }
        #endregion

        #region private
        private void LinkUsersPath(string oldconfig, string sPath)
        {
            System.IO.DirectoryInfo infsource = new System.IO.DirectoryInfo(definitions.path.Users);
            string source = infsource.FullName;
            if (sPath != oldconfig)
            {
                definitions.path.SetUserConfig(sPath);
                string dest = definitions.path.Users;

                if (!System.IO.Directory.Exists(dest))
                {
                    System.IO.DirectoryInfo infdest = System.IO.Directory.CreateDirectory(dest);
                    
                    Folder.Copy(source, dest);

                    if (System.Windows.Forms.MessageBox.Show("Do you want to keep a data copy in the last path? It is more better to ans Yes", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        Folder.Delete(source, true);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Old user data there is found is this directory, you just link with the new path and no data is tranfered");
                    System.IO.DirectoryInfo infdest = new System.IO.DirectoryInfo(dest);
                    string snewuser = infdest.GetDirectories()[0].Name;
                    SetCurrentUser(snewuser,true);
                    UpdateAllUserScript();
                }
            }
        }
        private void SetCurrentUser(string name, bool bisCurrentProfile)
        {
            if (name != "")
            {
                System.IO.TextWriter f = new System.IO.StreamWriter(definitions.path.UserCurrent);
                f.WriteLine(name);
                f.Close();
                mUser = new User(name);
                mUser.LoadData(bisCurrentProfile);
                if (bisCurrentProfile)
                {
                    mUser.TransferCurrentProfileInCurrentScript();
                }
            }
        }
        private File.textlist ObtaintUserTextList()
        {
            return new PeonLib.File.textlist(definitions.path.UserList);   
        }
        private bool LoadCurrentUser()
        {
            File.textlist f = new PeonLib.File.textlist(definitions.path.UserCurrent);

            if (f.Items.Count == 0)
            {
                return false;
            }
            if (f.Items[0] == "")
            {
                return false;
            }
            mUser = new User(f.Items[0]);
            mUser.LoadData(true);
            return true;
        }
        private void NextCurrentUser()
        {
            File.textlist f = ObtaintUserTextList();
            if (f.Items.Count >= 2)
            {
                foreach (string s in f.Items)
                {
                    if (s != mUser.NAME)
                    {
                        SetCurrentUser(s,true);
                        break;
                    }
                }
            }
            else
            {
                SetCurrentUser("",false);
            }
        }

        private string AskQuestion(string title, string question)
        {
            forms.inputForm f = new forms.inputForm(title,question);

            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                return f.Value;
            }
            return "";
        }
        private void UpdateAllScriptForOneUser(int nCase)
        {
            if (m_listUser == null)
            {
                m_listUser = ObtaintUserTextList();
            }
            if (nCase >= 0 && nCase < m_listUser.Items.Count)
            {
                mUser = new User(m_listUser.Items[nCase]);
                mUser.LoadData(false);
                mUser.UpdateAllProfileScript();
            }
        }
        public void ClipboardCopy()
        {
        }
        public void ClipboardPaste()
        {
        }
        #endregion
    }
}
