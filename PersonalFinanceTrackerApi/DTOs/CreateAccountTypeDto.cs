using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerApi.DTOs
{
    public class CreateAccountTypeDto
    {
        [Required]
        [MaxLength(50)]
        public string AccountTypeName { get; set; } = string.Empty;
    }
}
