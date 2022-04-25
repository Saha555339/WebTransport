using System;

namespace LibraryProjectExceptions
{
    [Serializable]
    public class LogicExceptions: Exception
    {
        public LogicExceptions() { }
        public LogicExceptions(string message)
        :base(message) { }
        public LogicExceptions(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
