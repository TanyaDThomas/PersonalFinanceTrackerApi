using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Account { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
    }
}
