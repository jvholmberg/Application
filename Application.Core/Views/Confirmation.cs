using System;
namespace Application.Core.Views
{
    public class Confirmation
    {

        public string Message { get; set; }

        public Confirmation(string message)
        {
            Message = message;
        }
    }
}
