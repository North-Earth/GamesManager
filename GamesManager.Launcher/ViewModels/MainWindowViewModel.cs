using System;
using GamesManager.Launcher.Views;
using MVVM_Helper.Binding;
using MVVM_Helper.Commands;

namespace GamesManager.Launcher.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private MainControlView userControl;

        public MainControlView UserControl
        {
            get => userControl;
            private set
            {
                userControl = value;
                RaiseOnPropertyChanged();
            }
        }

        public DelegateCommand SettingsButtonCommand { get; set; }

        public DelegateCommand FeedbackButtonCommand { get; set; }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            UserControl = new MainControlView();

            SettingsButtonCommand = new DelegateCommand(param => SettingsButtonClick());
            FeedbackButtonCommand = new DelegateCommand(param => FeedbackButtonClick());
        }

        #endregion

        #region Methods

        public void SettingsButtonClick() { throw new NotImplementedException(); }

        public void FeedbackButtonClick() { throw new NotImplementedException(); }

        #endregion
    }
}
