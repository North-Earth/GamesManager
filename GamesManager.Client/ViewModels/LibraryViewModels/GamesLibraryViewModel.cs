using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using GamesManager.Client.Helpers.Binding;
using GamesManager.Client.Models;
using GamesManager.Client.Models.Enums;
using GamesManager.Client.Views;

namespace GamesManager.Client.ViewModels.LibraryViewModels
{
    public class GamesLibraryViewModel : NotifyPropertyChanged
    {
        #region Fields

        private readonly ObservableCollection<GameControlView> _gameControls;

        private UserControl gameControl;
        private ObservableCollection<LibraryItemModel> libraryItems;
        private LibraryItemModel selectedLibraryItem;

        public UserControl GameControl
        {
            get => gameControl;
            set
            {
                gameControl = value;
                OnPropertyChanged(nameof(GameControl));
            }
        }

        public ObservableCollection<LibraryItemModel> LibraryItems
        {
            get => libraryItems;
            set => libraryItems = value;
        }

        public LibraryItemModel SelectedLibraryItem
        {
            get => selectedLibraryItem;
            set
            {
                GameControl = _gameControls
                    .Where(gc => gc.GameName.Equals(value.GameName))
                    .First();

                selectedLibraryItem = value;
            }
        }

        #endregion

        #region Constructors

        public GamesLibraryViewModel()
        {
            GameControl = new EmptyGameControlView();
            _gameControls = new ObservableCollection<GameControlView>()
            {
                new GameControlView(GameName.Roll_a_Ball),
                new GameControlView(GameName.The_Roll_Out)
            };

            LibraryItems = new ObservableCollection<LibraryItemModel>()
            {
                new LibraryItemModel()
                {
                    Icon = MaterialDesignThemes.Wpf.PackIconKind.QuestionMarkCircleOutline,
                    Name = "Roll a Ball",
                    GameName = GameName.Roll_a_Ball,
                },
                new LibraryItemModel()
                {
                    Icon = MaterialDesignThemes.Wpf.PackIconKind.Atom,
                    Name = "The Roll Out",
                    GameName = GameName.The_Roll_Out,
                },
            };
        }

        #endregion

        #region Methods

        #endregion
    }
}
