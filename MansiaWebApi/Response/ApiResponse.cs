namespace MansiaWebApi.Response
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public string StatusCode { get; set; }  
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }

    }
}
