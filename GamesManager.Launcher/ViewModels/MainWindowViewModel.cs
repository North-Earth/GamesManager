using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GamesManager.Launcher.Views;
using MVVM_Helper.Binding;
using MVVM_Helper.Commands;

namespace GamesManager.Launcher.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        const string MainHeader = "Alexey Kukushkin's Game Launcher";

        private string header;
        private string downloadStatus;
        private int downloadPercent;
        private UserControl informationControl;

        public string Header { get => header; set => header = value; }

        public string DownloadStatus
        {
            get => downloadStatus;
            set
            {
                downloadStatus = value;
                RaiseOnPropertyChanged();
            }
        }

        public string DownloadPercentString { get => $"{downloadPercent}%"; }

        public int DownloadPercent
        {
            get => downloadPercent;
            set
            {
                downloadPercent = value;
                RaiseOnPropertyChanged();
            }
        }

        public bool IsEnablePlayButton { get; set; }
        public DelegateCommand PlayButtonCommand { get; set; }

        public DelegateCommand SettingsButtonCommand { get; set; }

        public DelegateCommand FeedbackButtonCommand { get; set; }

        public UserControl InformationControl
        {
            get => informationControl;
            set
            {
                informationControl = value;
                RaiseOnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            Header = MainHeader;
            DownloadStatus = "Complete";
            downloadPercent = 100;
            PlayButtonCommand = new DelegateCommand(param => Play(), param => IsEnablePlayButton);
            SettingsButtonCommand = new DelegateCommand(param => Settings());
            FeedbackButtonCommand = new DelegateCommand(param => Feedback());
            IsEnablePlayButton = true;
        }

        #endregion

        #region Methods

        private void Play()
        {
            IsEnablePlayButton = false;
        }

        private void Settings()
        {
            
        }

        private void Feedback()
        {
            
        }

        #endregion
    }
}
