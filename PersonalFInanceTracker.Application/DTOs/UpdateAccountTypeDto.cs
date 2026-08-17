using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Application.DTOs
{
    public class UpdateAccountTypeDto
    {
        [Required]
        [MaxLength(50)]
        public string AccountTypeName { get; set; } = string.Empty;
    }
}
