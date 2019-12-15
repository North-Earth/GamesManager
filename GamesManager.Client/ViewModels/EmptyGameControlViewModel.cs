using System.Reflection;

namespace GamesManager.Client.ViewModels
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
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        #endregion

        #region Methods

        #endregion
    }
}
