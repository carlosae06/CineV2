

namespace FrontEnd.DTOs
{
   
   
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? value { get; set; }
    }



    public class ResponseError
    {
        public ResponseError(int StatusCode, string message)
        {
            this.Message = message;
            this.StatusCode = StatusCode;
        }




        public int StatusCode { get; set; }
        public string? Message { get; set; }

       





    }

}
