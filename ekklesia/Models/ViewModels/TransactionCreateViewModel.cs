using ekklesia.Models.TransactionModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class TransactionCreateViewModel
    {
        public TransactionCreateViewModel()
        {
        }

        public TransactionCreateViewModel(Transaction transaction, string title = "Criar")
        {
            Date = transaction.Date;
            Value = transaction.Value;
            Type = transaction.Type;
            Category = transaction.Category;
            PageTitle = title;
        }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        [Required]
        public string Category { get; set; }
        public string PageTitle { get; set; }
    }
}
