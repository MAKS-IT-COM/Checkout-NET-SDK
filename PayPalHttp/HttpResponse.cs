using System.Net;
using System.Net.Http.Headers;

namespace PayPalHttp {
    public class HttpResponse {
        public HttpHeaders Headers { get => _headers; }
        private HttpHeaders _headers;

    	public HttpStatusCode StatusCode { get => _statusCode; }
        private HttpStatusCode _statusCode;

        private object _result;

        public HttpResponse(HttpHeaders headers, HttpStatusCode statusCode, object result) {
            _headers = headers;
            _statusCode = statusCode;
            _result = result;
        }

        public T Result<T>() {
            return (T)_result;
        }
    }
}
