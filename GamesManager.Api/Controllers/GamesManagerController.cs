using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesManager.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesManagerController : ControllerBase
    {
        [HttpGet]
        public async Task<LatestVersionInfo> Get()
        {
            return null;
        }
    }
}