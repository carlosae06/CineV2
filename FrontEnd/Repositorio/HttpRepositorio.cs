using Microsoft.JSInterop;
using System.Text;
using FrontEnd.Helpers;
using System.Net.Http.Headers;

namespace FrontEnd.Repositorio
{
    public class HttpRepositorio : IRepositorio
    { 
        private readonly HttpClient httpClient;
        private readonly IJSRuntime js;
        public HttpRepositorio(HttpClient httpClient, IJSRuntime js)
        {
            this.httpClient = httpClient;
            this.js = js;
        }
        private async Task AsignarToken()
        {
            string token = await js.GetFromLocalStorage("TOKENKEY");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            await AsignarToken();
            var responseHttp = await httpClient.GetAsync(url);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializeObject<T>(responseHttp);
                return new HttpResponseWrapper<T>(response!, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<T>(default!, true, responseHttp);
            }
            

        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            await AsignarToken();
            var enviarJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(responseHttp, false, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data)
        {
            await AsignarToken();
            var enviarJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializeObject<TResponse>(responseHttp);
                return new HttpResponseWrapper<TResponse>(response!, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default!, true, responseHttp);
            }

        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T data)
        {
            await AsignarToken();
            var enviarJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(responseHttp, false, responseHttp);
        }
        public async Task<HttpResponseWrapper<object>> Delete<T>(string url)
        {
            var responseHttp = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(responseHttp, false, responseHttp);
        }


        public async Task<T?> DeserializeObject<T>(HttpResponseMessage httpResponse)
        {
            var body = await httpResponse.Content.ReadAsStringAsync();
            //vamos a serializar de json a un objeto
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(body);

        }

    }
}
