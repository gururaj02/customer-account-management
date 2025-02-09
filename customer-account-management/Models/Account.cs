using System.ComponentModel.DataAnnotations;

namespace customer_account_management.Models
{
    public class Account
    {
        [Key]
        public long AccountNumber { get; set; }

        [Required]
        public string FullName { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
