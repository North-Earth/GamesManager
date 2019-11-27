using System;
using System.Net;
using System.Threading.Tasks;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models;
using GamesManager.Launcher.Models.Enums;
using GamesManager.Launcher.Models.Events;
using MaterialDesignThemes.Wpf;
using MVVM_Helper.Binding;
using MVVM_Helper.Commands;

namespace GamesManager.Launcher.ViewModels
{
    public class ProductItemViewModel : ObservableObject, IDisposable
    {
        private const string UPDATE_STATUS = "Update"; // TODO: Get from resoures.

        #region Fields

        public IGameManager GameManager { get; }

        private bool isEnabledPlayButton;
        private int progressBarValue;
        private bool isIndeterminateProgressBar;
        private string badgedText;
        private PackIconKind playButtonIcon;
        private PlayButtonState playButtonStatus;
        private OperationState operationState;

        public string BadgedText
        {
            get => badgedText;
            set
            {
                badgedText = value;
                RaiseOnPropertyChanged();
            }
        }

        public int ProgressBarValue
        {
            get => progressBarValue;
            set
            {
                progressBarValue = value;
                RaiseOnPropertyChanged();
            }
        }

        public bool IsIndeterminateProgressBar
        {
            get => isIndeterminateProgressBar;
            set
            {
                isIndeterminateProgressBar = value;
                ProgressBarValue = -1;
                RaiseOnPropertyChanged();
            }
        }

        public bool IsEnabledPlayButton
        {
            get => isEnabledPlayButton;
            set
            {
                isEnabledPlayButton = value;
                RaiseOnPropertyChanged();
            }
        }

        public PackIconKind PlayButtonIcon
        {
            get => playButtonIcon;
            set
            {
                playButtonIcon = value;
                RaiseOnPropertyChanged();
            }
        }

        public PlayButtonState PlayButtonStatus
        {
            get => playButtonStatus;
            set
            {
                playButtonStatus = value;

                switch (playButtonStatus)
                {
                    case PlayButtonState.Play:
                        PlayButtonIcon = PackIconKind.PlayCircleOutline;
                        break;
                    case PlayButtonState.Install:
                        PlayButtonIcon = PackIconKind.Download;
                        break;
                    case PlayButtonState.Update:
                        BadgedText = UPDATE_STATUS;
                        PlayButtonIcon = PackIconKind.Download;
                        break;
                    case PlayButtonState.Cancel:
                        PlayButtonIcon = PackIconKind.StopCircleOutline;
                        break;
                    case PlayButtonState.Wait:
                        PlayButtonIcon = PackIconKind.TimerSand;
                        break;
                }

                RaiseOnPropertyChanged();
            }
        }

        public OperationState OperationState
        {
            get => operationState;
            set
            {
                operationState = value;
                switch (operationState)
                {
                    case OperationState.Completed:
                        ProgressBarValue = 0;
                        IsIndeterminateProgressBar = false;
                        IsEnabledPlayButton = true;
                        break;
                    case OperationState.Playing:
                        ProgressBarValue = -1;
                        IsIndeterminateProgressBar = true;
                        IsEnabledPlayButton = false;
                        break;
                    case OperationState.Downloading:
                        IsIndeterminateProgressBar = false;
                        IsEnabledPlayButton = true;
                        break;
                    case OperationState.Installing:
                        ProgressBarValue = -1;
                        IsIndeterminateProgressBar = true;
                        IsEnabledPlayButton = true;
                        break;
                    case OperationState.Canceling:
                        ProgressBarValue = -1;
                        IsIndeterminateProgressBar = true;
                        IsEnabledPlayButton = false;
                        break;
                    case OperationState.Checking:
                        ProgressBarValue = -1;
                        IsIndeterminateProgressBar = true;
                        IsEnabledPlayButton = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public DelegateCommand PlayButtonCommand { get; set; }

        #endregion

        #region Constructors

        public ProductItemViewModel() { throw new NotImplementedException(); }

        public ProductItemViewModel(GameName gameName)
        {
            GameManager = new GameManager(gameName);
            GameManager.DownloadProgressChanged += GameManager_DownloadProgressChangedEventHandler;
            GameManager.OperationStatusChanged += GameManager_OperationStatusChanged;

            PlayButtonCommand = new DelegateCommand(param => PlayButtonClick());

            Task.Run(() => GameManager.CheckСondition());
        }


        #endregion

        #region Methods

        private void PlayButtonClick()
        {
            GameManager.StartProcess();
        }

        private void GameManager_DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs args)
        {
            if (ProgressBarValue != args.ProgressPercentage && OperationState != OperationState.Canceling)
            {
                ProgressBarValue = args.ProgressPercentage;
            }
        }
        private void GameManager_OperationStatusChanged(object sender, OperationStatusChangedEventArgs args)
        {
            PlayButtonStatus = args.GameState;
            OperationState = args.OperationState;
        }

        public void Dispose()
        {
            GameManager.DownloadProgressChanged -= GameManager_DownloadProgressChangedEventHandler;
        }

        #endregion
    }
}
