namespace WodItEasy.Workouts.Web.Extensions
{
    using System;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        private const string SectionNotFoundMessage = "The {0} section is not found!";
        private const string ConnectionString = "DefaultConnection";
        private const string AdminEmail = "Admin:Email";
        private const string AdminPassword = "Admin:Password";

        public static string GetConnectionString(this IConfiguration configuration)
            => configuration
                .GetConnectionString(ConnectionString)
                ?? throw new InvalidOperationException(
                    string.Format(SectionNotFoundMessage, ConnectionString));

        public static string GetAdminEmail(this IConfiguration configuration)
            => configuration[AdminEmail]
                ?? throw new InvalidOperationException(
                    string.Format(SectionNotFoundMessage, AdminEmail));

        public static string GetAdminPassword(this IConfiguration configuration)
            => configuration[AdminPassword]
                ?? throw new InvalidOperationException(
                    string.Format(SectionNotFoundMessage, AdminPassword));
    }
}