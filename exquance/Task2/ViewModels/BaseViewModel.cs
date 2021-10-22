using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Task2.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
