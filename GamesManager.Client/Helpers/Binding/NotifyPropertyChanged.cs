using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GamesManager.Client.Helpers.Binding
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
