using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace Business.Util
{
    public static class WebRequestor
    {
        private static HttpClient client;

        public static void Init()
        {
            string proxyUri = "203.0.172.4:8181";
            NetworkCredential proxyCred = new NetworkCredential(@"trainer", @"trainer", @"HDWA");
            WebProxy proxy = new WebProxy(proxyUri, false)
            {
                UseDefaultCredentials = false,
                Credentials = proxyCred
            };

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false
            };

            client = new HttpClient(httpClientHandler);
        }

        public static string Post(string url)
        {
            var response = client.PostAsync(url, null);

            var responseContent = response.Result.Content.ReadAsStringAsync();

            return responseContent.Result;
        }

        public static string Get(string url)
        {
            return client.GetStringAsync(url).Result;
        }
    }
}
