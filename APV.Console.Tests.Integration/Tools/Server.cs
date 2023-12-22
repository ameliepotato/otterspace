using System;
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
        public static void StartListeningOnAndRespondWith(string url, string? path, string? responseData)
        {
            if (!HttpListener.IsSupported)
            {
                return;
            }

            bool responded = false;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();

            while (!responded)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                string? requestUrl = context.Request.Url?.AbsoluteUri;
                if (requestUrl.Contains(path))
                {
                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;
                    if (responseData != null)
                    {
                        // Construct a response.
                        string responseString = responseData;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        // Get a response stream and write the response to it.
                        response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        // You must close the output stream.
                        output.Close();
                        responded = true;
                    }
                }
            }
            listener.Stop();
        }
    }
}
