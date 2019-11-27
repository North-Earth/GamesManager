using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace GamesManager.Launcher.Models
{
    public class WebClient : IWebClient
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
                var wc = new System.Net.WebClient();

                wc.DownloadFileCompleted += AsyncCompletedEventHandler;
                wc.DownloadProgressChanged += DownloadProgressChangedEventHandler;

                token.Register(() => wc.CancelAsync());

                await wc.DownloadFileTaskAsync(address, fileName).ConfigureAwait(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
