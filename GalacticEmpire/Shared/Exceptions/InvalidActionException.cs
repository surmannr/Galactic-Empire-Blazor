using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Exceptions
{
    public class InvalidActionException : Exception
    {
        public InvalidActionException()
        {
        }

        public InvalidActionException(string message) : base(message)
        {
        }

        public InvalidActionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidActionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
