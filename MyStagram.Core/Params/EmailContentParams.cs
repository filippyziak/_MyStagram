using SendGrid.Helpers.Mail;

namespace MyStagram.Core.Params
{
    public class EmailContentParams
    {
        private readonly string sender;
        private readonly string receiver;

        public EmailAddress FromAddress { get; private set; }
        public EmailAddress ToAddress { get; private set; }

        public EmailContentParams(string sender, string receiver)
        {
            this.sender = sender;
            this.receiver = receiver;

            BuildEmailContent();
        }

        private void BuildEmailContent()
        {
            FromAddress = new EmailAddress(sender);
            ToAddress = new EmailAddress(receiver);
        }
    }
}