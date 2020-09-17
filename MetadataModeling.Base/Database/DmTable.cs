using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;


namespace MetadataModeling.Base.Database
{
    public class DmTable
    {
        public DmTable()
        {

        }

        public string ID { get; set; }
        public string Name { get; set; }

        internal string GetCodeName()
        {
            throw new NotImplementedException();
        }
    }
}
