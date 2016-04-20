using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PeonLib;
using PeonLib.definitions;
using System.Diagnostics;

namespace PeonLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string call = "";
            PeonLib.Object.PeonRoot peonroot = new PeonLib.Object.PeonRoot();
            
            if (args.Length > 0)
            {
                if (args[0] == "dev")
                {
                    PeonLib.definitions.path.RealoadPath(false);
                    call = PeonLib.definitions.launcher.AddProfile;
                }
                else
                {
                    call = args[0];
                    PeonLib.definitions.path.LoadUserConfig();
                }
            }
            else
            {
                PeonLib.definitions.path.RealoadPath(false);
                Process peon = new Process();
                peon.StartInfo.FileName = PeonLib.definitions.path.BaseScript;
                peon.Start();
                return;
            }
            
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (call == launcher.AddWeblink)
            {
                peonroot.AddWeblink();
            }
            else if (call == launcher.AddShortcutFolder)
            {
                peonroot.AddShortcutFolder();
            }
            else if (call == launcher.ChangeProfile)
            {
                peonroot.ChangeProfile();
            }
            else if (call == launcher.Top1)
            {
                peonroot.SelectTop5UserProfile(1);
            }
            else if (call == launcher.Top2)
            {
                peonroot.SelectTop5UserProfile(2);
            }
            else if (call == launcher.Top3)
            {
                peonroot.SelectTop5UserProfile(3);
            }
            else if (call == launcher.Top4)
            {
                peonroot.SelectTop5UserProfile(4);
            }
            else if (call == launcher.Top5)
            {
                peonroot.SelectTop5UserProfile(5);
            }
            else if (call == launcher.ChangeUser)
            {
                peonroot.ChangeUser();
            }
            else if (call == launcher.DeleteShortcutFolder)
            {
                peonroot.DeleteShortcutFolder();
            }
            else if (call == launcher.DeleteWeblink)
            {
                peonroot.DeleteWeblink();
            }
            else if (call == launcher.DeleteUser)
            {
                peonroot.DeleteUser();
            }
            else if (call == launcher.AddProfile)
            {
                peonroot.CreateProfile();
            }
            else if (call == launcher.AddUser)
            {
                peonroot.CreateUser();
            }
            else if (call == launcher.DeleteProfile)
            {
                peonroot.DeleteProfile();
            }
            else if (call == launcher.ReqgoogleData)
            {
                Application.Run(new PeonLib.forms.ListForm(args[1], args[2], args[3]));
            }
            else if (call == launcher.ScriptUpdateAll)
            {
                peonroot.UpdateAllUserScript();
            }
            else if (call == launcher.peonAbout)
            {
                peonroot.PeonAbout();
            }
            else if (call == launcher.SettingsAdvanced)
            {
                peonroot.SettingAdvanced();
            }
            else if (call == launcher.ClipboardCopy)
            {
                peonroot.ClipboardCopy();
            }
            else if (call == launcher.ClipboardPaste)
            {
                peonroot.ClipboardPaste();
            }
            else if (call == launcher.StopPeon)
            {
                Process[] myProcesses;
                
                myProcesses = Process.GetProcessesByName("AutoHotkey");
                
                foreach (Process myProcess in myProcesses)
                {
                    myProcess.Kill();
                }
            }
            
            
            
        }
    }
}
