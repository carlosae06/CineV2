namespace FrontEnd.Repositorio
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T response, bool error, HttpResponseMessage  httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
            
        }

        public bool Error { get; set; } 
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public T Response { get; set; }

        public async Task<string> GetBody()
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }

        public async Task<E?> GetBody<E>()
        {            
            var body = await HttpResponseMessage.Content.ReadAsStringAsync();
            //vamos a serializar de json a un objeto
            return  Newtonsoft.Json.JsonConvert.DeserializeObject<E>(body);

        }



    }
}
