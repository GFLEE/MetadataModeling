using MetadataModeling.Base.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataModeling.Shell;

namespace MetadataModeling.Shell.Database.ViewModel
{
    public partial class ColumnXmlViewModel
    {
        public DmColumn DmColumn { get { return Tag as DmColumn; } }

        public string CodeName
        {
            get
            { return WpfUtility.GetCodeName(Code, Name); }
        }
        partial void OnAfterChangeOfCode()
        {
            NotifyPropertyChanged("CodeName");
        }
    }
}