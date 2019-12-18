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
using GamesManager.Client.Models.Enums;
using GamesManager.Client.ViewModels.LibraryViewModels;

namespace GamesManager.Client.Views
{
    /// <summary>
    /// Interaction logic for GameControlView.xaml
    /// </summary>
    public partial class GameControlView : UserControl
    {
        public readonly GameName GameName;

        public GameControlView()
        {
            InitializeComponent();
        }

        public GameControlView(GameName gameName)
        {
            GameName = gameName;
            DataContext = new GameControlViewModel(gameName);

            InitializeComponent();
        }
    }
}
