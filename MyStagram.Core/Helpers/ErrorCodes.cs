namespace MyStagram.Core.Helpers
{
    public static class ErrorCodes
    {
        public const string ServiceError = "SERVICE_UNAVAILABLE";
        public const string AuthError = "AUTHORIZATION_ERROR";
        public const string ServerError = "SERVER_ERROR";
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string AccountNotConfirmed = "ACCOUNT_NOT_CONFIRMED";
        public const string EmailExists = "EMAIL_EXISTS";
        public const string UsernameExists = "USERNAME_EXISTS";
        public const string TokenInvalid = "TOKEN_INVALID";
        public const string TokenExpired = "TOKEN_EXPIRED";
        public const string ResetPasswordFailed = "RESET_PASSWORD_FAILED";
        public const string CannotGenerateResetPasswordToken = "GENERATE_RESET_PASSWORD_TOKEN_FAILED";
        public const string EntityNotFound = "ENTITY_NOT_FOUND";
        public const string ValidationError = "VALIDATION_ERROR";
        public const string CrudActionFailed = "CRUD_ACTION_FAILED";
        public const string PermissionDenied = "PERMISSION_DENIED";
        public const string UpdateFailed = "UPDATE_ERROR";
        public const string ChangePasswordFailed = "CHANGE_PASSWORD_FAILED";
        public const string UploadPhotoFailed = "UPLOAD_PICTURE_FAILED";
        public const string DeletePhotoFailed = "DELETE_PICTURE_FAILED";
        public const string ConnectionFailed = "CONNECTION_FAILED";
        public const string AccountBlocked = "ACCOUNT_BLOCKED";
        public const string AdminActionFailed = "ADMIN_ACTION_FAILED";

    }
}