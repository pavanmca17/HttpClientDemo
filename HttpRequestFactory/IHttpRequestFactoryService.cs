

namespace HttpRequestFactory
{
    public interface IHttpRequestFactoryService
    {
        public HttpRequestMessage CreateHttpRequestMessage(string urlPath, String urlQuery);
       
    }
}
