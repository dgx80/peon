using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace PeonLib
{
    public class Folder
    {
        public static void Copy(string source, string destination)
        {
            DirectoryInfo s = new DirectoryInfo(source);
            DirectoryInfo d = new DirectoryInfo(destination);

            CopyDirectory(s, d);
        }
        public static void Delete(string name,bool recurcive)
        {
            if(Directory.Exists(name))
            {
                Directory.Delete(name, recurcive);
            }
        }
        private static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name));
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if (dir.Name != ".svn")
                {
                    // Get destination directory.
                    string destinationDir = Path.Combine(destination.FullName, dir.Name);

                    // Call CopyDirectory() recursively.
                    CopyDirectory(dir, new DirectoryInfo(destinationDir));
                }
            }
        }
    }
}
