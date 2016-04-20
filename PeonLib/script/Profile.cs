using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PeonLib.script
{
    class Profile
    {
        private AhkManager mAhk = new AhkManager();
        private FileInfo mFile = null;

        public Profile(string name)
        {
            mFile = new FileInfo(definitions.PathRoot+definitions.PathProfile+name+ "." + definitions.ExtProfile);
        }
    }
}
