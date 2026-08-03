using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerApi.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
