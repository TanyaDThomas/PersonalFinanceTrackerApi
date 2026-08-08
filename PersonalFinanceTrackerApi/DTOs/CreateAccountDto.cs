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
        [Required]
        [Range(typeof(decimal), "0.01", "999999999999999.99")]
        public decimal CurrentBalance { get; set; }
    }
    
}
