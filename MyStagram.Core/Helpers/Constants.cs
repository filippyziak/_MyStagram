using MyStagram.Core.Enums;
using MyStagram.Core.Models.Helpers.Email;

namespace MyStagram.Core.Helpers
{
    public static class Constants
    {
        private const string BorderBottom = "<div style=" + "\"border-bottom:2px solid black; margin-top: 5px; margin-bottom: 5px;\"" + "></div>";

        #region values

        public const int MaxUserNameLength = 32;
        public const int MinUserNameLength = 4;
        public const int MinNameLength = 2;
        public const int MaxUserPasswordLength = 32;
        public const int MinUserPasswordLength = 6;
        public const int MaximumDescriptionLength = 500;
        public const int MaximumCommentLength = 255;
        public const int ServerHostedServiceTimeInDays = 1;
        public const string FakeEmail = "filippyziak0@gmail.com";
        public const string NlogConfig = "nlog.config";

        public const string INFO = "INFO";
        public const string DEBUG = "DEBUG";
        public const string WARNING = "WARNING";
        public const string ERROR = "ERROR";

        public const int InfoLogLifeTimeInMonths = 1;
        public const int WarningLogLifeTimeInMonths = 2;
        public const int ErrorLogLifeTimeInMonths = 3;

        #endregion

        #region policies

        public const string AdminPolicy = "AdminPolicy";
        public const string HeadAdminPolicy = "HeadAdminPolicy";

        public const string CorsPolicy = "CorsPolicy";

        #endregion

        #region roles

        public static string AdminRole = "Admin";
        public static string HeadAdminRole = "HeadAdmin";

        public static string[] AdminRoles = { AdminRole, HeadAdminRole };
        public static RoleName[] RolesToSeed = { RoleName.User, RoleName.Admin, RoleName.HeadAdmin };

        #endregion

        #region emails

        public static EmailMessage ActivationAccountEmail(string email, string username, string callbackUrl)
           => new EmailMessage(
               email: email,
               subject: "MyStagram - activate your account",
               message: $"<p>Hi <strong>{username}</strong>!</p>" +
                   BorderBottom +
                   $"<p>In order to activate your account on MyStagram, click link below.<br><br>" +
                   $"Activation account link: <a href='{callbackUrl}'>LINK</a></p>" +
                   "<p>Best regards,<br>" +
                   "MyStagram team</p>"
       );

        public static EmailMessage ResetPasswordEmail(string email, string username, string callbackUrl)
            => new EmailMessage(
                email: email,
                subject: "MyStagram - Reset Your Password!",
                message: $"<p>Hi <strong>{username}</strong>!</p>" +
                    BorderBottom +
                    $"<p>In order to reset your password on MyStagram, click link below.<br><br>" +
                    $"Password reset link: <a href='{callbackUrl}'>LINK</a></p>" +
                    "<p>Best regards,<br>" +
                    "MyStagram team</p>"
        );
        #endregion
    }
}