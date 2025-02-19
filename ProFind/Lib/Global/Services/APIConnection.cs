﻿using System;
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
            //bool useLocalhost = true;

            //if (useLocalhost)
            //{
            //var handler = new HttpClientHandler();
            //handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            //handler.ServerCertificateCustomValidationCallback =
            //    (httpRequestMessage, cert, cetChain, policyErrors) => true;


            //var client = new HttpClient();
            //client.BaseAddress = new Uri("https://127.0.0.1:7073");

            //_service = new WebServiceClient(client);
            //}
            //else
            //{
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.profind.work");

            _service = new WebServiceClient(client);
            //}



        }
    }
}
