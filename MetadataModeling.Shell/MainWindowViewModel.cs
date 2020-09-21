using MetadataModeling.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataModeling.Shell
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {




        public string title;
        public string Title
        {
            get
            {
                if (title == null)
                {
                    return ConfigurationManager.AppSettings["Title"];
                }
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
