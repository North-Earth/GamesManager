using System;
using System.Collections.Generic;
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

        Task<List<T>> GetAsync<T>(Uri requestUri) where T : class;

        #endregion
    }
}
