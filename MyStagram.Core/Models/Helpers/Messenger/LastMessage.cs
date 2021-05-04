using System;

namespace MyStagram.Core.Models.Helpers.Messenger
{
    public class LastMessage
    {
        public string Content { get; set; }
        public string SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
    }
}