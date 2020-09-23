using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MetadataModeling.Wpf
{
    public class RelayCommand : ICommand
    {
        readonly Action excute1;
        readonly Action<object> excute2;
        readonly Func<bool> canExcute1;
        readonly Func<object, bool> canExcute2;

        public RelayCommand(Action excute) : this(excute, null)
        {

        }

        public RelayCommand(Action<object> excute) : this(excute, null)
        {

        }
        public RelayCommand(Action excute, Func<bool> canExcute)
        {
            if (excute == null)
            {
                throw new ArgumentException("ArgumentException");
            }
            excute1 = excute;
            canExcute1 = canExcute;


        }
        public RelayCommand(Action<object> excute, Func<object,bool> canExcute)
        {
            if (excute == null)
            {
                throw new ArgumentException("ArgumentException");
            }
            excute2 = excute;
            canExcute2 = canExcute;


        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;

            }
        }



        public bool CanExecute(object parameter)
        {
            if (canExcute1 != null)
            {
                return canExcute1();
            }

            if (excute2 != null)
            {
                return canExcute2(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (excute1 != null)
            {
                excute1();
                return;
            }

            if (excute2 != null)
            {
                excute2(parameter);
                return;
            }
        }


        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

    }
}
