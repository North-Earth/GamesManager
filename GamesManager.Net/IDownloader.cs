using System;
using System.Threading;
using System.Threading.Tasks;
using GamesManager.Common.Events;

namespace GamesManager.Net
{
    public interface IDownloader
    {
        #region Fields

        public delegate void StatusChangedHandler(OperationStatusChangedEventArgs eventArgs);

        public event StatusChangedHandler StatusChangedEvent;

        #endregion

        #region Methods

        public Task DownloadFileAsync(Uri address, string fileName, CancellationToken token);

        #endregion

    }
}
