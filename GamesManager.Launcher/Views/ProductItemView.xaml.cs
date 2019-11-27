using System.Windows.Controls;
using GamesManager.Common.Enums;
using GamesManager.Launcher.ViewModels;

namespace GamesManager.Launcher.Views
{
    /// <summary>
    /// Interaction logic for ProductItemView.xaml
    /// </summary>
    public partial class ProductItemView : UserControl
    {
        public ProductItemView()
        {
            InitializeComponent();
        }

        public ProductItemView(GameName gameName)
        {
            DataContext = new ProductItemViewModel(gameName);
            InitializeComponent();
        }
    }
}
