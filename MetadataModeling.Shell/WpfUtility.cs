using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataModeling.Shell
{
    internal class WpfUtility
    {
        public static string GetCodeName(string code, string name)
        {
            return string.Format("{0}({1})", code, name);
        }

    }
}
