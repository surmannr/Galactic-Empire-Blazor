using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Exceptions
{
    public class InProcessException : Exception
    {
        public InProcessException()
        {
        }

        public InProcessException(string message) : base(message)
        {
        }

        public InProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InProcessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
