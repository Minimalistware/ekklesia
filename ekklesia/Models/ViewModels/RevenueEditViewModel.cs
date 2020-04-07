using ekklesia.Models.TransactionModel;

namespace ekklesia.Models.ViewModels
{
    public class RevenueEditViewModel : RevenueCreateViewModel
    {
        public RevenueEditViewModel() : base()
        {
        }

        public RevenueEditViewModel(Revenue revenue, string title = "Editar") :
            base(revenue)
        {
            Id = revenue.Id;
            PageTitle = title;
        }

        public string PageTitle { get; set; }
        public int Id { get; set; }
    }
}
