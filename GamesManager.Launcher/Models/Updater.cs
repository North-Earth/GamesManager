using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Enums;
using Newtonsoft.Json;

namespace GamesManager.Launcher.Models
{
    public class Updater : IUpdater
    {
        #region Fields

        private const string CACHE_PATH = "cache";
        private ProcessStatus status;

        public int CompletionPercent { get; private set; }

        public ProcessStatus Status 
        { 
            get => status; 
            private set => status = value; 
        }

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public async Task<LatestVersionInfo> GetLatestVersionInfo(GameName gameName)
        {
            LatestVersionInfo latestVersionInfo = default;

            using (var client = new HttpClient())
            {
                var uri = new Uri(@$"https://localhost:5001/gamemanager/{gameName}");
                var releaseJson = await client.GetStringAsync(uri);

                latestVersionInfo = JsonConvert.DeserializeObject<LatestVersionInfo>(releaseJson);
            }

            return latestVersionInfo;
        }

        public async Task DownloadLatestVersion(LatestVersionInfo latestVersion)
        {
            Status = ProcessStatus.Checking;

            if (!IsValidCache(latestVersion))
            {
                try
                {
                    Status = ProcessStatus.Downloading;
                    await DownloadFile(latestVersion);
                    Status = ProcessStatus.Complete;
                }
                catch (Exception)
                {
                    Status = ProcessStatus.Error;
                    throw;
                }
            }
        }

        public async Task InstallLatestVersion(GameName gameName)
        {
            throw new NotImplementedException();
        }

        private bool IsValidCache(LatestVersionInfo latestVersion)
        {
            Debug.WriteLine("Validation Cache...");

            if (!Directory.Exists(CACHE_PATH))
            {
                Directory.CreateDirectory(CACHE_PATH);
            }

            var file = GetFile(CACHE_PATH, latestVersion.FileName);

            if (file == null)
            {
                return false;
            }
            else
            {
                if (file.Exists)
                {
                    if (file.Length == latestVersion.Size)
                    {
                        return true;
                    }

                    DeleteFile(file);
                }
            }

            return false;
        }

        private async Task DownloadFile(LatestVersionInfo latestVersion)
        {
            Debug.WriteLine("Download File");
            try
            {
                var webClient = new WebClient();

                var downloadFileTask = await Task.Factory.StartNew(async () 
                    => await webClient.DownloadFileTaskAsync(latestVersion.Uri,
                        Path.Combine(CACHE_PATH, latestVersion.FileName)));

                var file = GetFile(CACHE_PATH, latestVersion.FileName);

                while (file.Length != latestVersion.Size)
                {
                    file = GetFile(CACHE_PATH, latestVersion.FileName);

                    if (file != null)
                    {
                        file.Refresh();

                        if (file.Length > 0)
                        {
                            CompletionPercent = (int)((double)file.Length / (double)latestVersion.Size * 100);
                        }

                        Debug.WriteLine($"{CompletionPercent} {file.Length} / {file.Length}");
                    }
                }

                downloadFileTask.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private FileInfo GetFile(string directoryPath, string fileName)
        {
            var directory = new DirectoryInfo(directoryPath);
            var file = Array.Find(directory.GetFiles(),
                f => f.Name == fileName);

            return file;
        }

        private void DeleteFile(FileInfo file)
        {
            file.Delete();
        }

        #endregion
    }
}
