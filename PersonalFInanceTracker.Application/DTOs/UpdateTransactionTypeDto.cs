using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Application.DTOs
{
    public class UpdateTransactionTypeDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
