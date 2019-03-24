using System.Collections.Generic;

namespace Application.Users.Views.Response
{
    public class Error
    {
        public string Type { get; set; }

        public string Message { get; set; }

        public ICollection<string> Data { get; set; }

        public string StackTrace { get; set; }
    }
}
