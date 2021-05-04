namespace MyStagram.Core.Models.Dtos.Messenger
{
    public class RecipientDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsPrivate { get; set; }
    }
}