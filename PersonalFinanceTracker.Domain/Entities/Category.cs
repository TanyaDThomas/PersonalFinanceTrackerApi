using System.Diagnostics.Eventing.Reader;

namespace PersonalFinanceTracker.Domain.Entities
{
    public class Category : BaseEntity
    {
     
        public string Name { get; set; } = string.Empty;

        //FUTURE IDENTITY public string UserId { get; set; } = string.Empty;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public bool IsActive { get; set; } = true;
    }
}
