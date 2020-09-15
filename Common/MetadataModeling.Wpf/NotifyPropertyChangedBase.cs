using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetadataModeling.Wpf
{
    /// <summary>
    /// NotifyPropertyChangedBase
    /// </summary>
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 引发属性更改
        /// </summary>
        /// <param name="propertyName"></param>
        public  void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
            OnNotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// 当属性变更后，需要执行的方法
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
