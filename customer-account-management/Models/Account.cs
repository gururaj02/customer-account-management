using System.ComponentModel.DataAnnotations;

namespace customer_account_management.Models
{
    public class Account
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
