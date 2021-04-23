using MetadataModeling.Base.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataModeling.Shell;
using MetadataModeling.Wpf;

namespace MetadataModeling.Shell.Database.ViewModel
{
    public partial class ColumnXmlViewModel : NotifyPropertyChangedBase
    {
        public DmColumn DmColumn { get { return Tag as DmColumn; } }

        public string CodeName
        {
            get
            { return WpfUtility.GetCodeName("", ""); }
        }

        public DmColumn Tag { get; private set; }

        public void OnAfterChangeOfCode()
        {
            NotifyPropertyChanged("CodeName");
        }
    }
}