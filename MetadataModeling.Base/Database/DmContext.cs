using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataModeling.Base.Database
{
    public class DmContext
    {



        public List<DmColumn> Columns { get; set; }
        public List<DmTable> Tables { get; set; }
        public List<DmDefaultColumn> DefaultColumns { get; set; }
        public DmDefaultColumnEx DefaultColumnEx { get; set; }
    }
}
