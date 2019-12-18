using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GamesManager.Client.Helpers.Binding;
using GamesManager.Client.Models.Enums;
using GamesManager.Client.Views;

namespace GamesManager.Client.ViewModels.LibraryViewModels
{
    public class GameControlViewModel : NotifyPropertyChanged
    {
        #region Fields

        private readonly GameName _gameName;

        private string name;
        private string backgroundImage;

        private ObservableCollection<GameNewsItemView> gameNewsItems;
        private UserControl gameHeadControl;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string BackgroundImage
        {
            get => backgroundImage;
            set => backgroundImage = value;
        }

        public ObservableCollection<GameNewsItemView> GameNewsItems
        {
            get => gameNewsItems;
            set => gameNewsItems = value;
        }

        public UserControl GameHeadControl
        {
            get => gameHeadControl;
            set => gameHeadControl = value;
        }

        #endregion

        #region Constructors

        public GameControlViewModel(GameName gameName)
        {
            _gameName = gameName;
            Name = gameName.ToString().Replace('_', ' ');

            if (gameName == GameName.Roll_a_Ball)
            {
                BackgroundImage = @"/Resources/Images/RaB_BackGround.png";
                GameHeadControl = new GameHeaderView("Incredibly simple game.", "This game is for everyone.");
                GameNewsItems = new ObservableCollection<GameNewsItemView>()
                {
                    new GameNewsItemView(),
                    new GameNewsItemView(),
                };
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}
