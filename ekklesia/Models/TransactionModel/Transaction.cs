using Caelum.Stella.CSharp.Vault;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.TransactionModel
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

        public string ToJson()
        {
            return JObject.FromObject(
            new
            {
                Id,
                Date = Date.ToString("O"),
                Value = (decimal)Value,
                Type = Type.ToString(),
                Category
            }).ToString();

        }
    }
}
