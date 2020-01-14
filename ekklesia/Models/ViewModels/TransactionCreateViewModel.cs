using Caelum.Stella.CSharp.Vault;
using ekklesia.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class TransactionCreateViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Money Value { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        public string PageTitle { get; set; }
    }
}
