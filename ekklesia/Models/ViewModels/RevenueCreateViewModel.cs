using ekklesia.Models.TransactionModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class RevenueCreateViewModel
    {
        public RevenueCreateViewModel()
        {

        }

        public RevenueCreateViewModel(Revenue revenue)
        {
            Date = revenue.Date;
            Value = revenue.Value;
            RevenueType = revenue.RevenueType;
            OccasionId = revenue.OccasionId;
        }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public RevenueType RevenueType { get; set; }
        public int OccasionId { get; set; }
    }
}
