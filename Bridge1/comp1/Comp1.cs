using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Bridge1.comp1
{
    public class Comp1 : INotifyPropertyChanged
    {
        private string _myProp;
        public string MyProp { get { return _myProp; } set { _myProp = value; OnPropertyChanged(nameof(MyProp), _myProp); } }

        public Comp1()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName, object newValue)
        {
            //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName, newValue));
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName, newValue));
        }


    }

}
