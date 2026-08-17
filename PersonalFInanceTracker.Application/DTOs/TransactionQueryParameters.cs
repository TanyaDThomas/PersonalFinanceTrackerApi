namespace PersonalFinanceTracker.Application.DTOs
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
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
    }
}
