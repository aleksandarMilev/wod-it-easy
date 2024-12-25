namespace WodItEasy.Infrastructure.Persistence.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    [Owned]
    public class PhoneNumber
    {
        private const int NumberMaxLength = 20;

        [Required]
        [MaxLength(NumberMaxLength)]
        public string Number { get; set; } = null!;
    }
}
