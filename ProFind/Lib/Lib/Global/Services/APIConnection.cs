using System;
using System.Net;
using System.Net.Http;

namespace ProFind.Lib.Global.Services
{
    public class APIConnection
    {
        private static WebServiceClient _service;

        public static WebServiceClient GetConnection
        {
            get
            {
                if (_service == null)
                    Init();
                return _service;
            }
        }
        public static void Init()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("http://localhost:5073");

            _service = new WebServiceClient(client);
        }
    }
}
