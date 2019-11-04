using System;
using System.Collections.Generic;
using System.Text;
using ControlzEx;
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

        private PackIconKind playButtonIcon;
        private PlayButtonStatus playButtonStatus;
        private bool isPlayButtonEnabled;

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

        public bool IsPlayButtonEnabled
        {
            get => isPlayButtonEnabled;
            set
            {
                isPlayButtonEnabled = value;
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

        public PlayButtonStatus PlayButtonStatus
        {
            get => playButtonStatus;
            set
            {
                playButtonStatus = value;
                switch (playButtonStatus)
                {
                    case PlayButtonStatus.Play:
                        PlayButtonIcon = PackIconKind.PlayCircleOutline;
                        break;
                    case PlayButtonStatus.Install:
                    case PlayButtonStatus.Update:
                        PlayButtonIcon = PackIconKind.Download;
                        break;
                    case PlayButtonStatus.Cancel:
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
            BadgedText = updateStatus;
            PlayButtonStatus = PlayButtonStatus.Install;
        }

        #endregion

        #region Methods

        #endregion
    }
}
