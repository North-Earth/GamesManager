using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Enums;
using GamesManager.Launcher.Models.Events;

namespace GamesManager.Launcher.Models
{
    public class GameManager : IGameManager
    {
        #region Fields

        public GameName GameName { get; }

        public string Name { get; }

        public OperationState OperationState { get; private set; }
        public PlayButtonState PlayButtonState { get; private set; }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;

        public event OperationStatusChangedEventHandler OperationStatusChanged;

        private IRestClient RestClient;

        private IWebClient WebClient;

        private CancellationTokenSource TokenSource;

        private Uri VersionUri { get; }

        #endregion

        #region Constructors

        public GameManager(GameName gameName)
        {
            GameName = gameName;
            Name = DirectoryManager.ConvertEnumToName(GameName);

            //TODO: Add DI.
            RestClient = new RestClient();
            WebClient = new WebClient();

            VersionUri = new Uri($@"https://localhost:5001/gamemanager/{GameName}"); //TODO: Using resourse or config.

            DirectoryManager.CreateApplicationDirectory();
        }

        #endregion

        #region Methods

        public void CheckСondition()
        {
            UpdateStatus(OperationState.Checking, PlayButtonState.Wait);

            Task.Delay(5000).Wait();

            PlayButtonState playButtonState;

            if (IsInstalled)
            {
                playButtonState = IsUpdated() ? PlayButtonState.Play : PlayButtonState.Update;
            }
            else
            {
                playButtonState = PlayButtonState.Install;
            }

            UpdateStatus(OperationState.Completed, playButtonState);
        }

        private void UpdateStatus(OperationState operationState, PlayButtonState playButtonState)
        {
            OperationState = operationState;
            PlayButtonState = playButtonState;

            OperationStatusChanged?.Invoke(this, new OperationStatusChangedEventArgs(OperationState, PlayButtonState));
        }

        public void StartProcess()
        {
            if (TokenSource == null || TokenSource.IsCancellationRequested == true)
            {
                TokenSource = new CancellationTokenSource();
            }

            switch (PlayButtonState)
            {
                case PlayButtonState.Play:
                    Task.Run(() => Play(TokenSource.Token), TokenSource.Token);
                    break;
                case PlayButtonState.Install:
                    Task.Run(async () => await Install(TokenSource.Token).ConfigureAwait(true), TokenSource.Token);
                    break;
                case PlayButtonState.Update:
                    Task.Run(async () => await Update(TokenSource.Token).ConfigureAwait(true), TokenSource.Token);
                    break;
                case PlayButtonState.Cancel:
                    Task.Run(() => Cancel());
                    break;
                default:
                    break;
            }
        }

        private void Play(CancellationToken token)
        {
            try
            {
                UpdateStatus(OperationState.Playing, PlayButtonState.Play);

                var proc = Process.Start(fileName: DirectoryManager.GetPathGame(GameName));

                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{nameof(Play)}\n" + ex.Message + "\n" + ex.ToString());
                throw;
            }
            finally
            {
                CheckСondition();
            }
        }

        private async Task Install(CancellationToken token)
        {
            try
            {
                UpdateStatus(OperationState.Downloading, PlayButtonState.Cancel);

                var latestVersion = await GetUpdate(token).ConfigureAwait(true);
                var cachePath = Path.Combine(DirectoryManager.CachePath, latestVersion.FileName);

                WebClient.DownloadProgressChangedEventHandler += DownloadProgressChanged;
                WebClient.AsyncCompletedEventHandler += (s, e) =>
                {
                    Debug.WriteLine("Complited");
                    /* TODO: */
                };

                if (!DirectoryManager.IsExistsFile(cachePath, latestVersion.Size))
                {
                    await WebClient.DownloadFileAsync(latestVersion.Uri, cachePath, token).ConfigureAwait(true);
                }


                UpdateStatus(OperationState.Installing, PlayButtonState.Cancel);

                DirectoryManager.ExtractToDirectory(cachePath, DirectoryManager.AppsPath);

                UpdateStatus(OperationState.Completed, PlayButtonState.Play);

            }
            catch (OperationCanceledException)
            {
                TokenSource.Dispose();
                TokenSource = null;

                throw;
            }
            catch (Exception ex)
            {
                //TODO: Notification about error.
                MessageBox.Show($"{nameof(Install)}\n" + ex.Message + "\n" + ex.ToString());
                throw;
            }
            finally
            {
                WebClient.DownloadProgressChangedEventHandler -= DownloadProgressChanged;
                CheckСondition();
            }
        }

        private async Task Update(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private async Task<VersionInfo> GetUpdate(CancellationToken token)
            => await RestClient.GetAsync<VersionInfo>(VersionUri, token).ConfigureAwait(true);

        private void Cancel()
        {
            UpdateStatus(OperationState.Canceling, PlayButtonState.Cancel);
            Task.Delay(5000).Wait();

            TokenSource.Cancel();
            TokenSource.Dispose();
        }

        private bool IsInstalled => DirectoryManager.IsInstaled(GameName);

        private bool IsUpdated()
        {
            //TODO: Implement.
            return true;
        }

        #endregion

    }
}
