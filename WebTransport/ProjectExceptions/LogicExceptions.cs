using System;

namespace WebTransport.ProjectExceptions
{
    [Serializable]
    public class LogicExceptions: Exception
    {
        public LogicExceptions() { }
        public LogicExceptions(string message)
        :base(message) { }
    }
}
