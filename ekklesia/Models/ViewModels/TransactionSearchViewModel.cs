using ekklesia.Models.TransactionModel;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class TransactionSearchViewModel
    {
        public TransactionSearchViewModel()
        {
            PageTitle = "Buscar Transação";           
        }

        public TransactionType? TransactionType { get; set; }

        
        public int? Max { get; set; }
        public int? Min { get; set; }
        public int? Days { get; set; }

        public string PageTitle { get; set; }
    }
}
