using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeonLib.Object
{
    public class User
    {
        #region Variables

        private List<Profile> mlProfile = new List<Profile>();
        private string mName = "";
        private Profile mCurrentProfile = null;
        private List<compte> mlLastUserProfile = new List<compte>();

        #endregion

        //je crois que cette fonction doit devenir static
        public bool GetLastUserIndex(ref compte c, int nIndex)
        {
            if (nIndex >= 0 && nIndex < mlLastUserProfile.Count)
            {
                c.sUser = mlLastUserProfile[nIndex].sUser;
                c.sProfile = mlLastUserProfile[nIndex].sProfile;
                return true;
            }
            return false;
        }

        #region Constructor

        public User(string name)
        {
            mName = name;
        }
        
        #endregion

        #region DATA

        public void LoadData(bool bisCurrentProfile)
        {
            PeonLib.File.textlist tx = new PeonLib.File.textlist(GetProfileListPath());
            
            string sCurrentName = GetCurrentProfileName();
            mlProfile.Clear();

            foreach (string s in tx.Items)
            {
                if (s != sCurrentName)
                {
                    AddProfile(s);
                }
                else
                {
                    mCurrentProfile = AddProfile(s);
                }
            }
            
            mCurrentProfile.LoadData();
            if (bisCurrentProfile)
            {
                LoadLastUserProfile();
            }
        }

        #endregion

        #region PATHTOOLS
        public string GetPathUser()
        {
            return definitions.path.Users + NAME + "\\";
        }
        private string GetCurrentProfilePath()
        {
            return GetPathUser() + "current." + definitions.extension.Profil;
        }
        private string GetProfileListPath()
        {
            return GetPathUser() + "profil." + definitions.extension.Profil;
        }
      
        #endregion

        #region User

        public void CreateUser()
        {
            System.IO.Directory.CreateDirectory(GetPathUser());
            mCurrentProfile = CreateProfile(definitions.nameconst.home);
        }
        public void DestroyUser()
        {
            string s = GetPathUser();
            System.IO.Directory.Delete(s, true);
        }

        #endregion

        #region Profile
        public Profile CreateProfile(string name)
        {
            Profile prof = null;
            if (!IsProfileNameExist(name))
            {
                prof = AddProfile(name);
                prof.CreateStandardProfil();
                WriteProfileFile();
                PeonLib.File.textlist tx = new PeonLib.File.textlist(GetProfileListPath());
                tx.Sort();
                tx.WriteList();
                AddToLastUserProfile(NAME, name);
            }
            return prof;
        }
        public void DeleteProfile()
        {
            if (mlProfile.Count > 1)
            {
                PeonLib.File.textlist file = new PeonLib.File.textlist(GetProfileListPath());
                forms.UserSelectForm f = new PeonLib.forms.UserSelectForm("Delete profile");

                f.LoadFile(file);
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    int n = f.ID;
                    bool bIsCurrentProf = (n == GetProfileCase(mCurrentProfile.Name));
                    User.RemoveFromLastUserProfile(NAME, mlProfile[n].Name);
                    mlProfile[n].DestroyProfile();

                    if (bIsCurrentProf)
                    {
                        if (n == 0)
                        {
                            SetCurrentProfile(mlProfile[1].Name);
                        }
                        else
                        {
                            SetCurrentProfile(mlProfile[0].Name);
                        }
                        TransferCurrentProfileInCurrentScript();
                    }
                    mlProfile.RemoveAt(n);
                    WriteProfileFile();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("you cannot delete the last profile");
            }
        }
        public void UpdateAllProfileScript()
        {
            foreach (Profile p in mlProfile)
            {
                p.LoadData();
                ProfileData d = new ProfileData();
                p.ObtainProfileData(ref d);
                p.CreateStandardProfil();
                p.SetNewProfileData(d);
                p.UpdateAllScript();
            }
        }
        public void LoadLastUserProfile()
        {
            File.textlist tx = new PeonLib.File.textlist(definitions.path.LastUsersProfile);
            mlLastUserProfile.Clear();
            for (int i = 0; i < tx.Items.Count; i = i + 2)
            {
                compte o = new compte();
                o.sUser = tx.Items[i];
                o.sProfile = tx.Items[i + 1];
                mlLastUserProfile.Add(o);
            }
            RemoveFromLastUserProfile();
        }
        public void PushFrontLastUserProfile()
        {
            compte newCompte = new compte();
            newCompte.sUser = NAME;
            newCompte.sProfile = PROFILE_NAME;
            mlLastUserProfile.Insert(0, newCompte);

        }
        public void WriteLastUserProfile()
        {
            File.textlist tx = new PeonLib.File.textlist(definitions.path.LastUsersProfile);
            tx.Items.Clear();
            int n = 0;
            foreach (compte c in mlLastUserProfile)
            {
                if (++n <= 8)
                {
                    tx.Items.Add(c.sUser);
                    tx.Items.Add(c.sProfile);
                }
            }
            tx.WriteList();
        }
        public void RemoveFromLastUserProfile()
        {
            int n = 0;
            foreach (compte c in mlLastUserProfile)
            {
                if (c.sUser == NAME && c.sProfile == PROFILE_NAME)
                {
                    mlLastUserProfile.RemoveAt(n);
                    break;
                }
                n++;
            }
        }
        public void TransferCurrentProfileInCurrentScript()
        {
            mCurrentProfile.UpdateInterfaceTopList(ref mlLastUserProfile);
            mCurrentProfile.TransferProfileInCurrentScript();
        }
        public void AskToChangeCurrentProfile()
        {
            PeonLib.File.textlist file = new PeonLib.File.textlist(GetProfileListPath());
            forms.UserSelectForm f = new PeonLib.forms.UserSelectForm("Change profile");
            f.LoadFile(file);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                int n = f.ID;
                ChangeCurrentProfile(file.Items[n], true);
            }
        }
        public void ChangeCurrentProfile(string name, bool bPushFrontLastCurrentProfile)
        {
            if (bPushFrontLastCurrentProfile)
            {
                PushFrontLastUserProfile();
            }
            SetCurrentProfile(name);
            WriteProfileFile();
            RemoveFromLastUserProfile();
            WriteLastUserProfile();
            TransferCurrentProfileInCurrentScript();
        }
        private void SetCurrentProfile(string name)
        {
            foreach (Profile p in mlProfile)
            {
                if (name == p.Name)
                {
                    mCurrentProfile = p;
                    break;
                }
            }
        }
        private Profile AddProfile(string name)
        {
            Profile p = null;

            foreach (Profile i in mlProfile)
            {
                if (i.Name == name)
                {
                    p = i;
                }
            }
            if (p == null)
            {
                p = new Profile(this, name);
                mlProfile.Add(p);
            }
            return p;
        }
        private int GetProfileCase(string name)
        {
            int n = 0;
            foreach (Profile p in mlProfile)
            {
                if (name == p.Name)
                {
                    return n;
                }
                n++;
            }
            return -1;
        }
        private void WriteProfileFile()
        {
            string name = GetProfileListPath();
            System.IO.TextWriter fw = new System.IO.StreamWriter(GetProfileListPath());
            foreach (Profile p in mlProfile)
            {
                fw.WriteLine(p.Name);
            }
            fw.Close();
            name = GetPathUser() + "current." + definitions.extension.Profil;
            fw = new System.IO.StreamWriter(name);
            if (mCurrentProfile != null)
            {
                fw.Write(mCurrentProfile.Name);
            }
            else
            {
                fw.Write(mlProfile[0].Name);
            }
            fw.Close();
        }
        
        
        //Cette fonction est demeure dans user
        private string GetCurrentProfileName()
        {
            PeonLib.File.textlist tx = new PeonLib.File.textlist(GetCurrentProfilePath());
            return tx.Items[0];
        }

        //Cette fonction est demeure dans user
        private bool IsProfileNameExist(string name)
        {
            foreach (Profile i in mlProfile)
            {
                if (i.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        
        #endregion

        #region Properties

        public string NAME
        {
            get{return mName;}
            set{mName = value;}
        }
        public List<compte> LIST_PROFILE_TOP_5
        {
            get
            {
                return mlLastUserProfile;
            }
            set
            {
                mlLastUserProfile = value;
            }
        }
        public string PROFILE_NAME
        {
            get { return mCurrentProfile.Name; }
        }
        public Profile CurrentProfile
        {
            get { return mCurrentProfile; }
        }
        #endregion

        #region Private

        
        #endregion

        #region Static

        static public void RemoveFromLastUserProfile(string sUser, string sProfile)
        {
            File.textlist tx = new PeonLib.File.textlist(definitions.path.LastUsersProfile);
            List<int> listn = new List<int>();

            for (int i = 0; i < tx.Items.Count; i = i + 2)
            {
                if (tx.Items[i] == sUser && (tx.Items[i + 1] == sProfile || sProfile == ""))
                {
                    listn.Insert(0, i);
                }
            }
            foreach (int n in listn)
            {
                tx.Items.RemoveRange(n, 2);
            }
            tx.WriteList();
        }
        static public void AddToLastUserProfile(string sUser, string sProfile)
        {
            File.textlist tx = new PeonLib.File.textlist(definitions.path.LastUsersProfile);
            List<int> listn = new List<int>();

            tx.Items.Add(sUser);
            tx.Items.Add(sProfile);

            tx.WriteList();
        }
    
        #endregion

    }
}









