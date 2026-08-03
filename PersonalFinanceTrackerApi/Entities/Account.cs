namespace PersonalFinanceTrackerApi.Entities
{
    public class Account : BaseEntity
    {
       
        public string AccountName { get; set; } = string.Empty;
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; } = null!;
        public decimal CurrentBalance { get; set; }
        //FUTURE IDENTITY public string UserId {get; set;} = string.Empty;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public bool IsActive { get; set; } = true;

    }
}
