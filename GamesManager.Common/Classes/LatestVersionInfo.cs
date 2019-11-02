using System;
using GamesManager.Common.Enums;

namespace GamesManager.Common.Classes
{
    public class VersionInfo
    {
        public GameName Id { get; set; }

        public string FileName { get; set; }

        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Uri Uri { get; set; }

        public long Size { get; set; }
    }
}
