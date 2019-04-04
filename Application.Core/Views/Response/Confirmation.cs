namespace Application.Core.Views.Response
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
