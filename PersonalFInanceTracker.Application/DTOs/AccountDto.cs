using PersonalFinanceTracker.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Application.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }

    }
}
