using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IUpdater updater;

        private int downloadPercent;
        private string header;
        private bool isEnableProcessButton;

        private ProcessStatus processStatus;
        private ProcessButtonStatus processButtonStatus;
        private UserControl informationControl;

        private CancellationTokenSource tokenSource;

        public string Header { get => header; set => header = value; }

        public ProcessStatus ProcessStatus
        {
            get => processStatus;
            set
            {
                switch (value)
                {
                    case ProcessStatus.Checking:
                        IsEnableProcessButton = false;
                        break;
                    case ProcessStatus.Downloading:
                    case ProcessStatus.Installing:
                        ProcessButtonName = ProcessButtonStatus.Cancel;
                        IsEnableProcessButton = true;
                        break;
                    case ProcessStatus.Complete:
                        ProcessButtonName = ProcessButtonStatus.Play;
                        IsEnableProcessButton = true;
                        break;
                    case ProcessStatus.Done:
                        ProcessButtonName = ProcessButtonStatus.Install;
                        IsEnableProcessButton = true;
                        break;
                }

                processStatus = value;
                RaiseOnPropertyChanged();
            }
        }

        public int DownloadPercent
        {
            get => downloadPercent;
            set
            {
                downloadPercent = value;
                RaiseOnPropertyChanged();
            }
        }

        public bool IsEnableProcessButton
        {
            get => isEnableProcessButton;
            set
            {
                isEnableProcessButton = value;
                RaiseOnPropertyChanged();
            }
        }

        public ProcessButtonStatus ProcessButtonName 
        { 
            get => processButtonStatus;
            set
            {
                processButtonStatus = value;
                RaiseOnPropertyChanged();
            }
        }

        public DelegateCommand ProcessButtonCommand { get; set; }

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
            tokenSource = new CancellationTokenSource();

            header = MainHeader;
            downloadPercent = 100;

            ProcessButtonCommand = new DelegateCommand(async param => await ProcessButtonClick(), param => IsEnableProcessButton);

            SettingsButtonCommand = new DelegateCommand(param => Settings());
            FeedbackButtonCommand = new DelegateCommand(param => Feedback());

            ProcessStatus = ProcessStatus.Done;
        }

        #endregion

        #region Methods

        private async Task ProcessButtonClick()
        {
            switch (ProcessButtonName)
            {
                case ProcessButtonStatus.Play:
                    await PlayGame(GameName.Roll_a_Ball);
                    break;
                case ProcessButtonStatus.Install:
                    await Task.Factory.StartNew(async () => await StartProcess(), tokenSource.Token);          
                    break;
                case ProcessButtonStatus.Cancel:
                    CancelProcesses();
                    break;
                default:
                    break;
            }
        }

        private async Task StartProcess()
        {
            try
            {
                var latestVersion = await GetUpdates(GameName.Roll_a_Ball);

                var task = await Task.Factory.StartNew(async () =>
                {
                    await updater.DownloadLatestVersion(latestVersion);
                }, tokenSource.Token);

                while (!task.IsCompleted)
                {
                    DownloadPercent = updater.CompletionPercent;
                    ProcessStatus = updater.Status;
                }
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine($"\r\nDownload canceled.\r\n");
                DownloadPercent = 100;
                ProcessStatus = ProcessStatus.Done;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\r\nDownload failed.\r\n");
            }
            finally
            {
                Debug.WriteLine($"\r\ntokenSource.Dispose();\r\n");
                tokenSource.Dispose();
            }
        }

        private void CancelProcesses()
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
            }
        }

        private async Task PlayGame(GameName gameName)
        {
            //switch (ProcessStatus)
            //{
            //    case ProcessStatus.Checking:
            //        break;
            //    case ProcessStatus.Downloading:
            //        break;
            //    case ProcessStatus.Installing:
            //        break;
            //    case ProcessStatus.Complete:
            //        break;
            //    case ProcessStatus.Error:
            //        break;
            //    default:
            //        break;
            //}

            //ProcessStatus = ProcessStatus.Checking;
            //IsEnableProcessButton = false;

            //var latestVersion = await GetUpdates(GameName.Roll_a_Ball);
            //processStatus = ProcessStatus.Complete;

            //var task = await Task.Factory.StartNew(async () => await updater.DownloadLatestVersion(latestVersion));

            //while (!task.IsCompleted)
            //{
            //    DownloadPercent = updater.CompletionPercent;
            //    ProcessStatus = updater.Status;
            //}

            //task.Wait();

            //IsEnableProcessButton = true;

            //bool test = true;
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
