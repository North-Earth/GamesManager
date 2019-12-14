using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using GamesManager.Client.Views;

namespace GamesManager.Client.ViewModels
{
    public class MainControlViewModel
    {
        #region Fields

        private UserControl mainControl;

        public UserControl MainControl
        {
            get => mainControl;
            set => mainControl = value;
        }

        #endregion

        #region Constructors

        public MainControlViewModel() 
        {
            MainControl = new GamesLibraryView();
        }

        #endregion

        #region Methods

        #endregion
    }
}
