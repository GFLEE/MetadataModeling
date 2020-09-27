using MetadataModeling.Shell.Database.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataModeling.Shell.Database
{
    class SelectFkColumnViewModel
    {
        private ColumnXmlViewModel dmColumnVm;

        public SelectFkColumnViewModel(ColumnXmlViewModel dmColumnVm)
        {
            this.dmColumnVm = dmColumnVm;
        }

        public SelectFkColumn Window { get;  set; }
    }
}
