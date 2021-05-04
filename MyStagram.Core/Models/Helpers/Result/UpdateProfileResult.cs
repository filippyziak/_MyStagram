namespace MyStagram.Core.Models.Helpers.Result
{
    public class UpdateProfileResult
    {

        public string NewUserName { get; }
        public string NewSurname { get; }
        public string NewName { get; }
        public string NewDescription { get; }
        public string NewEmail { get; }
        public bool IsPrivate { get; }

        public UpdateProfileResult(string newUserName, string newSurname, string newName, string newDescription, string newEmail, bool isPrivate)
        {
            NewUserName = newUserName;
            NewSurname = newSurname;
            NewName = newName;
            NewDescription = newDescription;
            NewEmail = newEmail;
            IsPrivate = isPrivate;
        }
    }
}