namespace MyStagram.Core.Models.Helpers.Email
{
    public class EmailMessage
    {
        public string Email { get; private set; }
        public string Subject { get; private set; }
        public string Message { get; private set; }

        public EmailMessage(string email, string subject, string message)
        {
            Email = email;
            Subject = subject;
            Message = message;
        }

    }
}