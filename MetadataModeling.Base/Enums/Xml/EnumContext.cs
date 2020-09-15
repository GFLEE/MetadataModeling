using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataModeling.Base.Enums.Xml
{
    /// <summary>
    /// EnumContext
    /// </summary>
    public class EnumContext
    {
        public RootXml RootXml { get; set; }

        public EnumContext()
        {
            Renew();
        }

        private void Renew()
        {
            RootXml = new RootXml();
            Init(RootXml);

        }

        private void Init(RootXml rootXml)
        {


        }
    }
}
