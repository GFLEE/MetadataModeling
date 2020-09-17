using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;

namespace MetadataModeling.Base.Database
{
    public class DmTableGroup : DmItem<TableGroupXml>, ICopy
    {
        public DmTableGroup(DmContext context, TableGroupXml xmlItem)
        {
            this.Context = context;
            this.XmlItem = xmlItem;
            this.Tables = new List<DmTable>();
        }

        List<DmTable> Tables { get; set; }

    }
}
