using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    public class TestEndpoint : IEndpoint
    {
        public Callback GetHandler()
        {
            return Test;
        }

        public string GetURI()
        {
            return "test";
        }

        private string Test(Stream input)
        {
            return "Test";
        }
    }
}
