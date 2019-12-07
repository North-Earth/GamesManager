using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace GamesManager.Launcher.Models
{
    public interface IWebClient
    {
        #region Fields

        event AsyncCompletedEventHandler AsyncCompletedEventHandler;

        event DownloadProgressChangedEventHandler DownloadProgressChangedEventHandler;

        #endregion

        #region Methods

        Task DownloadFileAsync(Uri address, string fileName, CancellationToken token);

        #endregion
    }
}
