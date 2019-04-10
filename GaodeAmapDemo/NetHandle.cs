using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GaodeAmapDemo
{
    public class NetHandle
    {
        public static Tuple<HttpStatusCode, string> GetHtmlContent(string url,string referer = "")
        {
            Tuple<HttpStatusCode, string> htmlResult = new Tuple<HttpStatusCode, string>(HttpStatusCode.Gone, string.Empty);
            string content = string.Empty;
            try
            {
                var clientHandler = new HttpClientHandler();
                if (clientHandler.SupportsAutomaticDecompression)
                {
                    clientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                using (var httpClient = new HttpClient(clientHandler))
                {
                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                    httpClient.Timeout = TimeSpan.FromSeconds(25);
                    requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36");
                    if (!string.IsNullOrEmpty(referer)) requestMessage.Headers.Add("Referer", referer);
                    var response = httpClient.SendAsync(requestMessage).Result;
                    content = response.Content.ReadAsStringAsync().Result;
                    htmlResult = new Tuple<HttpStatusCode, string>(response.StatusCode, content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return htmlResult;
        }
    }
}
