using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models;
using GamesManager.Launcher.Models.Enums;
using GamesManager.Launcher.Views;
using MVVM_Helper.Binding;
using MVVM_Helper.Commands;

namespace GamesManager.Launcher.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private const string MainHeader = "Alexey Kukushkin's Game Launcher";

        private string header;
        private ProcessStatus processStatus;
        private int downloadPercent;
        private UserControl informationControl;

        private readonly IUpdater updater;

        public string Header { get => header; set => header = value; }

        public ProcessStatus ProcessStatus
        {
            get => processStatus;
            set
            {
                processStatus = value;
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
            updater = new Updater();

            Header = MainHeader;
            ProcessStatus = ProcessStatus.Complete;
            downloadPercent = 100;
            PlayButtonCommand = new DelegateCommand(async param => await Task.Factory.StartNew(async () => await Play()), param => IsEnablePlayButton);
            SettingsButtonCommand = new DelegateCommand(param => Settings());
            FeedbackButtonCommand = new DelegateCommand(param => Feedback());
            IsEnablePlayButton = true;
        }

        #endregion

        #region Methods

        private async Task Start()
        {

        }

        private async Task Play()
        {
            switch (ProcessStatus)
            {
                case ProcessStatus.Checking:
                    break;
                case ProcessStatus.Downloading:
                    break;
                case ProcessStatus.Installing:
                    break;
                case ProcessStatus.Complete:
                    break;
                case ProcessStatus.Error:
                    break;
                default:
                    break;
            }

            ProcessStatus = ProcessStatus.Checking;
            IsEnablePlayButton = false;

            var latestVersion = await GetUpdates(GameName.Roll_a_Ball);
            processStatus = ProcessStatus.Complete;

            var task = await Task.Factory.StartNew(async () => await updater.DownloadLatestVersion(latestVersion));

            while (!task.IsCompleted)
            {
                DownloadPercent = updater.CompletionPercent;
                ProcessStatus = updater.Status;
            }

            task.Wait();

            IsEnablePlayButton = true;
        }

        private void Settings()
        {
            
        }

        private void Feedback()
        {
            
        }

        private async Task<LatestVersionInfo> GetUpdates(GameName gameName) 
            => await updater.GetLatestVersionInfo(gameName);

        #endregion
    }
}
