using PersonalFinanceTrackerApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerApi.DTOs
{
    public class CreateAccountDto
    {
        [Required]
        [MaxLength(100)]
        public string AccountName { get; set; } = string.Empty;

        [Required]
        public int AccountTypeId { get; set; }

        public decimal CurrentBalance { get; set; }
    }
    
}
