using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProFind.Lib.Global.Services
{
    public class WebAPIConnection
    {
        private static HttpClient client = new HttpClient();

        public static HttpClient GetConnection => client;

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:5210/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}