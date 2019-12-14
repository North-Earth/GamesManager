using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using GamesManager.Client.Views;

namespace GamesManager.Client.ViewModels
{
    public class GamesLibraryViewModel
    {
        #region Fields

        private GameControlView gameControl;

        public GameControlView GameControl
        {
            get => gameControl;
            set => gameControl = value;
        }

        #endregion

        #region Constructors

        public GamesLibraryViewModel() 
        {
            GameControl = new GameControlView();
        }

        #endregion

        #region Methods

        #endregion
    }
}
