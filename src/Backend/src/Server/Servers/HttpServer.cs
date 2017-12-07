using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// A barebones implementation of an HTTP server. Uses HttpListener and handles connections asynchronously.
    /// </summary>
    public class HttpServer: IServer
    {
        private HttpListener listener;
        private IRouter router;

        public HttpServer(IRouter _router)
        {
            var rootUrlConfig = ConfigurationManager.AppSettings["serverURL"];

            if (rootUrlConfig != null)
            {
                listener = new HttpListener();
                listener.Prefixes.Add(rootUrlConfig);
                router = _router;
            }
            else
            {
                throw new ConfigurationErrorsException("No server host URL configured.");
            }
        }

        /// <summary>
        /// Starts the server and begins to listen.
        /// Is non-blocking.
        /// </summary>
        public async void Start()
        {
            listener.Start();

            while (listener.IsListening)
            {
                try
                {
                    var context = await listener.GetContextAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Factory.StartNew(() => HandleConnection(context));
#pragma warning restore CS4014 // I regret doing so many things
                }
                catch (ObjectDisposedException)
                {
                    return;
                }
                catch (HttpListenerException)
                {
                    return;
                }
            }
        }
        
        /// <summary>
        /// A routine for handling every incoming request. Makes use of <see cref="IRouter"/> and handles
        /// the possible exceptions
        /// </summary>
        /// <param name="ctx">The <see cref="HttpListenerContext"/> of the particular request</param>
        private void HandleConnection(HttpListenerContext ctx)
        {
            //All the exception handling happenning in one function
            //The arrow function contains both retrieving the handler and invoking it
            MapExceptions(
                () => {
                    return router.GetHandler(
                        ctx.Request.GetEndpoint(),
                        new HttpMethod(ctx.Request.HttpMethod)
                    )
                    (
                        ctx.Request.InputStream,
                        ctx.Request.QueryString
                    );
                }, 
                out int statusCode, 
                out Stream outputStream
            );

            ctx.Response.StatusCode = statusCode;

            if (outputStream != null)
            {
                outputStream.CopyTo(ctx.Response.OutputStream);
                outputStream.Close();
            }

            ctx.Response.Close();
        }

        /// <summary>
        /// Contains the responses for every sort of exception case expected to possibly occur, and assigns them 
        /// appropriately upon catching.
        /// </summary>
        /// <param name="handler">The arrow function, that calls the internal handling method of the endpoint</param>
        /// <param name="statusCode">The out'd <see cref="int"/> to pass the appropriate status code to the caller</param>
        /// <param name="outputStream">The out'd <see cref="Stream"/> to pass the appropriate response body to the caller</param>
        private void MapExceptions(Func<Stream> handler, out int statusCode, out Stream outputStream)
        {
            try
            {
                statusCode = (int)HttpStatusCode.OK;
                outputStream = handler();
            }
            catch (ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                outputStream = new MemoryStream(Encoding.UTF8.GetBytes("Wrong parameters"));
            }
            catch (KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                outputStream = new MemoryStream(Encoding.UTF8.GetBytes("Wrong URL or method"));
            }
            catch (JsonReaderException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                outputStream = new MemoryStream(Encoding.UTF8.GetBytes("Format error"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                statusCode = (int)HttpStatusCode.InternalServerError;
                outputStream = new MemoryStream(Encoding.UTF8.GetBytes("Sorry but 500"));
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            listener.Close();
        }
    }
}
