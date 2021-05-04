using System;

namespace MyStagram.Core.Models.Dtos.Social
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string SenderName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public bool IsRead { get; set; }
    }
}