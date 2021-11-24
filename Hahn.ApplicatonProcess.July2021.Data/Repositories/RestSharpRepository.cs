using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;

namespace Hahn.ApplicatonProcess.July2021.Data.Repositories
{
    public class HttpRequestRepository : IHttpRequestRepository
    {
        public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken)
        {
            var response = await url.GetAsync(cancellationToken);

            if (response.StatusCode != (int)System.Net.HttpStatusCode.OK)
            {
                throw new Exception(string.Format("Expected: StatusCode 'OK'. Actual: {0}. Url: {1}. Response: {2}",
                    response.StatusCode,
                    url,
                    response));
            }
            return await response.GetJsonAsync<T>();
        }
    }
}
