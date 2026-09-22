using PersonalFinanceTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonalFinanceTracker.Application.Models
{
    public class TransactionQueryParameters
    {
        public int? AccountId { get; set; }

        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? TransactionTypeName { get; set; }

        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    //public class TransactionQueryParameters
    //{
    //    public int? AccountId { get; set; }
    //    public int? CategoryId { get; set; }
    //    public int? TransactionTypeId { get; set; }

    //    public decimal? MinAmount { get; set; }
    //    public decimal? MaxAmount { get; set; }

    //    public DateTime? StartDate { get; set; }
    //    public DateTime? EndDate { get; set; }

    //    public string? Search { get; set; }

    //    public string? SortBy { get; set; }
    //    public string? SortDirection { get; set; }

    //    public int PageNumber { get; set; } = 1;
    //    public int PageSize { get; set; } = 10;

    //}
}
