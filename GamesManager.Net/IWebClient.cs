using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace GamesManager.Net
{
    public interface IWebClient
    {
        #region Fields

        public event AsyncCompletedEventHandler AsyncCompletedEventHandler;

        public event DownloadProgressChangedEventHandler DownloadProgressChangedEventHandler;

        #endregion

        #region Methods

        public Task DownloadFileAsync(Uri address, string fileName, CancellationToken token);

        #endregion

    }
}
