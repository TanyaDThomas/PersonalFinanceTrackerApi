
using System.ComponentModel;

namespace PersonalFinanceTracker.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate {  get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; } = null!;

        //FUTURE IDENTITY public string UserId { get; set; } = string.Empty;


       
    }
}
