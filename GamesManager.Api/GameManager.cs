using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using GamesManager.Api.Models;
using GamesManager.Common;
using Microsoft.Extensions.Configuration;

namespace GamesManager.Api
{
    public class GameManager : IGameManager
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Controllers

        public GameManager(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        #endregion

        #region Methods

        public async Task<LatestVersionInfo> GetLatestVersionAsync(GameName gameName, GamePlatform gamePlatform)
        {
            var release = await GetReleaseAsync(gameName).ConfigureAwait(false);
            var uri = release.assets.Where(a => a.name.Contains(value: gamePlatform.ToString(), StringComparison.Ordinal))
                .SingleOrDefault()?.browser_download_url;

            return new LatestVersionInfo()
            {
                Id = gameName,
                ReleaseDate = DateTime.Parse(release.published_at, new CultureInfo("en-US", false)),
                Version = release.tag_name,
                Uri = uri
            };
        }

        private async Task<Release> GetReleaseAsync(GameName name)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Release>));

            var mediaType = new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json");
            var requestUri = _configuration.GetValue<Uri>($"ReleasesUri:{name}");

            List<Release> releases = default;


            using (var client = _clientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(mediaType);
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                Task<System.IO.Stream> taskStream = client.GetStreamAsync(requestUri);
                var releaseStream = await client.GetStreamAsync(requestUri).ConfigureAwait(false);

                releases = serializer.ReadObject(releaseStream) as List<Release>;
            }

            return releases.Where(r => r.id == releases.Max(r => r.id)).SingleOrDefault();
        }

        #endregion
    }
}
