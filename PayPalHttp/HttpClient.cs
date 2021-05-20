using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayPalHttp
{
    public class HttpClient
    {
        public Encoder Encoder { get => _encoder; }
        private Encoder _encoder;
        protected Environment _environment;
        private System.Net.Http.HttpClient _httpClient;
        private List<IInjector> _injectors;

        public HttpClient(System.Net.Http.HttpClient httpClient, Environment environment) {

            _encoder = new Encoder();
            _environment = environment;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(environment.BaseUrl());
            _httpClient.DefaultRequestHeaders.Add("User-Agent", GetUserAgent());
            _injectors = new List<IInjector>();
        }

        protected virtual string GetUserAgent() {
            return "PayPalHttp-Dotnet HTTP/1.1";
        }

        public void AddInjector(IInjector injector) {
            if (injector != null) {
                _injectors.Add(injector);
            }
        }

        public void SetConnectTimeout(TimeSpan timeout) {
            _httpClient.Timeout = timeout;
        }

        public virtual async Task<HttpResponse> Execute<T>(T req) where T: HttpRequest  {
            var request = req.Clone<T>();

            foreach (var injector in _injectors) {
                injector.Inject(request);
            }

            request.RequestUri = new Uri(_environment.BaseUrl() + request.Path);

            if (request.Body != null) {
                request.Content = Encoder.SerializeRequest(request);
            }

			var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode) {
                object responseBody = null;
                if (response.Content.Headers.ContentType != null) {
                    responseBody = Encoder.DeserializeResponse(response.Content, request.ResponseType);
                }
                return new HttpResponse(response.Headers, response.StatusCode, responseBody);
            }
            else
            {
				var responseBody = await response.Content.ReadAsStringAsync();
				throw new HttpException(response.StatusCode, response.Headers, responseBody);
            }
        }
    }
}
