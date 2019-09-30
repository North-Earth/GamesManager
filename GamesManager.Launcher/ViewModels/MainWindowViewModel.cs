using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
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

        public string Header { get => header; set => header = value; }

        public string DownloadStatus
        {
            get => downloadStatus; set
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

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            Header = MainHeader;
            DownloadStatus = "Complete";
            downloadPercent = 100;
            PlayButtonCommand = new DelegateCommand(param => Play(), param => IsEnablePlayButton);
            IsEnablePlayButton = true;
        }

        #endregion

        #region Methods

        private void Play()
        {
            IsEnablePlayButton = false;
            DownloadPercent = 10;

        }

        #endregion
    }
}
