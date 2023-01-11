namespace FrontEnd.Repositorio
{
    public interface IRepositorio
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url); 
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T data);
        Task<HttpResponseWrapper<object>> Delete<T>(string url);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);
    }
}
