using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerApi.DTOs
{
    public class UpdateAccountDto
    {
        [Required]
        [MaxLength(100)]
        public string AccountName { get; set; } = string.Empty;

        [Required]
        public int AccountTypeId { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}
