using ekklesia.Models.TransactionModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class ExpenditureCreateViewModel
    {
        public ExpenditureCreateViewModel()
        {

        }

        public ExpenditureCreateViewModel(Expenditure expenditure)
        {
            Date = expenditure.Date;
            Value = expenditure.Value;
            Description = expenditure.Description;
            Invoice = expenditure.Invoice;
            OccasionId = expenditure.OccasionId;
        }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public string Description { get; set; }
        public string Invoice { get; set; }
        public int OccasionId { get; set; }

    }
}
