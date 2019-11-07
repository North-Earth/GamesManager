using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GamesManager.Common.Enums;
using GamesManager.Launcher.ViewModels;

namespace GamesManager.Launcher.Views
{
    /// <summary>
    /// Interaction logic for ProductItemView.xaml
    /// </summary>
    public partial class ProductItemView : UserControl
    {
        public ProductItemView(GameName gameName)
        {
            DataContext = new ProductItemViewModel(gameName);

            InitializeComponent();
        }
    }
}
