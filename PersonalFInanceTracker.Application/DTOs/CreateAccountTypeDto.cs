using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Application.DTOs
{
    public class CreateAccountTypeDto
    {
        [Required]
        [MaxLength(50)]
        public string AccountTypeName { get; set; } = string.Empty;
    }
}
