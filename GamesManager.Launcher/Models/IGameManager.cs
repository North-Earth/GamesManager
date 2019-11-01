using System.Threading;
using System.Threading.Tasks;
using GamesManager.Common.Enums
using GamesManager.Launcher.Models.Events;

namespace GamesManager.Launcher.Models
{
    public interface IGameManager
    {
        public GameName GameName { get; }

        public delegate void StatusesChangedHandler(GameManagerStatusesChangedEventArgs eventArgs);

        public event StatusesChangedHandler StatusesChanged;

        public Task StartupChecks();

        public Task StartProcess(CancellationToken token);

        public void CancelProcesses();
    }
}