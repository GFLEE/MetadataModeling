using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataModeling.Wpf.Base
{
    public class KvItem<TKey, TValue> : NotifyPropertyChangedBase
    {
        public TKey key { get; set; }
        public TValue value { get; set; }


        public KvItem(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public KvItem(object tag, TValue value)
        {
            this.Tag = tag;
            this.Value = value;
        }


        public TValue Value
        {
            get
            {
                return value;
            }
            set
            {
                if(object.Equals(this.value,value))
                {
                    return;
                }
                this.value = value;
                NotifyPropertyChanged("Value");
            }
        }
         


        public object Tag { get; set; }
    }
}
