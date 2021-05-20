using System.IO;
using System.Net;
using System.Net.Http.Headers;
namespace PayPalHttp
{
    public class HttpException: IOException
    {
        public HttpStatusCode StatusCode { get => _statusCode; }
		public HttpHeaders Headers { get => _headers; }
        private HttpStatusCode _statusCode;
        private HttpHeaders _headers;

        public HttpException(HttpStatusCode statusCode, HttpHeaders headers, string message): base(message) {
            _statusCode = statusCode;
            _headers = headers;
    	}
    }
}
