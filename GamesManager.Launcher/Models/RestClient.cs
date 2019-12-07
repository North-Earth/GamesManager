using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GamesManager.Launcher.Models
{
    public class RestClient : IRestClient
    {
        #region Fields

        #endregion

        #region Constructors

        public RestClient() { }

        #endregion

        #region Methods

        public async Task<T> GetAsync<T>(Uri requestUri, CancellationToken token) where T : class
        {
            try
            {
                T result = default;

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(hours: 0, minutes: 0, seconds: 10);
                    var json = await client.GetStringAsync(requestUri).ConfigureAwait(true);

                    result = JsonConvert.DeserializeObject<T>(json);
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
