using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeonLib
{
    public class compte
    {
        public string sUser;
        public string sProfile;
    };
    namespace definitions
    {
        public class extension
        {
            public const string Profil = "txt";
            public const string txt = "txt";
            public const string ahk = "ahk";
        }
        public class nameconst
        {
            public const string script          = "script";
            public const string users           = "users";
            public const string user            = "user";
            public const string userconfig      = "user_conf.txt";
            public const string admin           = "admin";
            public const string userCurrent     = "user_current";
            //public const string joystick        = "joystick";
            public const string ahk             = "ahk";
            public const string home            = "home";
            public const string template        = "template";
            public const string popupDlg        = "PopupDlg";
            public const string ui              = "ui";
            public const string key             = "key";
            public const string data            = "data";
            public const string tmp_pref        = "tmp_";
            public const string pInterface      = "interface";
            public const string pShortcuts      = "shortcuts";
            public const string pWeblink        = "WebLink";
            public const string peonAdmin       = "peon_admin";
            public const string peonUser        = "peon_user";
            public const string peonProfile     = "peon_profile";
            public const string Clipboard       = "clipboard";
            public const string DataShortcuts   = "data_shortcuts";
            public const string DataWeblink     = "data_weblink";
            public const string DataClipboard   = "data_clipboard.txt";
            public const string ClipboardCopy   = "clipboard_copy";
            public const string ClipboardPaste  = "clipboard_paste";
            public const string LastUserProfile = "last_userprofile";
        };

        public class path
        {
            //root
            public static string Root= "..\\..\\";
            public static string Script = Root + nameconst.script + "\\";
            public static string Admin = Root + nameconst.admin + "\\";
            public static string Template = Root + nameconst.template + "\\";
            public static string TemplatePref = Template + nameconst.tmp_pref;
            public static string BaseScript = Ahk + "base." + extension.ahk;

            //admin
            public static string UserConfig = Root;
            public static string Users = UserConfig + nameconst.users + "\\";
            public static string UserList = Users + nameconst.users + "." + extension.txt;
            public static string UserCurrent = Root + nameconst.userCurrent + "." + extension.txt;
            public static string LastUsersProfile = Root + nameconst.LastUserProfile + "." + extension.txt;
            public static string DataShortcuts = Root + nameconst.DataShortcuts + "." + extension.txt;
            public static string DataWeblink = Root + nameconst.DataWeblink + "." + extension.txt;
            
            //script
            public static string Ahk = Script + "ahk\\";
                
            
            public static void RealoadPath(bool isExtern)
            {
 
                if (isExtern)
                {
                    Root = "..\\..\\";
                }
                else
                {
                    Root = "..\\";
                }
                Script = Root + nameconst.script + "\\";
                Admin = Root + nameconst.admin + "\\";
                Template = Root + nameconst.template + "\\";
                TemplatePref = Template + nameconst.tmp_pref;
                LoadUserConfig();
                Ahk = Script + "ahk\\";
                BaseScript = Ahk + "base." + extension.ahk;
            }
            private static void ReloadUserPath()
            {
                Users = UserConfig + nameconst.users + "\\";
                UserList = Users + nameconst.users + "." + extension.txt;
                UserCurrent = Root + nameconst.userCurrent + "." + extension.txt;
            }
            public static void LoadUserConfig()
            {
                File.textlist tx = new PeonLib.File.textlist(Root + nameconst.userconfig);
                if (tx.Items.Count > 0)
                {
                    UserConfig = tx.Items[0];
                }
                else
                {
                    UserConfig = Root;
                }

                ReloadUserPath();
            }
            public static void SetUserConfig(string path)
            {
                UserConfig = path;
                File.textlist tx = new PeonLib.File.textlist(Root + nameconst.userconfig);
                if (tx.Items.Count < 1)
                {
                    tx.Items.Add("");
                }
                tx.Items[0] = path;
                tx.WriteList();
                ReloadUserPath();
            }

            //user
            public enum eProfile
            {
                ahk = 0,
                count
            };

            public string[] ProfileFolder = new string[(int)eProfile.count] { nameconst.ahk};
        }
        

        
        public class launcher
        {
            public const string Top1 = "top.1";
            public const string Top2 = "top.2";
            public const string Top3 = "top.3";
            public const string Top4 = "top.4";
            public const string Top5 = "top.5";
            public const string AddUser         = "user.add";
            public const string ChangeUser      = "user.change";
            public const string DeleteUser      = "user.delete";
            public const string AddProfile      = "profile.add";
            public const string ChangeProfile = "profile.change";
            public const string DeleteProfile = "profile.delete";
            public const string AddWeblink      = "weblink.add";
            public const string DeleteWeblink = "weblink.delete";
            public const string StopPeon = "peon.stop";
            public const string AddShortcutFolder     = "shortcutFolder.add";
            public const string DeleteShortcutFolder = "shortcutFolder.delete";
            public const string ScriptUpdateAll = "w";
            public const string peonAbout = "peon.about";
            public const string ReqgoogleData   = "req.google.data";
            public const string PeonLauncher = "peonLauncher";
            public const string SettingsAdvanced = "settings.advanced";
            public const string ClipboardCopy = "clipboard.copy";
            public const string ClipboardPaste = "clipboard.paste";

        }
        public class FormTitle
        {
            public const string AddUser = "Add user";
            public const string AddProfile = "Add profile";
        }
        public class Question
        {
            public const string AddProfile = "What is the profile name?";
            public const string AddUser = "What is the user name?";
        }
        public class Message
        {
            public const string NoUser = "Create user before";
            public const string UserExist = "This user name already existed";
            public const string ProfileExist = "This profile name already existed";
        }
    }
}
