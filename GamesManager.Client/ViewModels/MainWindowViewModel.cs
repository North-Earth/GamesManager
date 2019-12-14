using System.Windows.Controls;
using GamesManager.Client.Views;

namespace GamesManager.Client.ViewModels
{
    public class MainWindowViewModel
    {
        #region Fields

        private UserControl userControl;

        public UserControl UserControl
        {
            get => userControl;
            set => userControl = value;
        }

        #endregion

        #region Constructors

        public MainWindowViewModel() 
        {
            UserControl = new MainControlView();
        }

        #endregion

        #region Methods

        #endregion
    }
}
