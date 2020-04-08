using ekklesia.Models.TransactionModel;


namespace ekklesia.Models.ViewModels
{
    public class ExpenditureEditViewModel : ExpenditureCreateViewModel
    {
        public ExpenditureEditViewModel() : base()
        {
        }

        public ExpenditureEditViewModel(Expenditure expenditure, string title = "Editar")
        {
            Id = expenditure.Id;
            Date = expenditure.Date;
            Value = expenditure.Value;
            Description = expenditure.Description;
            PageTitle = title;
        }

        public string PageTitle { get; set; }
        public int Id { get; set; }

    }
}
