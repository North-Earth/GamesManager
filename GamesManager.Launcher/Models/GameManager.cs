using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Enums;
using GamesManager.Launcher.Models.Events;
using Newtonsoft.Json;

namespace GamesManager.Launcher.Models
{
    public class GameManager : IGameManager
    {
        #region Fields

        private const string CachePath = "cache";
        private const string AppsPath = "apps";

        private ProcessStatus Status { get; set; }
        private PlayButtonStatus ButtonStatus { get; set; }

        private int progressPercentage;
        private int ProgressPercentage
        {
            get => progressPercentage;
            set
            {
                progressPercentage = value;
                InvokeStatusesChangedHandler();
            }
        }

        private VersionInfo LatestVersionInfo { get; set; }

        public GameName GameName { get; }

        public event IGameManager.StatusesChangedHandler StatusesChangedEvent;

        #endregion

        #region Constructors

        public GameManager(GameName gameName) => GameName = gameName;

        #endregion

        #region Methods

        private void InvokeStatusesChangedHandler()
            => StatusesChangedEvent(new GameManagerStatusesChangedEventArgs(Status, ButtonStatus, ProgressPercentage));

        private void UpdateStatus(ProcessStatus status, PlayButtonStatus buttonStatus)
        {
            Status = status;
            ButtonStatus = buttonStatus;
            InvokeStatusesChangedHandler();
        }

        private void ResetProgressPercentage()
            => ProgressPercentage = 0;

        public async Task StartupChecks()
        {
            UpdateStatus(ProcessStatus.Checking, PlayButtonStatus.Cancel);

            try
            {
                LatestVersionInfo = await GetLatestVersionInfo().ConfigureAwait(true);

                if (IsInstalled())
                {
                    if (IsUpdated())
                    {
                        UpdateStatus(ProcessStatus.Complete, PlayButtonStatus.Play);
                    }
                    else
                    {
                        UpdateStatus(ProcessStatus.Done, PlayButtonStatus.Update);
                    }
                }
                else
                {
                    UpdateStatus(ProcessStatus.Done, PlayButtonStatus.Install);
                }
            }
            catch (Exception)
            {
                //TODO: 
                throw;
            }
        }

        public async Task StartProcess(CancellationToken token)
        {
            try
            {
                switch (ButtonStatus)
                {
                    case PlayButtonStatus.Play:
                        await Play();
                        break;
                    case PlayButtonStatus.Install:
                        await Install(token).ConfigureAwait(false);
                        break;
                    case PlayButtonStatus.Update:
                        await Update(token).ConfigureAwait(false);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task Install(CancellationToken token)
        {
            try
            {
                UpdateStatus(ProcessStatus.Checking, PlayButtonStatus.Cancel);

                LatestVersionInfo = await GetLatestVersionInfo().ConfigureAwait(false);

                UpdateStatus(ProcessStatus.Downloading, PlayButtonStatus.Cancel);

                await Task.Run(async () => await DownloadGame(LatestVersionInfo, token).ConfigureAwait(false), token).ConfigureAwait(false);

                UpdateStatus(ProcessStatus.Installing, PlayButtonStatus.Cancel);

                await Task.Run(async () => await InstallGame(LatestVersionInfo, token).ConfigureAwait(false), token).ConfigureAwait(false);

                UpdateStatus(ProcessStatus.Complete, PlayButtonStatus.Play);
            }
            catch (OperationCanceledException)
            {
                UpdateStatus(ProcessStatus.Done, PlayButtonStatus.Install);
                ResetProgressPercentage();
                throw;
            }
            catch (Exception)
            {
                UpdateStatus(ProcessStatus.Error, PlayButtonStatus.Install);
                throw;
            }
        }

        private async Task Update(CancellationToken token)
        {
            throw new Exception();
        }

        private async Task Play()
        {
            try
            {
                UpdateStatus(ProcessStatus.Playing, PlayButtonStatus.Play);

                var gameName = GameName.ToString().Replace('_', ' ');
                var path = Path.Combine(AppsPath, gameName, gameName + ".exe");

                var proc = Process.Start(path);

                proc.WaitForExit();

                UpdateStatus(ProcessStatus.Complete, PlayButtonStatus.Play);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DownloadGame(VersionInfo versionInfo, CancellationToken token)
        {
            var webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

            try
            {
                if (!IsDownloaded())
                {
                    // TODO: Explore and understand.
                    token.Register(() =>
                    {
                        webClient.CancelAsync(); // This does not work...
                    });

                    var uri = versionInfo.Uri;
                    var path = Path.Combine(CachePath, versionInfo.FileName);

                    if (!Directory.Exists(CachePath))
                    {
                        Directory.CreateDirectory(CachePath);
                    }

#warning Download continues even after cancellation.
                    var t = Task.Run(async () => await webClient.DownloadFileTaskAsync(uri, path).ConfigureAwait(false), token);

                    // Works instead of instead WebClient.CancelAsync().
                    while (!t.IsCompleted)
                    {
                        if (token.IsCancellationRequested)
                        {
                            token.ThrowIfCancellationRequested();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                webClient.DownloadProgressChanged -= WebClient_DownloadProgressChanged;
                webClient.DownloadFileCompleted -= WebClient_DownloadFileCompleted;
                webClient.Dispose();
            }
        }

        private async Task InstallGame(VersionInfo versionInfo, CancellationToken token)
        {
            try
            {
                var arhivePath = Path.Combine(CachePath, versionInfo.FileName);

                if (!Directory.Exists(AppsPath))
                {
                    Directory.CreateDirectory(AppsPath);
                }

                await Task.Run(() => System.IO.Compression.ZipFile.ExtractToDirectory(arhivePath, AppsPath), token).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            => ProgressPercentage = e.ProgressPercentage;

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
            => UpdateStatus(ProcessStatus.Done, ButtonStatus);

        private bool IsInstalled()
        {
            var gameName = GameName.ToString().Replace('_', ' ');
            var path = Path.Combine(AppsPath, gameName);

            if (Directory.Exists(AppsPath))
            {
                var filePath = Path.Combine(path, gameName + ".exe");

                FileInfo file = new FileInfo(filePath);

                //TODO: Check hash.
                if (file.Exists && file.Length == LatestVersionInfo.Size)
                    return true;
            }

            return false;
        }

        private bool IsUpdated()
        {
            //TODO: Сheck the version of the installed the game.
            return false;
        }

        private bool IsDownloaded()
        {
            if (Directory.Exists(CachePath))
            {
                var path = Path.Combine(CachePath, LatestVersionInfo.FileName);

                FileInfo file = new FileInfo(path);

                //TODO: Check hash.
                if (file.Exists && file.Length == LatestVersionInfo.Size)
                    return true;
            }

            return false;
        }

        private async Task<VersionInfo> GetLatestVersionInfo()
        {
            VersionInfo latestVersionInfo = default;

            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(hours: 0, minutes: 0, seconds: 10);

                var uri = new Uri(@$"https://localhost:5001/gamemanager/{GameName}");
                var releaseJson = await client.GetStringAsync(uri).ConfigureAwait(false);

                latestVersionInfo = JsonConvert.DeserializeObject<VersionInfo>(releaseJson);
            }

            return latestVersionInfo;
        }

        #endregion
    }
}
