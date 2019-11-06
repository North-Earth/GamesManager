using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GamesManager.Common.Enums;
using GamesManager.Common.Events;

namespace GamesManager.Net
{
    public class Downloader : IDownloader
    {
        #region Fields

        public event AsyncCompletedEventHandler AsyncCompletedEventHandler;

        public event DownloadProgressChangedEventHandler DownloadProgressChangedEventHandler;

        #endregion

        #region Methods

        public async Task DownloadFileAsync(Uri address, string fileName, CancellationToken token)
        {
            try
            {
                var wc = new WebClient();

                wc.DownloadFileCompleted += AsyncCompletedEventHandler;
                wc.DownloadProgressChanged += DownloadProgressChangedEventHandler;

                token.Register(() => wc.CancelAsync());

                await wc.DownloadFileTaskAsync(address, fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
