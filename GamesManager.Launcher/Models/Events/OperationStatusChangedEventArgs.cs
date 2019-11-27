using System;
using GamesManager.Launcher.Models.Enums;

namespace GamesManager.Launcher.Models.Events
{
    public delegate void OperationStatusChangedEventHandler(object sender, OperationStatusChangedEventArgs args);

    public class OperationStatusChangedEventArgs : EventArgs
    {
        public OperationState OperationState { get; }

        public PlayButtonState GameState { get; }

        public int ProgressPercentage { get; }

        public OperationStatusChangedEventArgs(OperationState operationState, PlayButtonState playButtonState, int progressPercentage = 0)
        {
            OperationState = operationState;
            GameState = playButtonState;
            ProgressPercentage = progressPercentage;
        }
    }
}
