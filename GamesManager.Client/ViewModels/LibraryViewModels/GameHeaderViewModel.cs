using GamesManager.Client.Helpers.Binding;

namespace GamesManager.Client.ViewModels.LibraryViewModels
{
    public class GameHeaderViewModel : NotifyPropertyChanged
    {
        #region Fields

        private string headerText;
        private string descriptionText;

        public string HeaderText
        {
            get => headerText;
            set => headerText = value;
        }

        public string DescriptionText
        {
            get => descriptionText;
            set => descriptionText = value;
        }

        #endregion

        #region Constructors

        public GameHeaderViewModel(string header, string description)
        {
            HeaderText = header;
            DescriptionText = description;
        }

        #endregion
    }
}
