using System;
using System.Collections.Generic;
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

        public HttpServer(string rootUrl, IRouter _router)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(rootUrl);
            router = _router;
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
                var context = await listener.GetContextAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Factory.StartNew(() => HandleConnection(context));
#pragma warning restore CS4014 // I regret doing so many things
            }
        }
        
        /// <summary>
        /// A routine for handling every incoming request. Makes use of <see cref="IRouter"/> and handles
        /// the possible exceptions
        /// </summary>
        /// <param name="ctx">The <see cref="HttpListenerContext"/> of the particular request</param>
        private void HandleConnection(HttpListenerContext ctx)
        {
            Stream outputStream = new MemoryStream(); 
            try
            {
                var handler = router.GetHandler(ctx.Request.GetEndpoint(), new HttpMethod(ctx.Request.HttpMethod));
                outputStream = handler(ctx.Request.InputStream, ctx.Request.QueryString);
                ctx.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (ArgumentException)
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                outputStream = new MemoryStream(Encoding.UTF8.GetBytes("Wrong parameters"));
            }
            catch (KeyNotFoundException)
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.NotFound;
                outputStream = new MemoryStream(Encoding.UTF8.GetBytes("Wrong URL or method"));
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Console.WriteLine(ex);
                outputStream = new MemoryStream(Encoding.UTF8.GetBytes("Sorry but 500"));
            }
            finally
            {
                outputStream.CopyTo(ctx.Response.OutputStream);
                ctx.Response.Close();
            }
            outputStream.Close();
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
