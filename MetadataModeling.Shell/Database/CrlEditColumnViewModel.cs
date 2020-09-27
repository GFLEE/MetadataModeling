using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataModeling.Wpf.Base.Control;
using System.Windows;
using System.Windows.Input;
using MetadataModeling.Shell.Database.ViewModel;
using MetadataModeling.Wpf;

namespace MetadataModeling.Shell.Database
{
    public class CrlEditColumnViewModel : UserControlViewModel
    {
        public ColumnXmlViewModel DmColumnVm { get; set; }

        public CrlEditColumnViewModel(ColumnXmlViewModel dmColumnVm)
        {
            DmColumnVm = dmColumnVm;
        }

        #region Select Command
        RelayCommand selectCommand = null;
        public ICommand SelectCommand
        {
            get
            {
                if (selectCommand != null)
                {
                    return selectCommand;
                }
                selectCommand = new RelayCommand(() =>
                {
                    var columnWindow = new SelectFkColumn();
                    var model = new SelectFkColumnViewModel(DmColumnVm);
                    columnWindow.DataContext = model;
                    model.Window = columnWindow;
                    columnWindow.ShowDialog();
                }, () =>
                {
                    return true;
                });

                return selectCommand;
            }
        }



        #endregion


    }
}
