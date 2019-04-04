using System;
namespace Application.Core.Exceptions
{
    public class ExistingFoundException : Exception
    {
        public ExistingFoundException(string message)
            : base(message) { }

        public ExistingFoundException()
            : base() { }
    }
}
