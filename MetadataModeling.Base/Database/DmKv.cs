using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataModeling.Base.Database
{
    public class DmKv
    {
        public DmKv(string k) : this(k, "")
        {

        }
        public DmKv(string k, string v)
        {
            this.K = k;
            this.V = v;
        }


        public string K { get; set; }
        public string V { get; private set; }

    }
}
