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
using Newtonsoft.Json;

namespace GamesManager.Launcher.Models
{
    public class GameManager : IGameManager
    {
        #region Fields

        private const string CachePath = "cache";
        private const string AppsPath = "apps";

        public event IGameManager.StatusChangedHandler StatusChangedEvent;

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
        }

        #endregion

        #region Methods

        public void CheckСondition()
        {
            if (IsInstalled())
            {
                GameState = IsUpdated() ? GameState.Play : GameState.Update;
            }
            else
            {
                GameState = GameState.Install;
            }

            OperationState = OperationState.Completed;
            UpdateStatus();
        }

        public void StartProcess()
        {
            switch (GameState)
            {
                case GameState.Play:
                    Task.Run(async () => await Play().ConfigureAwait(true));
                    break;
                case GameState.Install:
                    Task.Run(async () => await Install().ConfigureAwait(true));
                    break;
                case GameState.Update:
                    Task.Run(async () => await Update().ConfigureAwait(true));
                    break;
                case GameState.Cancel:
                    Task.Run(() => Cancel());
                    break;
                default:
                    break;
            }
        }

        private Task Play() => throw new NotImplementedException();

        private Task Install() => throw new NotImplementedException();

        private Task Update() => throw new NotImplementedException();

        private void Cancel() => throw new NotImplementedException();

        private void UpdateStatus()
        {
            StatusChangedEvent?.Invoke(new OperationStatusChangedEventArgs(OperationState, GameState));
        }

        private void Check() => throw new NotImplementedException();

        private bool IsInstalled()
        {
            return true;
        }

        private bool IsUpdated()
        {
            return true;
        }

        #endregion


        private string ConvertEnumToName(GameName gameName) => gameName.ToString().Replace('_', ' ');
    }
}
