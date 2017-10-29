using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// Implemented by the classes that aim to encapsulate the functionality of the server.
    /// The responsibilities of such classes include setting up the listeners, manage handling of the connections
    /// and handle the exceptions appropriately
    /// </summary>
    public interface IServer: IDisposable
    {
        /// <summary>
        /// Start the server
        /// </summary>
        void Start();
    }
}
