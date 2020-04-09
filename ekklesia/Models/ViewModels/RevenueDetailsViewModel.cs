using ekklesia.Models.TransactionModel;

namespace ekklesia.Models.ViewModels
{
    public class RevenueDetailsViewModel : TransactionDetailsViewModel
    {
        public RevenueDetailsViewModel(Revenue revenue)
        {
            var culture = new System.Globalization.CultureInfo("pt-BR");
            Date = revenue.Date.ToString("D", culture);
            Value = revenue.Value.ToString();
            PageTitle = "Receita";
            Occasion = $"{revenue.Occasion.EventType} {revenue.Occasion.Date}";

        }
    }
}
