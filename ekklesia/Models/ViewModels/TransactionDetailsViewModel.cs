using ekklesia.Models.TransactionModel;

namespace ekklesia.Models.ViewModels
{
    public class TransactionDetailsViewModel
    {
        public TransactionDetailsViewModel(Transaction transaction)
        {
            var culture = new System.Globalization.CultureInfo("pt-BR");
            Date = transaction.Date.ToString("D", culture);
            Value = transaction.Value.ToString();
            //Type = transaction.Type.ToString();
            //Category = transaction.Category;
        }

        public string Date { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string PageTitle { get; set; }
    }
}
