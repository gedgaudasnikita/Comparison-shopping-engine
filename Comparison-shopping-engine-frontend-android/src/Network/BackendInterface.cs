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
    public class BackendInterface
    {
        private Client cli;

        public BackendInterface()
        {
            //TODO: Figure out a way to export this into configuration
            cli = new Client("http://10.0.2.2:4444");
        }

        public async Task<List<string>> GetSuggestions(String input, int limit)
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

        public async Task<Receipt> ProcessImage(string input)
        {
            var endpoint = new ProcessImageEndpoint();
            var body = endpoint.GetRequestBody(input);

            var response = await cli.Request<GetSuggestionsEndpoint>(body: body);
            var result = endpoint.GetResponseBody(response);
            return result;
        }

        public async Task<List<Item>> ProcessReceipt(Receipt input)
        {
            var endpoint = new ProcessReceiptEndpoint();
            var body = endpoint.GetRequestBody(input);

            var response = await cli.Request<ProcessReceiptEndpoint>(body: body);
            var result = endpoint.GetResponseBody(response);
            return result;
        }

        public async void SaveReceipt(Receipt input)
        {
            var endpoint = new SaveReceiptEndpoint();
            var body = endpoint.GetRequestBody(input);

            await cli.Request<SaveReceiptEndpoint>(body: body);
        }
    }
}