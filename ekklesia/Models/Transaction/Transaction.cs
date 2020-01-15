using Caelum.Stella.CSharp.Vault;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.Transaction
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Money Value { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
