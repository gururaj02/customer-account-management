using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace customer_account_management.Models
{
    public class User
    {
        [Key]
        public long AccountNumber { get; set; }

        [Required]
        public string FullName { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0;

        [Required]
        public int SecurityPin { get; set; }
    }
}
