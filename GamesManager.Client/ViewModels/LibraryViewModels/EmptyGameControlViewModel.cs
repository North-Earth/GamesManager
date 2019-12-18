using System.Reflection;

namespace GamesManager.Client.ViewModels.LibraryViewModels
{
    public class EmptyGameControlViewModel
    {
        #region Fields

        private string version;

        public string Version
        {
            get => version;
            set => version = value;
        }

        #endregion

        #region Constructors

        public EmptyGameControlViewModel()
        {
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString(); //TODO: !!!!!!!!!!!!
        }

        #endregion
    }
}
