using System;
namespace Application.Core.Views.Response
{
    public class Error
    {

        public string Type { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public Error(Exception ex, bool includeStackTrace = false)
        {
            Type = ex.Source;
            Message = ex.Message;
            if (includeStackTrace)
            {
                StackTrace = ex.StackTrace;
            }
        }
    }
}
