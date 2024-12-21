namespace WodItEasy.Domain.Models.Athletes
{
    using Common;
    using Exceptions;

    using static ModelConstants.PhoneNumber;

    public class PhoneNumber : ValueObject
    {
        internal PhoneNumber(string number)
        {
            this.Validate(number);

            this.Number = number;
        }

        public string Number { get; }

        public static implicit operator string(PhoneNumber number) => number.Number;

        public static implicit operator PhoneNumber(string number) => new(number);

        private void Validate(string phoneNumber)
        {
            Guard.ForStringLength<InvalidPhoneNumberException>(
                phoneNumber,
                MinPhoneNumberLength,
                MaxPhoneNumberLength,
                nameof(this.Number));

            if (!phoneNumber.StartsWith(PhoneNumberFirstSymbol))
            {
                throw new InvalidPhoneNumberException($"Phone number must start with a '{PhoneNumberFirstSymbol}'.");
            }
        }
    }
}
