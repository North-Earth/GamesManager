using System.Collections.ObjectModel;
using GamesManager.Client.Helpers.Binding;
using GamesManager.Client.Models.Enums;
using GamesManager.Client.Views;

namespace GamesManager.Client.ViewModels
{
    public class GameControlViewModel : NotifyPropertyChanged
    {
        #region Fields

        private readonly GameName _gameName;
        private string name;
        private ObservableCollection<GameNewsItemView> gameNewsItems;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ObservableCollection<GameNewsItemView> GameNewsItems
        {
            get => gameNewsItems;
            set => gameNewsItems = value;
        }

        #endregion

        #region Constructors

        public GameControlViewModel()
        {

        }

        public GameControlViewModel(GameName gameName)
        {
            _gameName = gameName;
            Name = gameName.ToString().Replace('_', ' ');

            GameNewsItems = new ObservableCollection<GameNewsItemView>()
            {
                new GameNewsItemView(),
                new GameNewsItemView()
            };
        }

        #endregion

        #region Methods

        #endregion
    }
}
