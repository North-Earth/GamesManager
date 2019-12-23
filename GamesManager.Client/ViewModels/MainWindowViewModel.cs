using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GamesManager.Client.Helpers.Binding;
using GamesManager.Client.Helpers.Commands;
using GamesManager.Client.Models;
using GamesManager.Client.ViewModels.LibraryViewModels;
using GamesManager.Client.Views;
using MaterialDesignThemes.Wpf;

namespace GamesManager.Client.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<UserControl> dialogs { get; set; }

        private UserControl rootControl;
        private UserControl rootDialog;
        private bool isOpenRootDialog;

        public UserControl RootControl
        {
            get => rootControl;
            set => rootControl = value;
        }

        public UserControl RootDialog
        {
            get => rootDialog;
            set
            {
                if (rootDialog == value) { return; }

                rootDialog = value;
                OnPropertyChanged(nameof(RootDialog));
            }
        }

        public bool IsOpenRootDialog
        {
            get => isOpenRootDialog;
            set
            {
                if (isOpenRootDialog == value) { return; }

                isOpenRootDialog = value;
                OnPropertyChanged(nameof(IsOpenRootDialog));
            }
        }

        public ICommand OpenDialogWhatsNewCommand => new Command(OpenDialogWhatsNew);
        #endregion

        #region Constructors

        public MainWindowViewModel() 
        {
            RootControl = new MainControlView();
            InitializeDialogs();
        }

        #endregion

        #region Methods

        private void InitializeDialogs()
        {
            dialogs = new ObservableCollection<UserControl>();

            //TODO: Add Logic.
            var newsItems = new ObservableCollection<NewsItemModel>
            {
                new NewsItemModel()
                {
                    Header = "Header",
                    Description = "Description",
                    ReleaseDate = DateTime.Now
                },
                new NewsItemModel()
                {
                    Header = "Games Manager was created",
                    Description = "Sample text. Sample text. Sample text. Sample text. Sample text. Sample text. Sample text." +
                    " Sample text. Sample text. Sample text. Sample text. Sample text. Sample text. Sample text. Sample text." +
                    " Sample text. Sample text. Sample text. Sample text. Sample text. Sample text. Sample text. Sample text.",
                    ReleaseDate = new DateTime(2019, 9, 28)
                },
            };

            var whatsNewsDialogView = new WhatsNewsDialogView
            {
                Name = nameof(WhatsNewsDialogView),
                DataContext = new WhatsNewsDialogViewModel(newsItems)
            };

            dialogs.Add(whatsNewsDialogView);
        }

        private void OpenRootDialog(string name)
        {
            RootDialog = dialogs.Where(dlg => dlg.Name.Equals(name)).Single();
            IsOpenRootDialog = true;
        }

        private void CloseRootDialog() => IsOpenRootDialog = false;

        private void OpenDialogWhatsNew(object obj) => OpenRootDialog(nameof(WhatsNewsDialogView));

        #endregion
    }
}
