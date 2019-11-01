using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Enums;
using GamesManager.Launcher.Models.Events;
using GamesManager.Launcher.Properties;
using Newtonsoft.Json;

namespace GamesManager.Launcher.Models
{
    public class GameManager : IGameManager
    {
        #region Fields

        private ProcessStatus status;
        private ProcessButtonStatus buttonStatus;
        private int progressPercentage;

        public GameName GameName { get; }

        private ProcessStatus Status
        {
            get => status;
            set
            {
                status = value;
                InvokeStatusesChangedHandler();
            }
        }
        private ProcessButtonStatus ButtonStatus
        {
            get => buttonStatus;
            set
            {
                buttonStatus = value;
                InvokeStatusesChangedHandler();
            }
        }
        private int ProgressPercentage
        {
            get => progressPercentage;
            set
            {
                progressPercentage = value;
                InvokeStatusesChangedHandler();
            }
        }

        public event IGameManager.StatusesChangedHandler StatusesChanged;

        #endregion

        #region Constructors

        public GameManager(GameName gameName)
        {
            GameName = gameName;
        }

        #endregion

        #region Methods

        private void InvokeStatusesChangedHandler() 
            => StatusesChanged(eventArgs: new GameManagerStatusesChangedEventArgs(Status, ButtonStatus, ProgressPercentage));

        public async Task StartupChecks()
        {
            Status = ProcessStatus.Checking;

            try
            {
                if (IsInstalled())
                {
                    ButtonStatus = await IsUpdated() ? ProcessButtonStatus.Play : ProcessButtonStatus.Update;
                }
                else
                {
                    ButtonStatus = ProcessButtonStatus.Install;
                }
            }
            catch (Exception)
            {

                throw;
            }

            Status = ProcessStatus.Done;
        }

        public async Task StartProcess(CancellationToken token)
        {
            switch (ButtonStatus)
            {
                case ProcessButtonStatus.Play:
                    break;
                case ProcessButtonStatus.Install:
                    //await Install(token);
                    break;
                case ProcessButtonStatus.Update:

                    break;
                case ProcessButtonStatus.Cancel:
                    break;
                default:
                    break;
            }

            await StartupChecks();
        }

        public void CancelProcesses()
        {
            //_webClient.CancelAsync();
        }

        private async Task Install(CancellationToken token)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            var latestVersion = await GetLatestVersionInfo();




            Status = ProcessStatus.Installing;
            ButtonStatus = ProcessButtonStatus.Cancel;
            await Task.Delay(5000);
        }

        private async Task Download(CancellationToken token, VersionInfo versionInfo)
        {
            Status = ProcessStatus.Downloading;
            ButtonStatus = ProcessButtonStatus.Cancel;

            var webClient = new WebClient();
            //webClient.DownloadFileCompleted += (s, e) => throw new NotImplementedException();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

            token.Register(() =>
            {
                webClient.CancelAsync();
                Status = ProcessStatus.Waiting;
            });

            try
            {
                await webClient.DownloadFileTaskAsync(versionInfo.Uri, Path.Combine("cache", versionInfo.FileName));
            }
            catch (TaskCanceledException)
            {
                Status = ProcessStatus.Done;
                ButtonStatus = ProcessButtonStatus.Install;
            }
            catch (Exception)
            {
                Status = ProcessStatus.Error;
                throw;
            }
            finally
            {
                webClient.DownloadProgressChanged -= WebClient_DownloadProgressChanged;
                webClient.DownloadFileCompleted -= WebClient_DownloadFileCompleted;
                webClient.Dispose();
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressPercentage = e.ProgressPercentage;
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Status = ProcessStatus.Done;
        }


        private bool IsInstalled()
        {
            Task.Delay(5000);
            return false;
        }

        private async Task<bool> IsUpdated()
        {
            try
            {
                var latestVersion = await GetLatestVersionInfo();

                var currentVersion = Resources.ResourceManager.GetString(GameName.ToString(), new CultureInfo("en-US", false));

                return latestVersion.Version.Equals(currentVersion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<VersionInfo> GetLatestVersionInfo()
        {
            VersionInfo latestVersionInfo = default;

            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(hours: 0, minutes: 0, seconds: 10);

                var uri = new Uri(@$"https://localhost:5001/gamemanager/{GameName}");
                var releaseJson = await client.GetStringAsync(uri);

                latestVersionInfo = JsonConvert.DeserializeObject<VersionInfo>(releaseJson);
            }

            return latestVersionInfo;
        }


        //WebClient webClient;

        //public async Task StartProcess(CancellationToken token)
        //{
        //    try
        //    {
        //        await await Task.Factory.StartNew(async () =>
        //        {
        //            var latestVersion = await GetLatestVersionInfo(gameName);

        //            token.Register(() => webClient.CancelAsync());

        //                await webClient.DownloadFileTaskAsync(latestVersion.Uri, Path.Combine(CACHE_PATH, latestVersion.FileName));

        //        }, tokenSource.Token);
        //    }
        //    catch (OperationCanceledException ex)
        //    {
        //        Debug.WriteLine($"{nameof(StartProcess)}: STOPING!");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        tokenSource.Dispose();
        //        tokenSource = null;
        //    }
        //}

        //public void CancelProcess()
        //{
        //    webClient.CancelAsync();
        //    tokenSource.Cancel();
        //}

        ////************************************************************************************************//

        //public async Task Watcher()
        //{
        //    var latestVersion = await GetLatestVersionInfo(gameName);
        //    var completionPercent = 0;

        //    while (true)
        //    {
        //        var file = GetFile(CACHE_PATH, latestVersion.FileName);

        //        if (file != null)
        //        {
        //            file.Refresh();

        //            Debug.WriteLine($"{nameof(Watcher)}: {file.Length} / {file.Length} | {completionPercent}");

        //            if (file.Length > 0)
        //            {
        //                completionPercent = (int)((double)file.Length / (double)latestVersion.Size * 100);
        //            }
        //        }
        //        else
        //        {
        //            Debug.WriteLine($"{nameof(Watcher)}: File is not found!");
        //        }

        //        await Task.Delay(250);
        //    }
        //}

        //public async Task<LatestVersionInfo> GetLatestVersionInfo(GameName gameName)
        //{
        //    LatestVersionInfo latestVersionInfo = default;

        //    using (var client = new HttpClient())
        //    {
        //        var uri = new Uri(@$"https://localhost:5001/gamemanager/{gameName}");
        //        var releaseJson = await client.GetStringAsync(uri);

        //        latestVersionInfo = JsonConvert.DeserializeObject<LatestVersionInfo>(releaseJson);
        //    }

        //    return latestVersionInfo;
        //}

        //private FileInfo GetFile(string directoryPath, string fileName)
        //{
        //    var directory = new DirectoryInfo(directoryPath);
        //    var file = Array.Find(directory.GetFiles(),
        //        f => f.Name == fileName);

        //    return file;
        //}

        #endregion
    }
}
