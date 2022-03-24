using System;

namespace WebTransport.ProjectExceptions
{
    [Serializable]
    public class TransportParseException: Exception
    {
        public TransportParseException() { }

        public TransportParseException(string message)
            :base(message) { }

    }
}
