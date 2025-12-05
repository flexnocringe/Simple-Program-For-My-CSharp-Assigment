using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Exceptions
{
    //Sukurtas savas išimties tipas (1t.)
    internal class InvalidTaskActionException : Exception
    {
        public InvalidTaskActionException()
        {
        }

        public InvalidTaskActionException(string message)
            : base(message)
        {
        }

        public InvalidTaskActionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
