using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ControlzEx;
using GamesManager.Common.Enums;
using GamesManager.Common.Events;
using GamesManager.Launcher.Models;
using GamesManager.Launcher.Models.Enums;
using MaterialDesignThemes.Wpf;
using MVVM_Helper.Binding;
using MVVM_Helper.Commands;

namespace GamesManager.Launcher.ViewModels
{
    public class ProductItemViewModel : ObservableObject
    {
        #region Fields

        private const string updateStatus = "Update";

        public IGameManager GameManager { get; }

        private PackIconKind playButtonIcon;
        private GameState playButtonStatus;

        private string badgedText;

        private bool _isActive;
        private bool isEnabledPlayButton;
        private bool isIndeterminateProgressBar;

        private int progressBarValue;

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

        public bool IsEnabledPlayButton
        {
            get => isEnabledPlayButton;
            set
            {
                isEnabledPlayButton = value;
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

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
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

        public GameState PlayButtonStatus
        {
            get => playButtonStatus;
            set
            {
                playButtonStatus = value;
                switch (playButtonStatus)
                {
                    case GameState.Play:
                        PlayButtonIcon = PackIconKind.PlayCircleOutline;
                        break;
                    case GameState.Update:
                        BadgedText = updateStatus;
                        PlayButtonIcon = PackIconKind.Download;
                        break;
                    case GameState.Install:
                        PlayButtonIcon = PackIconKind.Download;
                        break;
                    case GameState.Cancel:
                        PlayButtonIcon = PackIconKind.StopCircleOutline;
                        break;
                    default:
                        break;
                }

                RaiseOnPropertyChanged();
            }
        }

        public DelegateCommand PlayButtonCommand { get; set; }

        #endregion

        #region Constructors

        public ProductItemViewModel(GameName gameName) 
        {
            GameManager = new GameManager(gameName);
            GameManager.StatusChangedEvent += GameManager_StatusChangedEvent;
            GameManager.CheckСondition();

            PlayButtonCommand = new DelegateCommand(param => PlayButtonClick());

        }

        #endregion

        #region Methods

        public void PlayButtonClick() => GameManager.StartProcess();

        private void GameManager_StatusChangedEvent(OperationStatusChangedEventArgs eventArgs)
        {
            PlayButtonStatus = eventArgs.GameState;
        }

        #endregion
    }
}
