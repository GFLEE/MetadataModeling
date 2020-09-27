using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MetadataModeling.Wpf;

namespace MetadataModeling.Wpf.Base.Control
{
    public class UserControlViewModel : NotifyPropertyChangedBase
    {

        /// <summary>
        /// Vm => Control
        /// </summary>
        public UserControl Control { get; set; }
    }
}
