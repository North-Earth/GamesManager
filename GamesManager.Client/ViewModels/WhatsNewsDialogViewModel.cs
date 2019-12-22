using System.Collections.Generic;
using System.Collections.ObjectModel;
using GamesManager.Client.Helpers.Binding;
using GamesManager.Client.Models;

namespace GamesManager.Client.ViewModels
{
    public class WhatsNewsDialogViewModel : NotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<NewsItemModel> _newsItems;

        public string Header { get; set; }

        public ObservableCollection<NewsItemModel> NewsItems
        {
            get => _newsItems;
            set => _newsItems = value;
        }

        #endregion

        #region Constructors

        public WhatsNewsDialogViewModel(ObservableCollection<NewsItemModel> newsItems)
        {
            Header = "What's News"; //TODO: Resourses.
            NewsItems = newsItems;
        }

        #endregion
    }
}
