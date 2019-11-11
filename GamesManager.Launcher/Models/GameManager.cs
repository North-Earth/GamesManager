using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;
using GamesManager.Common.Events;
using GamesManager.Launcher.Models.Enums;
using GamesManager.Launcher.Models.Events;
using GamesManager.Net;
using Newtonsoft.Json;

namespace GamesManager.Launcher.Models
{
    public class GameManager : IGameManager
    {
        #region Fields

        private const string CachePath = "cache";
        private const string AppsPath = "apps";

        private Uri VersionUri { get; }

        public event IGameManager.StatusChangedHandler StatusChangedEvent;

        private IWebClient WebClient { get; }
        private IRestClient RestClient { get; }

        private CancellationTokenSource TokenSource;

        private OperationState OperationState { get; set; }

        private GameState GameState { get; set; }

        public string Name { get; }

        public GameName GameName { get; }

        #endregion

        #region Constructors

        public GameManager(GameName gameName)
        {
            GameName = gameName;

            Name = ConvertEnumToName(GameName);

            //TODO: Add Dependency Injection.
            WebClient = new Net.WebClient();
            RestClient = new Net.RestClient();
            VersionUri = new Uri(@$"https://localhost:5001/gamemanager/{GameName}"); //TODO: Using resourse or config.
        }

        #endregion

        #region Methods

        public void CheckСondition()
        {
            GameState gameState;

            if (IsInstalled())
            {
                gameState = IsUpdated() ? GameState.Play : GameState.Update;
            }
            else
            {
                gameState = GameState.Install;
            }

            UpdateStatus(OperationState.Completed, gameState);
        }

        public void StartProcess()
        {
            if (TokenSource == null)
            {
                TokenSource = new CancellationTokenSource();
            }

            switch (GameState)
            {
                case GameState.Play:
                    Task.Run(async () => await Play(TokenSource.Token).ConfigureAwait(true));
                    break;
                case GameState.Install:
                    Task.Run(async () => await Install(TokenSource.Token).ConfigureAwait(true));
                    break;
                case GameState.Update:
                    Task.Run(async () => await Update(TokenSource.Token).ConfigureAwait(true));
                    break;
                case GameState.Cancel:
                    Task.Run(() => Cancel());
                    break;
                default:
                    break;
            }
        }

        private async Task Play(CancellationToken token) => throw new NotImplementedException();

        private async Task Install(CancellationToken token)
        {
            try
            {
                UpdateStatus(OperationState.Downloading, GameState.Cancel);

                var latestVersion = await GetUpdate(token).ConfigureAwait(true);

                WebClient.DownloadProgressChangedEventHandler += (s, e) => { throw new NotImplementedException(); };

                await WebClient.DownloadFileAsync(latestVersion.Uri, latestVersion.FileName, token).ConfigureAwait(true);

                UpdateStatus(OperationState.Completed, GameState.Play);

            }
            catch (OperationCanceledException)
            {
                TokenSource.Dispose();
                TokenSource = null;

                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        private async Task Update(CancellationToken token) => throw new NotImplementedException();

        private async Task<VersionInfo> GetUpdate(CancellationToken token) 
            => await RestClient.GetAsync<VersionInfo>(VersionUri, token).ConfigureAwait(true);

        private void Cancel() => throw new NotImplementedException();

        private void UpdateStatus(OperationState operationState, GameState gameState)
        {
            OperationState = operationState;
            GameState = gameState;

            StatusChangedEvent?.Invoke(new OperationStatusChangedEventArgs(OperationState, GameState));
        }

        private void Check() => throw new NotImplementedException();

        private bool IsInstalled()
        {
            return false;
        }

        private bool IsUpdated()
        {
            return false;
        }

        #endregion


        private string ConvertEnumToName(GameName gameName) => gameName.ToString().Replace('_', ' ');
    }
}
