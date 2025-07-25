﻿namespace WodItEasy.Common.Domain.Models
{
    using System.Text.RegularExpressions;

    using static Constants;

    public static class Guard
    {
        public static void AgainstEmptyString<TException>(string value, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (!string.IsNullOrEmpty(value))
                return;

            ThrowException<TException>($"{name} cannot be null ot empty.");
        }

        public static void ForStringLength<TException>(
            string value,
            int minLength,
            int maxLength,
            string name = "Value")
            where TException : BaseDomainException, new()
        {
            AgainstEmptyString<TException>(value, name);

            if (minLength <= value.Length && value.Length <= maxLength)
                return;

            ThrowException<TException>(
                $"{name} must have between {minLength} and {maxLength} symbols.");
        }

        public static void AgainstOutOfRange<TException>(
            int number,
            int min,
            int max,
            string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (min <= number && number <= max)
                return;

            ThrowException<TException>($"{name} must be between {min} and {max}.");
        }

        public static void AgainstOutOfRange<TException>(
            decimal number,
            decimal min,
            decimal max,
            string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (min <= number && number <= max)
                return;

            ThrowException<TException>($"{name} must be between {min} and {max}.");
        }

        public static void AgainstOutOfRange<TException>(
            DateTime date,
            DateTime min,
            DateTime max,
            string name = "Date")
            where TException : BaseDomainException, new()
        {
            if (date >= min && date <= max)
                return;

            ThrowException<TException>(
                $"{name} must be between {min.Date.ToShortDateString()} and {max.Date.ToShortDateString()}.");
        }

        public static void ForValidUrl<TException>(string url, string name = "Value")
            where TException : BaseDomainException, new()
        {
            AgainstEmptyString<TException>(url, name);

            if (url.Length <= UrlMaxLength && 
                Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return;

            ThrowException<TException>($"{name} must be a valid URL.");
        }

        public static void ForValidEmail<TException>(string email, string name = "Value")
            where TException : BaseDomainException, new()
        {
            AgainstEmptyString<TException>(email, name);

            var regEx = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (Regex.IsMatch(email, regEx))
                return;

            ThrowException<TException>($"{name} must be a valid email address.");
        }

        public static void Against<TException>(
            object actualValue,
            object unexpectedValue,
            string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (!actualValue.Equals(unexpectedValue))
                return;

            ThrowException<TException>($"{name} must not be {unexpectedValue}.");
        }

        private static void ThrowException<TException>(string message)
            where TException : BaseDomainException, new()
        {
            var exception = new TException()
            {
                Message = message
            };

            throw exception;
        }
    }
}
