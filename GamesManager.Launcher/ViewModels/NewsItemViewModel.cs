using GamesManager.Launcher.Helper.Binding;

namespace GamesManager.Launcher.ViewModels
{
    public class NewsItemViewModel : ObservableObject
    {
        #region Fields

        private string _headerText;
        private string _bodyText;

        public string HeaderText
        {
            get => _headerText;
            set
            {
                _headerText = value;
                RaiseOnPropertyChanged();
            }
        }

        public string BodyText
        {
            get => _bodyText;
            set
            {
                _bodyText = value;
                RaiseOnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public NewsItemViewModel()
        {

        }

        #endregion

        #region Methods

        #endregion
    }
}
