using System;
using System.Threading;
using System.Threading.Tasks;

namespace GamesManager.Launcher.Models
{
    public interface IRestClient
    {
        #region Fields

        #endregion

        #region Methods

        Task<T> GetAsync<T>(Uri requestUri, CancellationToken token) where T : class;

        #endregion
    }
}
