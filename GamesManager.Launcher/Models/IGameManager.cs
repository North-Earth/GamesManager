using System.Threading;
using System.Threading.Tasks;
using GamesManager.Common.Enums;
using GamesManager.Common.Events;
using GamesManager.Launcher.Models.Events;

namespace GamesManager.Launcher.Models
{
    public interface IGameManager
    {
        #region Fields

        public string Name { get; }

        public GameName GameName { get; }

        public delegate void StatusChangedHandler(OperationStatusChangedEventArgs eventArgs);

        public event StatusChangedHandler StatusChangedEvent;

        #endregion

        #region Methods

        public void CheckСondition();

        public void StartProcess();

        #endregion


    }
}