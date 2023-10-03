using HttpClientDemo.Model;

namespace HttpClientDemo
{
    public interface IHttpClientService
    {
        Task<List<ToDo>> GetToDoList();
    }
}