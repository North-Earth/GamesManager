using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ControlzEx;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Enums;
using MaterialDesignThemes.Wpf;
using MVVM_Helper.Binding;

namespace GamesManager.Launcher.ViewModels
{
    public class ProductItemViewModel : ObservableObject
    {
        #region Fields

        private const string updateStatus = "Update";

        private string _productName;
        private string badgedText;

        private bool _isActive;
        private bool isEnabledPlayButton;

        private PackIconKind playButtonIcon;
        private GameState playButtonStatus;
        private bool isIndeterminateProgressBar;
        private int progressBarValue;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                RaiseOnPropertyChanged();
            }
        }

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

        #endregion

        #region Constructors

        public ProductItemViewModel() { }

        public ProductItemViewModel(string productName, bool isActive = true)
        {
            ProductName = productName;
            IsActive = isActive;
            PlayButtonStatus = GameState.Install;

            var task = Task.Run(() =>
            {
                for (int i = 0; i < 101; i++)
                {
                    ProgressBarValue = i;
                    Task.Delay(250).Wait();
                }
                PlayButtonStatus = GameState.Update;
                IsIndeterminateProgressBar = true;
            });
        }

        #endregion

        #region Methods

        #endregion
    }
}
