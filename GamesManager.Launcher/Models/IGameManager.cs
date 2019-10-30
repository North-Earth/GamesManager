using System.Threading;
using System.Threading.Tasks;
using GamesManager.Common.Enums;
using GamesManager.Launcher.Models.Enums;

namespace GamesManager.Launcher.Models
{
    public interface IGameManager
    {
        public GameName GameName { get; }

        public ProcessStatus ProcessStatus { get; }

        public ProcessButtonStatus ProcessButtonStatus { get; }

        public int DownloadProgressPercentage { get; }

        public Task StartupChecks();

        public Task StartProcess(CancellationToken token);

        public void CancelProcesses();
    }
}