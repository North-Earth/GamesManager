using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
using GamesManager.Launcher.Properties;
using GamesManager.Launcher.Views;
using MVVM_Helper.Binding;
using MVVM_Helper.Commands;

namespace GamesManager.Launcher.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private const string MainHeader = "Alexey Kukushkin's Game Launcher";

        private readonly List<IGameManager> gameManagers;
        private readonly IGameManager gameManager;

        private int downloadBarValue;
        private string header;

        private bool isEnableProcessButton;
        private bool isIndeterminateDownloadBar;

        private ProcessStatus processStatus;
        private ProcessButtonStatus processButtonStatus;
        private UserControl informationControl;

        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;

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
                        IsIndeterminateDownloadBar = true;
                        break;
                    case ProcessStatus.Downloading:
                    case ProcessStatus.Installing:
                    case ProcessStatus.Complete:
                    case ProcessStatus.Done:
                        IsEnableProcessButton = true;
                        IsIndeterminateDownloadBar = false;
                        break;
                    default:
                        break;
                }

                processStatus = value;
                RaiseOnPropertyChanged();
            }
        }

        public int DownloadBarValue
        {
            get => downloadBarValue;
            set
            {
                downloadBarValue = value;
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

        public bool IsIndeterminateDownloadBar
        {
            get => isIndeterminateDownloadBar;
            set
            {
                isIndeterminateDownloadBar = value;
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
            gameManagers = new List<IGameManager>
            {
                new GameManager(GameName.Roll_a_Ball)
            };

            /*TEST*/
            gameManager = gameManagers.First();

            Header = MainHeader;
            DownloadBarValue = 0;

            ProcessButtonCommand = new DelegateCommand(async param => await ProcessButtonClick());
            SettingsButtonCommand = new DelegateCommand(param => Settings());
            FeedbackButtonCommand = new DelegateCommand(param => Feedback());

            Task.Factory.StartNew(async () => await Start());
        }

        #endregion

        #region Methods

        private async Task Start()
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            await StartWatcher(_token);
            await gameManager.StartupChecks();
        }

        private async Task ProcessButtonClick()
        {
            if (processButtonStatus != ProcessButtonStatus.Cancel)
            {
                await gameManager.StartProcess(_token);                
            }
            else
            {
                CancelProcesses();
            }
            
        }

        private void Settings()
        {
            
        }

        private void Feedback()
        {

        }

        private void CancelProcesses()
        {
            _tokenSource.Cancel();
            gameManager.CancelProcesses();
        }

        private async Task StartWatcher(CancellationToken token)
        {
            try
            {
                var task = await Task.Factory.StartNew(async () =>
                {
                    await Watcher(token);
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task Watcher(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                ProcessStatus = gameManager.ProcessStatus;
                ProcessButtonName = gameManager.ProcessButtonStatus;

                Debug.WriteLine("Watcher is worked.");
                await Task.Delay(100);
            }
        }

        #endregion
    }
}
