using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MainWindowViewModel
    {
        #region Fields

        private UserControl userControl;

        public UserControl UserControl
        {
            get => userControl;
            set => userControl = value;
        }

        public ICommand OpenDialogWhatsNewCommand => new Command(OpenDialogWhatsNew);
        #endregion

        #region Constructors

        public MainWindowViewModel() 
        {
            UserControl = new MainControlView();
        }

        #endregion

        #region Methods

        private async void OpenDialogWhatsNew(object obj)
        {
            var newsItems = new ObservableCollection<NewsItemModel>
            { 
                new NewsItemModel() 
                {
                    Header = "Header",
                    Description = "Description",
                    ReleaseDate = DateTime.Now 
                }
            };

            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new WhatsNewsDialogView
            {
                DataContext = new WhatsNewsDialogViewModel(newsItems)
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            bool isDebug = true;
        }

        #endregion
    }
}
