using ekklesia.Models.TransactionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class TransactionEditViewModel : TransactionCreateViewModel
    {
        public TransactionEditViewModel():base()
        {
        }

        public TransactionEditViewModel(Transaction transaction, string title = "Editar") :
            base(transaction, title)
        {
            Id = transaction.Id;
        }

        public int Id { get; set; }
    }
}
