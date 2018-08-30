using System.Net;
namespace Assets.Libs.Authsome
{
    public class HttpResponseWrapper<T>
    {
        public T Content { get; set; }
        public HttpStatusCode httpStatusCode { get; set; }
        public string ErrorJson { get; set; }
    }
}
