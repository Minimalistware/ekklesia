using ekklesia.Models.TransactionModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class RevenueCreateViewModel : TransactionCreateViewModel
    {
        [Required]
        public RevenueType RevenueType { get; set; }

        public RevenueCreateViewModel()
        {
            this.Date = DateTime.Now;
        }

        public RevenueCreateViewModel(HashSet<SelectListItem> events)
        {
            AllEvents = events;
            this.Date = DateTime.Now;
        }

        public RevenueCreateViewModel(Revenue revenue)
        {
            Date = revenue.Date;
            Value = revenue.Value;
            RevenueType = revenue.RevenueType;
            OccasionId = revenue.OccasionId;
        }

    }
}
