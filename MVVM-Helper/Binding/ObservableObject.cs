using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Helper.Binding
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaiseOnPropertyChanged([CallerMemberName] string prop = null)
        {
            if (string.IsNullOrEmpty(prop)) throw new ArgumentException("Not be null or empty");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
