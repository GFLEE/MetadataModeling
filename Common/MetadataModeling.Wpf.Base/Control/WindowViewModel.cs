using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public ContentControl Window { get; set; }

        public virtual bool Close()
        {
            return true;
        }


        public ICommand commandClose;
        public ICommand CommandClose
        {
            get
            {
                if (Window == null)
                {
                    throw new ArgumentNullException("Window");
                }
                if (commandClose == null)
                {
                    commandClose = new RelayCommand(() =>
                    {
                        CloseWindow();
                    });
                }
                return commandClose;
            }
        }


        public void CloseWindow()
        {
            if (Close())
            {
                if (Window != null && Window is Window)
                {
                    (Window as Window).Close();
                }
            }
        }

        /// <summary>
        /// 引发所有Command Excute
        /// </summary>
        public void RaiseCommandCanExcute()
        {
            CommandManager.InvalidateRequerySuggested();
        }




    }
}
