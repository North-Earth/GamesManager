using System;
using System.Collections.Generic;
using System.Text;
using GamesManager.Launcher.Models.Enums;

namespace GamesManager.Launcher.Models.Events
{
    public class GameManagerStatusesChangedEventArgs : EventArgs
    {
        public readonly ProcessStatus Status;

        public readonly ProcessButtonStatus ButtonStatus;

        public readonly int ProgressPercentage;

        public GameManagerStatusesChangedEventArgs(ProcessStatus status, ProcessButtonStatus buttonStatus, int progressPercentage)
        {
            Status = status;
            ButtonStatus = buttonStatus;
            ProgressPercentage = progressPercentage;
        }
    }
}
