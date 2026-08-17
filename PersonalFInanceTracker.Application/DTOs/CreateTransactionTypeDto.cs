using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Application.DTOs
{
    public class CreateTransactionTypeDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
