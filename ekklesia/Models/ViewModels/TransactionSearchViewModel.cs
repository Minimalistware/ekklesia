using ekklesia.Models.TransactionModel;
using System;

namespace ekklesia.Models.ViewModels
{
    public class TransactionSearchViewModel
    {
        public TransactionSearchViewModel()
        {
            PageTitle = "Buscar Membro";
        }

        public TransactionType? Type { get; set; }
        public string Category { get; set; }
        public string PageTitle { get; set; }
    }
}
