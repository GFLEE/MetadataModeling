using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataModeling.Wpf;
using MetadataModeling.Wpf.Base.Control;

namespace MetadataModeling.Shell
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    public class MainWindowViewModel : WindowViewModel
    {
        public MainWindowViewModel()
        {
            Title = ConfigurationManager.AppSettings["Title"];
        }





        /// <summary>
        /// Window Title
        /// </summary>
        public string title;
        public string Title
        {
            get
            {

                return title;
            }

            set
            {
                title = value;
                NotifyPropertyChanged("Title");

            }
        }
    }
}
