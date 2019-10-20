using System.Threading.Tasks;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Enums;

namespace GamesManager.Launcher.Models
{
    public interface IUpdater
    {
        public int CompletionPercent { get; }

        public ProcessStatus Status { get; }

        public Task<LatestVersionInfo> GetLatestVersionInfo(GameName gameName);

        public Task DownloadLatestVersion(LatestVersionInfo latestVersion);

        public Task InstallLatestVersion(GameName gameName);
    }
}
