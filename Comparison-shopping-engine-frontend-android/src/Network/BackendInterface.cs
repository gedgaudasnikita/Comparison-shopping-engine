using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.IO;
using Comparison_shopping_engine_web_protocol;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_frontend_android
{
    /// <summary>
    /// This class provides the easy-to-use methods for calling the backend endpoints.
    /// </summary>
    public class BackendInterface
    {
        private Client cli;

        public BackendInterface()
        {
            cli = new Client(Configuration.backendUrl);
        }

        /// <summary>
        /// Calls the backend's GetSuggestions endpoint
        /// </summary>
        /// <param name="input">The <see cref="string"/> to be matched</param>
        /// <param name="limit">The maximum amount of similar items</param>
        /// <returns>A <see cref="List{string}"/> with all the matches order by similarity</returns>
        public async Task<List<string>> GetSuggestions(string input, int limit)
        {
            var endpoint = new GetSuggestionsEndpoint();
            var query = endpoint.GetRequestParameters(new GetSuggestionsParameters
            {
                Input = input,
                Limit = limit
            });

            var response = await cli.Request<GetSuggestionsEndpoint>(query: query);
            var result = endpoint.GetResponseBody(response);
            return result;
        }

        /// <summary>
        /// Calls the backend's ProcessImage endpoint
        /// </summary>
        /// <param name="input">The <see cref="string"/>, containing the result of OCR</param>
        /// <returns>A <see cref="Receipt"/> object with the parsed data</returns>
        public async Task<Receipt> ProcessImage(string input)
        {
            var endpoint = new ProcessImageEndpoint();
            var body = endpoint.GetRequestBody(input);

            var response = await cli.Request<GetSuggestionsEndpoint>(body: body);
            var result = endpoint.GetResponseBody(response);
            return result;
        }

        /// <summary>
        /// Calls the backend's ProcessReceipt endpoint
        /// </summary>
        /// <param name="input">The <see cref="Receipt"/>, that has been confirmed by the user</param>
        /// <returns>The <see cref="List{Item}"/> with the cheapest items found</returns>
        public async Task<List<Item>> ProcessReceipt(Receipt input)
        {
            var endpoint = new ProcessReceiptEndpoint();
            var body = endpoint.GetRequestBody(input);

            var response = await cli.Request<ProcessReceiptEndpoint>(body: body);
            var result = endpoint.GetResponseBody(response);
            return result;
        }

        /// <summary>
        /// Calls the backend's SaveReceipt endpoint
        /// </summary>
        /// <param name="input">The <see cref="Receipt"/> to be saved</param>
        public async void SaveReceipt(Receipt input)
        {
            var endpoint = new SaveReceiptEndpoint();
            var body = endpoint.GetRequestBody(input);

            await cli.Request<SaveReceiptEndpoint>(body: body);
        }
    }
}