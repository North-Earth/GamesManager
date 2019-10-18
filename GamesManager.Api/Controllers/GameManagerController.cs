using System.Threading.Tasks;
using GamesManager.Common;
using Microsoft.AspNetCore.Mvc;

namespace GamesManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameManagerController : ControllerBase
    {
        #region Fields

        private readonly IGameManager _gamesManager;

        #endregion

        #region Constructors

        public GameManagerController(IGameManager gamesManager) 
            => _gamesManager = gamesManager;

        #endregion

        #region Fields

#warning Added GamePlatform parametr and remove hardcode.
        [HttpGet("{name}", Name = "Get")]
        public async Task<LatestVersionInfo> Get(GameName name) 
            => await _gamesManager.GetLatestVersionAsync(name, GamePlatform.win86).ConfigureAwait(false);

        #endregion
    }
}