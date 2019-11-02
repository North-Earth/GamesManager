using System;
using System.Collections.Generic;
using System.Text;
using GamesManager.Launcher.Models.Enums;

namespace GamesManager.Launcher.Models
{
    public class Statuses
    {
        public ProcessStatus ProcessStatus { get; set; }

        public ProcessButtonStatus ProcessButtonStatus { get; set; }

        public int DownloadProgressPercentage { get; set; }
    }
}
