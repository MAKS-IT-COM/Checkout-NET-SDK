using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PayPalHttp {
    public class FormEncodedSerializer: ISerializer {
        public string GetContentTypeRegexPattern() {
            return "application/x-www-form-urlencoded";
        }

        public object Decode(HttpContent content, Type responseType) {
            throw new IOException($"Unable to deserialize Content-Type: {GetContentTypeRegexPattern()}.");
        }

        public HttpContent Encode(HttpRequest request) {
            if (!(request.Body is IDictionary)) {
                throw new IOException($"Request requestBody must be Map<string, string> when Content-Type is: {GetContentTypeRegexPattern()}");
            }

            return new FormUrlEncodedContent((Dictionary<string, string>)request.Body);
        }
    }
}
