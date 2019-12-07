using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager.Launcher.Models.Enums
{
    public enum OperationState
    {
        Completed = 1,
        Playing = 2,
        Downloading = 3,
        Installing = 4,
        Canceling = 5,
        Checking = 6
    }
}
