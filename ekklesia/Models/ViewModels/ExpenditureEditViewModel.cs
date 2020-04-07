using ekklesia.Models.TransactionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class ExpenditureEditViewModel : ExpenditureCreateViewModel
    {
        public ExpenditureEditViewModel() : base()
        {
        }

        public ExpenditureEditViewModel(Expenditure expenditure, string title = "Editar") :
            base(expenditure)
        {
            Id = expenditure.Id;
            PageTitle = title;
        }

        public string PageTitle { get; set; }
        public int Id { get; set; }        

    }
}
