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

            while (true)
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
            byte[] output = new byte[0];
            try
            {
                Callback cb = router.GetCallback(ctx.Request.GetEndpoint(), new HttpMethod(ctx.Request.HttpMethod));
                output = Encoding.UTF8.GetBytes(cb(ctx.Request.InputStream, ctx.Request.QueryString));
                ctx.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (KeyNotFoundException)
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.NotFound;
                output = Encoding.UTF8.GetBytes("Wrong URL");
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Console.WriteLine(ex);
                output = Encoding.UTF8.GetBytes("Sorry but 500");
            }
            finally
            {
                ctx.Response.OutputStream.Write(output, 0, output.Length);
                ctx.Response.Close();
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
