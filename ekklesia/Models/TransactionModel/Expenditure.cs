using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.TransactionModel
{
    public class Expenditure : Transaction
    {

        public Expenditure()
        {
            TransactionType = TransactionType.DESPESA;
            this.Date = DateTime.Now;
        }

        [Required]
        public string Description { get; set; }

        public string Invoice { get; set; }

        public override object ToJson()
        {
            return new
            {
                Id,
                Date = Date.ToString("O"),
                Value = (decimal)Value,
                Description,
                Evento = Occasion.EventType.ToString()
            };

        }
    }
}
