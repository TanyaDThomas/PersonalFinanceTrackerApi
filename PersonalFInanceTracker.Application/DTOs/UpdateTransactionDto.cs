using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Application.DTOs
{
    public class UpdateTransactionDto
    {
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(typeof(decimal), "0.01", "999.99")]
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public int TransactionTypeId { get; set; }
    }
}
