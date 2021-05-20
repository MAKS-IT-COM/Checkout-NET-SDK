using System;
using System.Net.Http;

namespace PayPalHttp {
	public interface ISerializer {
        string GetContentTypeRegexPattern();
        HttpContent Encode(HttpRequest request);
        object Decode(HttpContent content, Type responseType);
    }
}
