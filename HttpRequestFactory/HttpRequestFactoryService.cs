using HttpClientDemo.Model;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace HttpRequestFactory
{
    public class HttpRequestFactoryService : IHttpRequestFactoryService
    {
        private readonly IOptions<HttpClientSettings> _httpClientSettings;
       
        public HttpRequestFactoryService(IOptions<HttpClientSettings> httpClientSettings)
        {
            _httpClientSettings = httpClientSettings;           
        }
        public HttpRequestMessage CreateHttpRequestMessage(string urlPath, string urlQuery)
        {
            var request = new HttpRequestMessage();
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            UriBuilder builder = new UriBuilder(_httpClientSettings.Value.BaseUrl);
            builder.Path = urlPath;
            builder.Query = urlQuery;           
            return request;
        }

    }
}
