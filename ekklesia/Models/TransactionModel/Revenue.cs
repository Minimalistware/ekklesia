using System;

namespace ekklesia.Models.TransactionModel
{
    public class Revenue : Transaction
    {
        public Revenue()
        {
            TransactionType = TransactionType.RECEITA;
        }

        public RevenueType RevenueType { get; set; }

        public override object ToJson()
        {
            return new
            {
                Id,
                Date = Date.ToString("O"),
                Value = (decimal)Value,
                Type = RevenueType.ToString(),
                Evento = Occasion.EventType.ToString()
            };

        }
    }
}
