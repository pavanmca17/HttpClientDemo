using HttpClientDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientDemo.Controllers
{
   
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/")]
    [Route("api/v{version:apiVersion}/")]    
    public class HttpDemoController : ControllerBase
    {       
        private readonly ILogger<HttpDemoController> _logger;
        private readonly IHttpClientService _httpClientService;

        public HttpDemoController(ILogger<HttpDemoController> logger, IHttpClientService httpClientService)
        {            
            _logger = logger;
            _httpClientService = httpClientService;
        }

        [HttpGet]
        [Route("/todo")]
        public async Task<List<ToDo>> Get()
        {
            return await _httpClientService.GetToDoList();
        }
    }
}