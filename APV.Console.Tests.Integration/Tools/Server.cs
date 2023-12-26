﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace APV.Console.Tests.Integration.Tools
{
    internal class Server
    {
        private Dictionary<string, string> _responseData = new Dictionary<string, string>();
        private bool Started = false;
       
        public void AddToResponseData(string path, string responsData)
        {
            if (_responseData.ContainsKey(path))
            {
                _responseData[path] = responsData;
            }
            else
            {
                _responseData.Add(path, responsData);
            }
        }

        public void StartListening(string url)
        {
            if (!HttpListener.IsSupported)
            {
                return;
            }

            foreach (var path in _responseData)
            {

                HttpListener listener = new HttpListener();
                listener.Prefixes.Add(url);
                listener.Start();

                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                string? requestUrl = context.Request.Url?.AbsoluteUri;
                HttpListenerResponse response = context.Response;
                if (requestUrl?.Contains(path.Key) == true)
                {
                    // Construct a response.
                    string responseString = path.Value;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
                listener.Stop();
            }
        }
    }
}
