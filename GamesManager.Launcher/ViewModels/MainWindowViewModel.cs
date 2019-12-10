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

        public DelegateCommand OpenDirectoryButtonCommand { get; set; }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            Vesion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            UserControl = new MainControlView();

            SettingsButtonCommand = new DelegateCommand(param => SettingsButtonClick());
            FeedbackButtonCommand = new DelegateCommand(param => FeedbackButtonClick());
            OpenDirectoryButtonCommand = new DelegateCommand(param => OpenDirectoryButtonClick());
        }

        #endregion

        #region Methods

        public void SettingsButtonClick() { throw new NotImplementedException(); }

        public void FeedbackButtonClick() { throw new NotImplementedException(); }

        public static void OpenDirectoryButtonClick() 
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        #endregion
    }
}
