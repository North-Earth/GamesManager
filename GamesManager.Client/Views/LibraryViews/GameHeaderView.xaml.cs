using System.Windows.Controls;
using GamesManager.Client.ViewModels.LibraryViewModels;

namespace GamesManager.Client.Views
{
    /// <summary>
    /// Interaction logic for GameHeadView.xaml
    /// </summary>
    public partial class GameHeaderView : UserControl
    {
        public GameHeaderView(string header, string description)
        {
            DataContext = new GameHeaderViewModel(header, description);

            InitializeComponent();
        }
    }
}
