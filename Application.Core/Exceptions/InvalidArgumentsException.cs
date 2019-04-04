using System;
namespace Application.Core.Exceptions
{
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException(string message)
            : base(message) { }

        public InvalidArgumentsException()
            : base() { }
    }
}
