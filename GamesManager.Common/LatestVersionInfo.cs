using System;
using System.Collections.Generic;
using System.Text;

namespace GamesManager.Common
{
    public class LatestVersionInfo
    {
        public GameName Id { get; set; }

        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Uri { get; set; }
    }
}
