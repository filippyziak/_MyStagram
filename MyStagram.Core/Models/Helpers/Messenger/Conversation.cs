using MyStagram.Core.Helpers;

namespace MyStagram.Core.Models.Helpers.Messenger
{
    public class Conversation
    {
        public string Id { get; } = Utils.Id();
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }

        public LastMessage LastMessage { get; set; }
    }
}