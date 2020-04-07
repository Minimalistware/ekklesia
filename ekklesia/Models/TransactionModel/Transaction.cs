using Caelum.Stella.CSharp.Vault;
using ekklesia.Models.EventModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.TransactionModel
{
    public abstract class Transaction
    {

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Money Value { get; set; }
        public TransactionType TransactionType { get; set; }
        public int OccasionId { get; set; }
        public Occasion Occasion { get; set; }

        public abstract Object ToJson();
    }
}
