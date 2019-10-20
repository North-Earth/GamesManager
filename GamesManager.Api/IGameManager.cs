using System.Threading.Tasks;
using GamesManager.Common.Classes;
using GamesManager.Common.Enums;

namespace GamesManager.Api
{
    public interface IGameManager
    {
        public Task<LatestVersionInfo> GetLatestVersionAsync(GameName name, GamePlatform gamePlatform);
    }
}