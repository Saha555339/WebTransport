using System;

namespace LibraryProjectExceptions
{
    [Serializable]
    public class TransportParseException: Exception
    {
        public TransportParseException() { }

        public TransportParseException(string message)
            :base(message) { }

    }
}
