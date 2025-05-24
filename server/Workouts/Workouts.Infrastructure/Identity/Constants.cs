namespace WodItEasy.Workouts.Infrastructure.Identity
{
    public static class Constants
    {
        public const int DefaultTokenExpirationTime = 7;

        public const int ExtendedTokenExpirationTime = 30;

        public const string AdministratorRoleName = "Administrator";

        public const string InvalidLoginErrorMessage = "Invalid login attempt.";
    }
}
