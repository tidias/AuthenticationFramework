using System;
using System.IO;
using System.Net;
using System.Text;

namespace AuthenticationFramework.Utils
{
    public static class HttpHelpers
    {
        public static string SendPostRequest(string endpoint, string postData, string contentType)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            var byteArray = Encoding.UTF8.GetBytes(postData);

            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = contentType;

            var data = httpWebRequest.GetRequestStream();

            data.Write(byteArray, 0, byteArray.Length);
            data.Close();

            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
