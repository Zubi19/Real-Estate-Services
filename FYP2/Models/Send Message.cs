using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace FYP2.Models
{
    public class Send_Message
    {
        private const string MessagesUrlPath = "services/api/messaging";
        static string txtIPAddress = "10.159.128.212";
        static string txtPort = "1688";

        public static string Message;
        protected static string ConstructBaseUri()
        {
            UriBuilder uriBuilder = new UriBuilder("http", txtIPAddress, Convert.ToInt32(txtPort));
            return uriBuilder.ToString();
        }

        public static async Task SendMessage(string custNum)
        {
            using (var client = new HttpClient())
            {
                string url = ConstructBaseUri();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("to", custNum));
                postData.Add(new KeyValuePair<string, string>("message", Message + "\n" + "From: " + GlobalVariables.tel));
                HttpContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(MessagesUrlPath, content);

            }

        }

    }
}