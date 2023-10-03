using HttpClientDemo.Model;
using HttpRequestFactory;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace HttpClientDemo
{
    public class HttpClientService : IHttpClientService
    {        
        private readonly ILogger<HttpClientService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpRequestFactoryService _httpRequestFactoryService;

        public HttpClientService(IHttpClientFactory httpClientFactory, IHttpRequestFactoryService httpRequestFactoryService, ILogger<HttpClientService> logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpRequestFactoryService = httpRequestFactoryService;
        }

        public async Task<List<ToDo>> GetToDoList()
        {
            var toDos = new List<ToDo>();
           
            var httpclient = _httpClientFactory.CreateClient(HttpClientConstants.httpclientname);         
            var request = _httpRequestFactoryService.CreateHttpRequestMessage(urlPath: "/todos", "");

            using var httpResponseMessage = await httpclient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (httpResponseMessage.IsSuccessStatusCode)      
            {              
                if (httpResponseMessage.Content is object && httpResponseMessage?.Content?.Headers?.ContentType?.MediaType == "application/json")
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    using var streamReader = new StreamReader(contentStream);

                    using var jsonReader = new JsonTextReader(streamReader);

                    JsonSerializer serializer = new JsonSerializer();

                    toDos = serializer.Deserialize<List<ToDo>>(jsonReader);

                }
            }

           return toDos;

        }
    }

}
