using System.Threading.Tasks;
using GamesManager.Common;

namespace GamesManager.Api
{
    public interface IGameManager
    {
        public Task<LatestVersionInfo> GetLatestVersionAsync(GameName name, GamePlatform gamePlatform);
    }
}