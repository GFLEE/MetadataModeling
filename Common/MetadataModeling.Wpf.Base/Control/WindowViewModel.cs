using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MetadataModeling.Wpf.Base.Control
{
    public class WindowViewModel : NotifyPropertyChangedBase
    {

        public WindowViewModel()
        {

        }

        /// <summary>
        /// ViewMode对应的窗口对象
        /// </summary>
        public Window Window { get; set; }

        public virtual bool Close()
        {
            return true;
        }




    }
}
