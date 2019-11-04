using System;
using GamesManager.Common.Enums;

namespace GamesManager.Common.Events
{
    public class OperationStatusChangedEventArgs : EventArgs
    {
        public OperationState OperationState { get; }

        public GameState GameState { get; }

        public int ProgressPercentage { get; }

        public OperationStatusChangedEventArgs(OperationState operationState, GameState gameState, int progressPercentage)
        {
            OperationState = operationState;
            GameState = gameState;
            ProgressPercentage = progressPercentage;
        }
    }
}
