using ekklesia.Models.TransactionModel;

namespace ekklesia.Models.ViewModels
{
    public class ExpenditureDetailsViewModel : TransactionDetailsViewModel
    {
        public ExpenditureDetailsViewModel(Expenditure expenditure)
        {
            var culture = new System.Globalization.CultureInfo("pt-BR");
            Date = expenditure.Date.ToString("D", culture);
            Value = expenditure.Value.ToString();
            PageTitle = "Despesa";
            Occasion = $"{expenditure.Occasion.EventType} {expenditure.Occasion.Date.ToString("D", culture)}";
            ExistingPhotoPath = expenditure.Invoice;
        }

        public string Description { get; set; }
        public string ExistingPhotoPath { get; set; }

    }
}
