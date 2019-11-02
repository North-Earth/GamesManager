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
using GamesManager.Launcher.Models.Events;
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
                    case ProcessStatus.Waiting:
                    case ProcessStatus.Playing:
                        IsEnableProcessButton = false;
                        IsIndeterminateDownloadBar = true;
                        downloadBarValue = 0;
                        break;
                    case ProcessStatus.Installing:
                        IsIndeterminateDownloadBar = true;
                        downloadBarValue = 0;
                        break;
                    case ProcessStatus.Downloading:
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

            ProcessButtonCommand = new DelegateCommand(param => ProcessButtonClick());
            SettingsButtonCommand = new DelegateCommand(param => SettingsButtonClick());
            FeedbackButtonCommand = new DelegateCommand(param => FeedbackButtonClick());

            // Check Game State.
            gameManager.StatusesChangedEvent += GameManager_StatusesChangedEvent;
            Task.Run(async () => await gameManager.StartupChecks().ConfigureAwait(false));
        }

        #endregion

        #region Methods

        private void GameManager_StatusesChangedEvent(GameManagerStatusesChangedEventArgs eventArgs)
        {
            if (ProcessStatus != eventArgs.Status)
            {
                ProcessStatus = eventArgs.Status;
            }

            if (ProcessButtonName != eventArgs.ButtonStatus)
            {
                ProcessButtonName = eventArgs.ButtonStatus;
            }

            if (DownloadBarValue != eventArgs.ProgressPercentage)
            {
                DownloadBarValue = eventArgs.ProgressPercentage;
            }

            Debug.WriteLine("Statuses changed Invoked!");
        }

        private void ProcessButtonClick()
        {
            if (processButtonStatus != ProcessButtonStatus.Cancel)
            {
                _tokenSource = new CancellationTokenSource();
                _token = _tokenSource.Token;

                Task.Run(async () => await StartProcess().ConfigureAwait(false), _tokenSource.Token);
            }
            else
            {
                try
                {
                    CancelProcesses();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void SettingsButtonClick()
        {

        }

        private void FeedbackButtonClick()
        {

        }

        private async Task StartProcess()
        {
            try
            {
                await gameManager.StartProcess(_token).ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                //TODO: Handle error.
                throw;
            }
            finally
            {
                _tokenSource.Dispose();
            }
        }

        private void CancelProcesses()
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }
        }

        #endregion
    }
}
