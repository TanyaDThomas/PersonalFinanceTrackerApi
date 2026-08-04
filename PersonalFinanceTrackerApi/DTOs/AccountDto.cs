using PersonalFinanceTrackerApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerApi.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }
    }
}
