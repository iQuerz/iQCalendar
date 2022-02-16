using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Business
{
    public class iQException : Exception
    {
        public iQException(string message)
            : base(message)
        {
            // TODO: log the exception
        }
        public iQException()
            : base()
        {
            // TODO: log the exception
        }
    }
}
