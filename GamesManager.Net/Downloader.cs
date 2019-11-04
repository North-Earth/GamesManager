using System;
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

        private int _progressPercentage;

        private OperationState OperationState { get; set; }
        private GameState GameState { get; set; }
        private int ProgressPercentage
        {
            get => _progressPercentage;
            set
            {
                _progressPercentage = value;
                InvokeStatusChangedEvent();
            }
        }

        public event IDownloader.StatusChangedHandler StatusChangedEvent;

        #endregion

        #region Methods

        public async Task DownloadFileAsync(Uri address, string fileName, CancellationToken token)
        {
            try
            {
                var wc = new WebClient();

                wc.DownloadFileCompleted += WebCLient_DownloadFileCompleted;
                wc.DownloadProgressChanged += WebCLient_DownloadProgressChanged;

                token.Register(() => wc.CancelAsync());

                await wc.DownloadFileTaskAsync(address, fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ChangeStatus(OperationState operationState, GameState gameState)
        {
            OperationState = operationState;
            GameState = gameState;

            InvokeStatusChangedEvent();
        }

        private void InvokeStatusChangedEvent()
            => StatusChangedEvent(new OperationStatusChangedEventArgs(OperationState, GameState, ProgressPercentage));

        private void WebCLient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            OperationState = OperationState.Completed;
            InvokeStatusChangedEvent();
        }

        private void WebCLient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressPercentage = e.ProgressPercentage;
            //TODO: Send file size.
        }

        #endregion
    }
}
