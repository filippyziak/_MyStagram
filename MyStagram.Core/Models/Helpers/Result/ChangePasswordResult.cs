namespace MyStagram.Core.Models.Helpers.Result
{
    public class ChangePasswordResult
    {

        public bool HasChanged { get; }
        public string ErrorMessage { get; }
        public ChangePasswordResult(bool hasChanged, string errorMessage = null)
        {
            HasChanged = hasChanged;
            ErrorMessage = errorMessage;
        }

    }
}