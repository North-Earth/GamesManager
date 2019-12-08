using System;
using System.Reflection;
using GamesManager.Launcher.Helper.Binding;
using GamesManager.Launcher.Helper.Commands;
using GamesManager.Launcher.Views;

namespace GamesManager.Launcher.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private MainControlView userControl;

        private string vesion;

        public string Vesion
        {

            get => vesion;
            set => vesion = value;
        }

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
            Vesion = Assembly.GetEntryAssembly().GetName().Version.ToString();
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
