using System.Net;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Events;

namespace GamesManager.Launcher.Models
{
    public interface IGameManager
    {
        #region Fields
        string Name { get; }

        GameName GameName { get; }

        event DownloadProgressChangedEventHandler DownloadProgressChanged;

        event OperationStatusChangedEventHandler OperationStatusChanged;

        #endregion

        #region Methods

        void CheckСondition();

        void StartProcess();

        #endregion
    }
}
