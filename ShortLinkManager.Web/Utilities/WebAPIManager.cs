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
        
        public async Task<ShortLinkResponse> PostAsync(string uri, string data, string contentType, string method = "POST")
        {
            
            var buffer = System.Text.Encoding.UTF8.GetBytes(data);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            using (var client = new HttpClient())
            {
                var jsonString = await client.PostAsync(uri, byteContent).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<ShortLinkResponse>(jsonString.Content.ToString());
            }

            //OLD
            //byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            //string streamread = string.Empty;

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //request.ContentLength = dataBytes.Length;
            //request.ContentType = contentType;
            //request.Method = method;

            //using (Stream requestBody = request.GetRequestStream())
            //{
            //    await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            //}

            //using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    streamread = await reader.ReadToEndAsync();
            //    ShortLinkResponse slr = JsonConvert.DeserializeObject<ShortLinkResponse>(streamread);

            //    return slr;
            //}
            
        }
    }
}