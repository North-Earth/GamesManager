using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesManager.Net
{
    public interface IRestClient
    {
        #region Fields

        #endregion

        #region Methods

        public Task<T> GetAsync<T>(Uri requestUri, CancellationToken token) where T : class;

        #endregion
    }
}
