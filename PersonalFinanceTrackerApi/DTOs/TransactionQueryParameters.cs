namespace PersonalFinanceTrackerApi.DTOs
{
    public class TransactionQueryParameters
    {
        public string? Description { get; set; }

        public int? AccountId { get; set; }

        public string? CategoryName { get; set; }

        public string? TransactionTypeName { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; } 
    }
}
