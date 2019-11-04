using System;
using System.Collections.Generic;
using System.Text;

namespace GamesManager.Common.Enums
{
    public enum OperationState
    {
        Canceled = 0,
        Checking = 1,
        Downloading = 2,
        Installing = 3,
        Completed = 4,
        Playing = 5
    }
}
