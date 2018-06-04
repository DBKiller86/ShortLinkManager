using ShortLinkManager.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Net.Http.Headers;

namespace ShortLinkManager.Web.Utilities
{
    public class WebAPIManager
    {
        static HttpClient client = new HttpClient();

        public async Task<ShortLinkResponse> GetAsync(string uri)
        {
            string streamread = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {

                streamread = reader.ReadToEnd();
            }

            ShortLinkResponse slr = JsonConvert.DeserializeObject<ShortLinkResponse>(streamread);

            return slr;
        }
        
    }
}