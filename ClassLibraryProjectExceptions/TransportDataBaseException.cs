using System;


namespace LibraryProjectExceptions
{
    [Serializable]
    public class TransportDataBaseException: Exception
    {
        public TransportDataBaseException() { }

        public TransportDataBaseException(string message)
            : base(message) { }
        public TransportDataBaseException(string message, Exception inner)
            :base(message, inner) { }
    }
}
