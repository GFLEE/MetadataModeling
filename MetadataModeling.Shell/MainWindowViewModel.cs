using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataModeling.Shell.Database;
using MetadataModeling.Shell.Enums;
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
            DatabaseViewModel = new CrlDatabaseViewModel();
            MainViewModel = new CrlMainViewModel();
        }



        public CrlDatabaseViewModel DatabaseViewModel { get; set; }
        public CrlMainViewModel MainViewModel { get; set; }
        /// <summary>
        /// Window Title
        /// </summary>
        public string title;
        public string Title
        {
            get
            {

                return ConfigurationManager.AppSettings["Title"];
            }

            set
            {
                title = value;
                NotifyPropertyChanged("Title");

            }
        }
    }
}
